using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace WebPersonal.Shared.Data.Db
{
    public class ConnectionWrapper : IDisposable
    {
        private readonly object _lock = new object();

        private readonly DbConnection _connection;
        private Task<DbConnection> _openConnection;
        private static readonly Task<DbConnection> _noConnection = Task.FromResult((DbConnection)null);

        public ConnectionWrapper(DbConnection connection)
        {
            _connection = connection;
        }

        public async Task<DbConnection> GetConnectionAsync(CancellationToken c = default(CancellationToken))
        {
            if (c != default(CancellationToken))
            {
                return await GetOpenConnectionOrOpenNewConnectionAsync(true, true, c);
            }

            // if cancellation token not specified, create one for 10 seconds
            using (var cts = new CancellationTokenSource())
            {
                cts.CancelAfter(10000);
                return await GetOpenConnectionOrOpenNewConnectionAsync(true, true, cts.Token);
            }
        }

        public DbConnection GetConnection()
        {
            var connTask = GetOpenConnectionOrOpenNewConnectionAsync(false, true, default(CancellationToken));
            connTask.Wait();

            return connTask.Result;
        }

        /// <summary>Get connection using async or non async model</summary>
        /// <param name="async">If true, will run async. If false, will run synchronusly and wrap the result in a task.</param>
        /// <param name="openIfNotOpenAlready">If true, will open the connection and return it if there is none. If false, returns null if the connection is not open</param>
        protected Task<DbConnection> GetOpenConnectionOrOpenNewConnectionAsync(bool async, bool openIfNotOpenAlready, CancellationToken c)// = default(CancellationToken))
        {
            lock (_lock)
            {
                if (_openConnection != null)
                {
                    return _openConnection;
                }

                if (openIfNotOpenAlready)
                {
                    _openConnection = async
                        ? BuildConnectionAsync()
                        : BuildConnection();

                    return NotifyConnectionOpened(_openConnection);
                }

                return _noConnection;
            }

            // open a connection async and return
            async Task<DbConnection> BuildConnectionAsync()
            {
                await _connection.OpenAsync(c);
                return _connection;
            }

            // open a connection NOT async and return, wrapped in a task
            Task<DbConnection> BuildConnection()
            {
                _connection.Open();
                return Task.FromResult(_connection);
            }

            async Task<DbConnection> NotifyConnectionOpened(Task<DbConnection> conn)
            {
                var cn = await conn;
                await OnConnectionOpened(cn, async);
                return cn;
            }
        }

        protected virtual Task OnConnectionOpened(DbConnection connection, bool async) => Task.CompletedTask;

        public virtual void Dispose()
        {
            _connection?.Dispose();
        }
    }
}
