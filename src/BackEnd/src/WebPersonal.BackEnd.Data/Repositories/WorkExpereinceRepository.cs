using Dapper;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;
using WebPersonal.Shared.Data.Db;


namespace WebPersonal.BackEnd.Model.Repositories
{
    public class WorkExpereinceRepository : BaseRepository<WorkExperienceEntity>
    {

        public override string TableName => TableNames.WorkExperience;

        public WorkExpereinceRepository(TransactionalWrapper conexion) : base(conexion)
        {
        }

        public override async Task<WorkExperienceEntity> InsertSingle(WorkExperienceEntity obj)
        {
            string sql = $"insert into {TableName} ({nameof(WorkExperienceEntity.UserId).ToLower()}, {nameof(WorkExperienceEntity.Position).ToLower()}, " +
                   $"{nameof(WorkExperienceEntity.CompanyName).ToLower()}, {nameof(WorkExperienceEntity.City)}, {nameof(WorkExperienceEntity.Country).ToLower()}," +
                   $"{nameof(WorkExperienceEntity.StartDate).ToLower()}, {nameof(WorkExperienceEntity.EndDate).ToLower()}, {nameof(WorkExperienceEntity.Environment).ToLower()}) " +
                   $"values (@{nameof(WorkExperienceEntity.UserId)}, @{nameof(WorkExperienceEntity.Position)}, @{nameof(WorkExperienceEntity.CompanyName)}," +
                   $"@{nameof(WorkExperienceEntity.City)}, @{nameof(WorkExperienceEntity.Country)}, @{nameof(WorkExperienceEntity.StartDate)}," +
                   $"@{nameof(WorkExperienceEntity.EndDate)}, @{nameof(WorkExperienceEntity.Environment)} );" +
                   $"SELECT LAST_INSERT_ID();";
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            var newId = (await connection.QueryAsync<int>(sql, new
            {
                obj.UserId,
                obj.Position,
                obj.CompanyName,
                obj.City,
                obj.Country,
                obj.StartDate,
                obj.EndDate,
                obj.Environment
            })).First();
            return WorkExperienceEntity.UpdateId(newId, obj);
        }

        public override async Task<WorkExperienceEntity> UpdateSingle(WorkExperienceEntity obj)
        {
            string sql = $"Update {TableName} " +
            $"set {nameof(WorkExperienceEntity.Position).ToLower()} = @{nameof(WorkExperienceEntity.Position)}, " +
                $"{nameof(WorkExperienceEntity.CompanyName).ToLower()} = @{nameof(WorkExperienceEntity.CompanyName)}, " +
                $"{nameof(WorkExperienceEntity.City).ToLower()} = @{nameof(WorkExperienceEntity.City)}, " +
                $"{nameof(WorkExperienceEntity.Country).ToLower()} = @{nameof(WorkExperienceEntity.Country)}," +
                $"{nameof(WorkExperienceEntity.StartDate).ToLower()} = @{nameof(WorkExperienceEntity.StartDate)}, " +
                $"{nameof(WorkExperienceEntity.EndDate).ToLower()} = @{nameof(WorkExperienceEntity.EndDate)}, " +
                $"{nameof(WorkExperienceEntity.Environment).ToLower()} = @{nameof(WorkExperienceEntity.Environment)} " +
                $"Where {nameof(WorkExperienceEntity.Id).ToLower()} = @{nameof(WorkExperienceEntity.Id)}";

            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            int filasAfectadas = await connection.ExecuteAsync(sql, new
            {
                obj.Position,
                obj.CompanyName,
                obj.City,
                obj.Country,
                obj.StartDate,
                obj.EndDate,
                obj.Environment,
                obj.Id
            });
            return obj;
        }
    }
}
