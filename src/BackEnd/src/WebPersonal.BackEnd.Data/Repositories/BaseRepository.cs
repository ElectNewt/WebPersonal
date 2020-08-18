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
        public abstract string TableName { get; }

        public BaseRepository(TransactionalWrapper conexion)
        {
            _conexionWrapper = conexion;
        }

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
    }
}
