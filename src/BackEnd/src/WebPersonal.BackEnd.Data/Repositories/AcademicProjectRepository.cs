using Dapper;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;
using WebPersonal.Shared.Data.Db;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class AcademicProjectRepository : BaseRepository<AcademicProjectEntity>
    {
        public override string TableName => TableNames.AcademicProject;

        public AcademicProjectRepository(TransactionalWrapper conexion) : base(conexion)
        {
        }

        public override async Task<AcademicProjectEntity> InsertSingle(AcademicProjectEntity obj)
        {
            string sql = $"insert into {TableName} ({nameof(AcademicProjectEntity.UserId).ToLower()}, {nameof(AcademicProjectEntity.EducationId).ToLower()}, " +
                     $"{nameof(AcademicProjectEntity.Name).ToLower()}, {nameof(AcademicProjectEntity.Details).ToLower()}, {nameof(AcademicProjectEntity.Environment).ToLower()}," +
                     $"{nameof(AcademicProjectEntity.Date).ToLower()}" +
                     $"values (@{nameof(AcademicProjectEntity.UserId)}, @{nameof(AcademicProjectEntity.EducationId)}, @{nameof(AcademicProjectEntity.Name)}," +
                     $"@{nameof(AcademicProjectEntity.Details)}, @{nameof(AcademicProjectEntity.Environment)}, @{nameof(AcademicProjectEntity.Date)});" +
                     $"SELECT LAST_INSERT_ID();";
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            var newId = (await connection.QueryAsync<int>(sql, new
            {
                UserId = obj.UserId,
                EducationId = obj.EducationId,
                Name = obj.Name,
                Details = obj.Details,
                Environment = obj.Environment,
                Date = obj.Date
            })).First();
            return AcademicProjectEntity.UpdateId(newId, obj);
        }

        public override async Task<AcademicProjectEntity> UpdateSingle(AcademicProjectEntity obj)
        {
            string sql = $"Update {TableName} " +
                 $"set {nameof(AcademicProjectEntity.EducationId).ToLower()} = @{nameof(AcademicProjectEntity.EducationId)}, " +
                     $"{nameof(AcademicProjectEntity.Name).ToLower()} = @{nameof(AcademicProjectEntity.Name)}, " +
                     $"{nameof(AcademicProjectEntity.Details).ToLower()} = @{nameof(AcademicProjectEntity.Details)}, " +
                     $"{nameof(AcademicProjectEntity.Environment).ToLower()} = @{nameof(AcademicProjectEntity.Environment)}," +
                     $"{nameof(AcademicProjectEntity.Date).ToLower()} = @{nameof(AcademicProjectEntity.Date)}" +
                     $"Where {nameof(AcademicProjectEntity.Id).ToLower()} = @{nameof(PersonalProfileEntity.Id)}";

            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            int filasAfectadas = await connection.ExecuteAsync(sql, new
            {
                EducationId = obj.EducationId,
                Name = obj.Name,
                Details = obj.Details,
                Environment = obj.Environment,
                Date = obj.Date,
                Id = obj.Id
            });
            return obj;
        }
    }
}
