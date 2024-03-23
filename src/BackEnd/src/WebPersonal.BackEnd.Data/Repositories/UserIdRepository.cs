using Dapper;
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
            string sql = $"insert into {TableName} ({nameof(UserIdEntity.UserName).ToLower()}) " +
                      $"values (@{nameof(UserIdEntity.UserName)});" +
                      $"SELECT LAST_INSERT_ID();";
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            var newId = (await connection.QueryAsync<int>(sql, new
            {
                UserName = obj.UserName
            })).First();
            return UserIdEntity.UpdateUserId(newId, obj);
        }

        public override async Task<UserIdEntity> UpdateSingle(UserIdEntity obj)
        {
            string sql = $"Update {TableName} " +
                  $"set {nameof(UserIdEntity.UserName).ToLower()} = @{nameof(UserIdEntity.UserName)} " +
                      $"Where {nameof(UserIdEntity.UserId).ToLower()} = @{nameof(UserIdEntity.UserId)}";
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            int filasAfectadas = await connection.ExecuteAsync(sql, new
            {
                UserName = obj.UserName,
                UserId = obj.UserId
            });
            return obj;
        }
    }
}
