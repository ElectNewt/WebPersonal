using Dapper;
using System.Data.Common;
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
    }
}
