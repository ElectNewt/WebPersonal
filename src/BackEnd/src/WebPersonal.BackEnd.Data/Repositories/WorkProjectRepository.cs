using Dapper;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;
using WebPersonal.Shared.Data.Db;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class WorkProjectRepository : BaseRepository<WorkProjectEntity>
    {

        public override string TableName => TableNames.WorkProject;

        public WorkProjectRepository(TransactionalWrapper conexion) : base(conexion)
        {
        }

        public override async Task<WorkProjectEntity> InsertSingle(WorkProjectEntity obj)
        {
            string sql = $"insert into {TableName} ({nameof(WorkProjectEntity.UserId).ToLower()}, {nameof(WorkProjectEntity.WorkId).ToLower()}, " +
                 $"{nameof(WorkProjectEntity.Name).ToLower()}, {nameof(WorkProjectEntity.Details)}, {nameof(WorkProjectEntity.Date).ToLower()}," +
                 $"{nameof(WorkProjectEntity.Environment).ToLower()}) " +
                 $"values (@{nameof(WorkProjectEntity.UserId)}, @{nameof(WorkProjectEntity.WorkId)}, @{nameof(WorkProjectEntity.Name)}," +
                 $"@{nameof(WorkProjectEntity.Details)}, @{nameof(WorkProjectEntity.Date)}, @{nameof(WorkProjectEntity.Environment)} );" +
                 $"SELECT LAST_INSERT_ID();";
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            var newId = (await connection.QueryAsync<int>(sql, new
            {
                obj.UserId,
                obj.WorkId,
                obj.Name,
                obj.Details,
                obj.Date,
                obj.Environment,
            })).First();
            return WorkProjectEntity.UpdateId(newId, obj);
        }

        public override async Task<WorkProjectEntity> UpdateSingle(WorkProjectEntity obj)
        {
            string sql = $"Update {TableName} " +
              $"set {nameof(WorkProjectEntity.WorkId).ToLower()} = @{nameof(WorkProjectEntity.WorkId)}, " +
                  $"{nameof(WorkProjectEntity.Name).ToLower()} = @{nameof(WorkProjectEntity.Name)}, " +
                  $"{nameof(WorkProjectEntity.Details).ToLower()} = @{nameof(WorkProjectEntity.Details)}, " +
                  $"{nameof(WorkProjectEntity.Date).ToLower()} = @{nameof(WorkProjectEntity.Date)}," +
                  $"{nameof(WorkProjectEntity.Environment).ToLower()} = @{nameof(WorkProjectEntity.Environment)} " +
                  $"Where {nameof(WorkProjectEntity.Id).ToLower()} = @{nameof(WorkProjectEntity.Id)}";

            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            int filasAfectadas = await connection.ExecuteAsync(sql, new
            {
                obj.WorkId,
                obj.Name,
                obj.Details,
                obj.Date,
                obj.Environment,
                obj.Id
            });
            return obj;
        }
    }
}
