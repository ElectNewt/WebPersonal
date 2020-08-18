using Dapper;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class PersonalProfileRepository : BaseRepository<PersonalProfileEntity>
    {
        public override string TableName => TableNames.PersonalProfile;

        public PersonalProfileRepository(DbConnection conexion) : base(conexion)
        {
        }


        public async Task<PersonalProfileEntity> Insertar(PersonalProfileEntity perfilPersonal)
        {
            string sql = $"insert into {TableName} ({nameof(PersonalProfileEntity.UserId)}, {nameof(PersonalProfileEntity.FirstName)}, " +
                    $"{nameof(PersonalProfileEntity.LastName)}, {nameof(PersonalProfileEntity.Description)}, {nameof(PersonalProfileEntity.Phone)}," +
                    $"{nameof(PersonalProfileEntity.Email)}, {nameof(PersonalProfileEntity.Website)}, {nameof(PersonalProfileEntity.GitHub)}) " +
                    $"values (@{nameof(PersonalProfileEntity.UserId)}, @{nameof(PersonalProfileEntity.FirstName)}, @{nameof(PersonalProfileEntity.LastName)}," +
                    $"@{nameof(PersonalProfileEntity.Description)}, @{nameof(PersonalProfileEntity.Phone)}, @{nameof(PersonalProfileEntity.Email)}," +
                    $"@{nameof(PersonalProfileEntity.Website)}, @{nameof(PersonalProfileEntity.GitHub)} );" +
                    $"Select CAST(SCOPE_IDENTITY() as int);";
            using (var con = _conexion)
            {
                con.Open();
                var newId = (await con.QueryAsync<int>(sql, perfilPersonal)).First();
                return PersonalProfileEntity.UpdateId(perfilPersonal, newId);
            }
        }

        public async Task<PersonalProfileEntity> Update(PersonalProfileEntity perfilPersonal)
        {
            string sql = $"Update {TableName} " +
                $"set {nameof(PersonalProfileEntity.FirstName)} = @{nameof(PersonalProfileEntity.FirstName)}, " +
                    $"{nameof(PersonalProfileEntity.LastName)} = @{nameof(PersonalProfileEntity.LastName)}, " +
                    $"{nameof(PersonalProfileEntity.Description)} = @{nameof(PersonalProfileEntity.Description)}, " +
                    $"{nameof(PersonalProfileEntity.Phone)} = @{nameof(PersonalProfileEntity.Phone)}," +
                    $"{nameof(PersonalProfileEntity.Email)} = @{nameof(PersonalProfileEntity.Email)}, " +
                    $"{nameof(PersonalProfileEntity.Website)} = @{nameof(PersonalProfileEntity.Website)}, " +
                    $"{nameof(PersonalProfileEntity.GitHub)} = @{nameof(PersonalProfileEntity.GitHub)}" +
                    $"Where {nameof(PersonalProfileEntity.Id)} = @{nameof(PersonalProfileEntity.UserId)}";

            using (var con = _conexion)
            {
                con.Open();
                int filasAfectadas = await con.ExecuteAsync(sql, perfilPersonal);
                return perfilPersonal;
            }
        }

        public async Task<int> Delete(int id)
        {
            string sql = $"delete from {TableName} Where {nameof(PersonalProfileEntity.Id)} = @id";

            using (var con = _conexion)
            {
                con.Open();
                await con.ExecuteAsync(sql, new { id = id });
                return id;
            }
        }


    }
}
