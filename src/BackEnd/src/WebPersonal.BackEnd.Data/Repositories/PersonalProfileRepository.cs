using Dapper;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;
using WebPersonal.Shared.Data.Db;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class PersonalProfileRepository : BaseRepository<PersonalProfileEntity>
    {
        public override string TableName => TableNames.PersonalProfile;

        public PersonalProfileRepository(TransactionalWrapper conexion) : base(conexion)
        {
        }


        public override async Task<PersonalProfileEntity> InsertSingle(PersonalProfileEntity obj)
        {
            string sql = $"insert into {TableName} ({nameof(PersonalProfileEntity.UserId).ToLower()}, {nameof(PersonalProfileEntity.FirstName).ToLower()}, " +
                    $"{nameof(PersonalProfileEntity.LastName).ToLower()}, {nameof(PersonalProfileEntity.Description)}, {nameof(PersonalProfileEntity.Phone).ToLower()}," +
                    $"{nameof(PersonalProfileEntity.Email).ToLower()}, {nameof(PersonalProfileEntity.Website).ToLower()}, {nameof(PersonalProfileEntity.GitHub).ToLower()}) " +
                    $"values (@{nameof(PersonalProfileEntity.UserId)}, @{nameof(PersonalProfileEntity.FirstName)}, @{nameof(PersonalProfileEntity.LastName)}," +
                    $"@{nameof(PersonalProfileEntity.Description)}, @{nameof(PersonalProfileEntity.Phone)}, @{nameof(PersonalProfileEntity.Email)}," +
                    $"@{nameof(PersonalProfileEntity.Website)}, @{nameof(PersonalProfileEntity.GitHub)} );" +
                    $"SELECT LAST_INSERT_ID();";
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            var newId = (await connection.QueryAsync<int>(sql, new
            {
                UserId = obj.UserId,
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Description = obj.Description,
                Phone = obj.Phone,
                Email = obj.Email,
                Website = obj.Website,
                GitHub = obj.GitHub
            })).First();
            return PersonalProfileEntity.UpdateId(obj, newId);
        }

        public override async Task<PersonalProfileEntity> UpdateSingle(PersonalProfileEntity obj)
        {
            string sql = $"Update {TableName} " +
                $"set {nameof(PersonalProfileEntity.FirstName).ToLower()} = @{nameof(PersonalProfileEntity.FirstName)}, " +
                    $"{nameof(PersonalProfileEntity.LastName).ToLower()} = @{nameof(PersonalProfileEntity.LastName)}, " +
                    $"{nameof(PersonalProfileEntity.Description).ToLower()} = @{nameof(PersonalProfileEntity.Description)}, " +
                    $"{nameof(PersonalProfileEntity.Phone).ToLower()} = @{nameof(PersonalProfileEntity.Phone)}," +
                    $"{nameof(PersonalProfileEntity.Email).ToLower()} = @{nameof(PersonalProfileEntity.Email)}, " +
                    $"{nameof(PersonalProfileEntity.Website).ToLower()} = @{nameof(PersonalProfileEntity.Website)}, " +
                    $"{nameof(PersonalProfileEntity.GitHub).ToLower()} = @{nameof(PersonalProfileEntity.GitHub)} " +
                    $"Where {nameof(PersonalProfileEntity.Id).ToLower()} = @{nameof(PersonalProfileEntity.Id)}";

            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            int filasAfectadas = await connection.ExecuteAsync(sql, new
            {
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                Description = obj.Description,
                Phone = obj.Phone,
                Email = obj.Email,
                Website = obj.Website,
                GitHub = obj.GitHub,
                Id = obj.Id
            });
            return obj;
        }
    }
}
