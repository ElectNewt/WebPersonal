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
                return await GetConnectionAsync(true, true, c);
            }

            using (var cts = new CancellationTokenSource())
            {
                cts.CancelAfter(10000);
                return await GetConnectionAsync(true, true, cts.Token);
            }
        }

        protected Task<DbConnection> GetConnectionAsync(bool async, bool openIfNotOpenAlready, CancellationToken c)
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

            async Task<DbConnection> BuildConnectionAsync()
            {
                await _connection.OpenAsync(c);
                return _connection;
            }

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
