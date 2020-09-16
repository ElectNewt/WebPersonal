using Dapper;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;
using WebPersonal.Shared.Data.Db;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class PersonalProjectsRepository : BaseRepository<PersonalProjectEntity>
    {

        public override string TableName => TableNames.PersonalProject;

        public PersonalProjectsRepository(TransactionalWrapper conexion) : base(conexion)
        {
        }

        public override async Task<PersonalProjectEntity> InsertSingle(PersonalProjectEntity obj)
        {
            string sql = $"insert into {TableName} ({nameof(PersonalProjectEntity.UserId).ToLower()}, {nameof(PersonalProjectEntity.Name).ToLower()}, " +
                      $"{nameof(PersonalProjectEntity.Details).ToLower()}, {nameof(PersonalProjectEntity.Environment)}, {nameof(PersonalProjectEntity.Date).ToLower()}) " +
                      $"values (@{nameof(PersonalProjectEntity.UserId)}, @{nameof(PersonalProjectEntity.Name)}, @{nameof(PersonalProjectEntity.Details)}," +
                      $"@{nameof(PersonalProjectEntity.Environment)}, @{nameof(PersonalProjectEntity.Date)});" +
                      $"SELECT LAST_INSERT_ID();";
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            var newId = (await connection.QueryAsync<int>(sql, new
            {
                obj.UserId,
                obj.Name,
                obj.Details,
                obj.Environment,
                obj.Date
            })).First();
            return PersonalProjectEntity.UpdateId(newId, obj);
        }

        public override async Task<PersonalProjectEntity> UpdateSingle(PersonalProjectEntity obj)
        {
            string sql = $"Update {TableName} " +
                   $"set {nameof(PersonalProjectEntity.Name).ToLower()} = @{nameof(PersonalProjectEntity.Name)}, " +
                       $"{nameof(PersonalProjectEntity.Details).ToLower()} = @{nameof(PersonalProjectEntity.Details)}, " +
                       $"{nameof(PersonalProjectEntity.Environment).ToLower()} = @{nameof(PersonalProjectEntity.Environment)}, " +
                       $"{nameof(PersonalProjectEntity.Date).ToLower()} = @{nameof(PersonalProjectEntity.Date)} " +
                       $"Where {nameof(PersonalProjectEntity.Id).ToLower()} = @{nameof(PersonalProjectEntity.Id)}";

            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            int filasAfectadas = await connection.ExecuteAsync(sql, new
            {
                obj.UserId,
                obj.Name,
                obj.Details,
                obj.Environment,
                obj.Date,
                obj.Id
            });
            return obj;
        }
    }
}
