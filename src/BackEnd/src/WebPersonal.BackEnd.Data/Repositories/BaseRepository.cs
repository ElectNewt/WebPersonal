using Dapper;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.Shared.Data.Db;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public abstract class BaseRepository<T>
        where T : class
    {

        protected readonly TransactionalWrapper _conexionWrapper;

        public BaseRepository(TransactionalWrapper conexion)
        {
            _conexionWrapper = conexion;
        }

        public abstract string TableName { get; }
        public abstract Task<T> InsertSingle(T obj);
        public abstract Task<T> UpdateSingle(T obj);

        public async Task<T?> GetByUserId(int userId)
        {
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            return await connection.QueryFirstOrDefaultAsync<T?>($"select * from {TableName} where UserId = @userId", new
            {
                userId = userId
            });
        }

        public async Task<List<T>> GetListByUserId(int userId)
        {
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            return (await connection.QueryAsync<T>($"select * from {TableName} where UserId = @userId", new
            {
                userId = userId
            })).ToList();
        }


        public async Task<List<T>> InsertList(List<T> obj)
        {
            return (await Task.WhenAll(obj.Select(a => InsertSingle(a)))).ToList();
        }

        public async Task<List<T>> UpdateList(List<T> obj)
        {
            return (await Task.WhenAll(obj.Select(a => UpdateSingle(a)))).ToList();
        }

        public async Task<int> Delete(int id)
        {
            string sql = $"delete from {TableName} Where id = @id";

            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            await connection.ExecuteAsync(sql, new { id });
            return id;
        }

        public async Task DeleteUnused(List<int> ids, int userId)
        {
            string sql = $"delete from {TableName} Where userid = @userId and id not in (@ids)";

            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            await connection.ExecuteAsync(sql, new { 
                userId = userId,
                ids = string.Join(",", ids)
             });
        }

        public async Task CommitTransaction()
        {
            await _conexionWrapper.CommitTransactionAsync();
        }
    }
}
