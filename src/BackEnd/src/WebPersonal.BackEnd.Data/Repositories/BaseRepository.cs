using Dapper;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public abstract class BaseRepository<T>
        where T : class
    {

        protected readonly DbConnection _conexion;
        public abstract string TableName { get; }

        public BaseRepository(DbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<T?> GetByUserId(int userId)
        {
            using(var con = _conexion)
            {
                con.Open();
                return await con.QueryFirstOrDefaultAsync<T?>($"select * from {TableName} where UserId = @userId", new
                {
                    userId = userId
                });
            }
        }

        public async Task<List<T>> GetListByUserId(int userId)
        {
            using (var con = _conexion)
            {
                con.Open();
                return (await con.QueryAsync<T>($"select * from {TableName} where UserId = @userId", new
                {
                    userId = userId
                })).ToList();
            }
        }
    }
}
