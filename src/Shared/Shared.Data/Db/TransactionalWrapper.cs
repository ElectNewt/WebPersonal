using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace WebPersonal.Shared.Data.Db
{
    public class TransactionalWrapper : ConnectionWrapper
    {
        private readonly object _lock = new object();

        private DbTransaction _openTransaction;

        public TransactionalWrapper(DbConnection connection)
            : base(connection)
        {
        }

        protected override async Task OnConnectionOpened(DbConnection connection, bool async)
        {
            await base.OnConnectionOpened(connection, async);

            lock (_lock)
            {
                if (_openTransaction != null)
                {
                    throw new InvalidOperationException("You can only have 1 connection/transaction open at a time.");
                }

                _openTransaction = connection.BeginTransaction();
            }
        }

        public async Task<DbTransaction> GetTransactionAsync(CancellationToken c = default(CancellationToken))
        {
            // actual work done in OnNewConnectionOpened method
            await GetOpenConnectionOrOpenNewConnectionAsync(true, true, c);
            return _openTransaction;
        }

        public DbTransaction GetTransaction()
        {
            // actual work done in OnNewConnectionOpened method
            var connTask = GetOpenConnectionOrOpenNewConnectionAsync(false, true, default(CancellationToken));
            connTask.Wait();
            return _openTransaction;
        }

        public async Task CommitTransactionAsync(CancellationToken c = default(CancellationToken))
        {
            var result = await GetOpenConnectionOrOpenNewConnectionAsync(true, false, c);
            CommitTransactionInternal(result);
        }

        public void CommitTransaction()
        {
            var conn = GetOpenConnectionOrOpenNewConnectionAsync(false, false, default(CancellationToken));
            conn.Wait();

            CommitTransactionInternal(conn.Result);
        }

        private void CommitTransactionInternal(DbConnection conn)
        {
            // if this is null, a connection has not been openend yet
            if (_openTransaction == null)
            {
                return;
            }

            lock (_lock)
            {
                _openTransaction.Commit();
                _openTransaction.Dispose();
                _openTransaction = conn.BeginTransaction();
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            try
            {
                // call GetOpenConnectionOrOpenNewConnectionAsync to ensure that a transaction 
                // is not in the process of being created
                GetOpenConnectionOrOpenNewConnectionAsync(false, false, default(CancellationToken)).Wait();
            }
            catch (Exception e)
            {
                // may occur if there was a timeout when opening the connection
                if (!IsTaskCanceledException(e))
                {
                    throw;
                }
            }

            _openTransaction?.Dispose();
        }

        private static bool IsTaskCanceledException(Exception e)
        {
            if (e == null)
            {
                return false;
            }

            if (e is TaskCanceledException)
            {
                return true;
            }

            if (IsTaskCanceledException(e.InnerException))
            {
                return true;
            }


            return false;
        }
    }
}
