using Dapper;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;
using WebPersonal.Shared.Data.Db;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class EducationRespository : BaseRepository<EducationEntity>
    {
        public override string TableName => TableNames.Education;

        public EducationRespository(TransactionalWrapper conexion) : base(conexion)
        {
        }

        public override async Task<EducationEntity> InsertSingle(EducationEntity obj)
        {
            string sql = $"insert into {TableName} ({nameof(EducationEntity.UserId).ToLower()}, {nameof(EducationEntity.StartDate).ToLower()}, " +
                     $"{nameof(EducationEntity.EndDate).ToLower()}, {nameof(EducationEntity.CourseName)}, {nameof(EducationEntity.CollegeName).ToLower()}," +
                     $"{nameof(EducationEntity.City).ToLower()}, {nameof(EducationEntity.Country).ToLower()}) " +
                     $"values (@{nameof(EducationEntity.UserId)}, @{nameof(EducationEntity.StartDate)}, @{nameof(EducationEntity.EndDate)}," +
                     $"@{nameof(EducationEntity.CourseName)}, @{nameof(EducationEntity.CollegeName)}, @{nameof(EducationEntity.City)}," +
                     $"@{nameof(EducationEntity.Country)});" +
                     $"SELECT LAST_INSERT_ID();";
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            var newId = (await connection.QueryAsync<int>(sql, new
            {
                obj.UserId,
                obj.StartDate,
                obj.EndDate,
                obj.CourseName,
                obj.CollegeName,
                obj.City,
                obj.Country
            })).First();
            return EducationEntity.UpdateId(newId, obj);
        }

        public override async Task<EducationEntity> UpdateSingle(EducationEntity obj)
        {
            string sql = $"Update {TableName} " +
                  $"set {nameof(EducationEntity.StartDate).ToLower()} = @{nameof(EducationEntity.StartDate)}, " +
                      $"{nameof(EducationEntity.EndDate).ToLower()} = @{nameof(EducationEntity.EndDate)}, " +
                      $"{nameof(EducationEntity.CourseName).ToLower()} = @{nameof(EducationEntity.CourseName)}, " +
                      $"{nameof(EducationEntity.CollegeName).ToLower()} = @{nameof(EducationEntity.CollegeName)}," +
                      $"{nameof(EducationEntity.City).ToLower()} = @{nameof(EducationEntity.City)}, " +
                      $"{nameof(EducationEntity.Country).ToLower()} = @{nameof(EducationEntity.Country)} " +
                      $"Where {nameof(EducationEntity.Id).ToLower()} = @{nameof(EducationEntity.Id)}";

            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            int filasAfectadas = await connection.ExecuteAsync(sql, new
            {
                obj.UserId,
                obj.StartDate,
                obj.EndDate,
                obj.CourseName,
                obj.CollegeName,
                obj.City,
                obj.Country,
                obj.Id
            });
            return obj;
        }
    }
}
