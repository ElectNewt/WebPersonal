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
            await GetConnectionAsync(true, true, c);
            return _openTransaction;
        }

        public async Task CommitTransactionAsync(CancellationToken c = default(CancellationToken))
        {
            var result = await GetConnectionAsync(true, false, c);
            if (_openTransaction == null)
            {
                return;
            }

            lock (_lock)
            {
                _openTransaction.Commit();
                _openTransaction.Dispose();
                _openTransaction = result.BeginTransaction();
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            GetConnectionAsync(false, false, default(CancellationToken)).Wait();
            _openTransaction?.Dispose();
        }
    }
}
