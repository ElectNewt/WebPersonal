using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;
#nullable enable

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class UserIdRepository : BaseRepository<UserIdEntity>
    {
        public override string TableName => TableNames.UserId;

        public UserIdRepository(DbConnection conexion) : base(conexion)
        {
        }

        public async Task<UserIdEntity?> GetByUserName(string userName)
        {
            using (var con = _conexion)
            {
                con.Open();
                return await con.QueryFirstOrDefaultAsync<UserIdEntity?>($"select * from {TableName} where UserName = @userName", new
                {
                    userName = userName
                });
            }
        }
    }
}
