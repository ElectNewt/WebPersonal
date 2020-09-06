using Dapper;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;
using WebPersonal.Shared.Data.Db;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class InterestsRepository : BaseRepository<InterestEntity>
    {
        public override string TableName => TableNames.Interest;

        public InterestsRepository(TransactionalWrapper conexion) : base(conexion)
        {
        }

        public override async Task<InterestEntity> InsertSingle(InterestEntity obj)
        {
            string sql = $"insert into {TableName} ({nameof(InterestEntity.UserId).ToLower()}, {nameof(InterestEntity.Description).ToLower()}) " +
                      $" values (@{nameof(InterestEntity.UserId)}, @{nameof(InterestEntity.Description)});" +
                      $"SELECT LAST_INSERT_ID();";

            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            var newId = (await connection.QueryAsync<int>(sql, new
            {
                UserId = obj.UserId,
                Description = obj.Description
            })).First();
            return InterestEntity.UpdateId(obj, newId);
        }

        public override async Task<InterestEntity> UpdateSingle(InterestEntity obj)
        {
            string sql = $"Update {TableName} " +
               $"set {nameof(InterestEntity.Description).ToLower()} = @{nameof(InterestEntity.Description).ToLower()} " +
                   $"Where {nameof(InterestEntity.Id)} = @{nameof(InterestEntity.Id)}";
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            int filasAfectadas = await connection.ExecuteAsync(sql, new
            {
                Id = obj.Id,
                Description = obj.Description
            });
            return obj;
        }
    }
}
