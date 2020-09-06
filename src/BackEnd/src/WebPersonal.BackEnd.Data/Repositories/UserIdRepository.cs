using Dapper;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;
using WebPersonal.Shared.Data.Db;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class UserIdRepository : BaseRepository<UserIdEntity>
    {
        public override string TableName => TableNames.UserId;

        public UserIdRepository(TransactionalWrapper conexion) : base(conexion)
        {
        }

        public async Task<UserIdEntity?> GetByUserName(string userName)
        {
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            return await connection.QueryFirstOrDefaultAsync<UserIdEntity?>($"select * from {TableName} where UserName = @userName", new
            {
                userName = userName
            });
        }

        public override async Task<UserIdEntity> InsertSingle(UserIdEntity obj)
        {
            string sql = $"insert into {TableName} ({nameof(UserIdEntity.UserName)}) " +
                      $"values (@{nameof(UserIdEntity.UserName)});" +
                      $"Select CAST(SCOPE_IDENTITY() as int);";
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            var newId = (await connection.QueryAsync<int>(sql, obj)).First();
            return UserIdEntity.UpdateUserId(newId, obj);
        }

        public override async Task<UserIdEntity> UpdateSingle(UserIdEntity obj)
        {
            string sql = $"Update {TableName} " +
                  $"set {nameof(UserIdEntity.UserName)} = @{nameof(UserIdEntity.UserName)} " +
                      $"Where {nameof(UserIdEntity.UserId)} = @{nameof(UserIdEntity.UserId)}";
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            int filasAfectadas = await connection.ExecuteAsync(sql, obj);
            return obj;
        }
    }
}
