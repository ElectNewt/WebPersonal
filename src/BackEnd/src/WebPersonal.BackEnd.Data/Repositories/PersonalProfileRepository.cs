using Dapper;
using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
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


        public async Task<PersonalProfileEntity> Insertar(PersonalProfileEntity perfilPersonal)
        {
            string sql = $"insert into {TableName} ({nameof(PersonalProfileEntity.UserId)}, {nameof(PersonalProfileEntity.FirstName)}, " +
                    $"{nameof(PersonalProfileEntity.LastName)}, {nameof(PersonalProfileEntity.Description)}, {nameof(PersonalProfileEntity.Phone)}," +
                    $"{nameof(PersonalProfileEntity.Email)}, {nameof(PersonalProfileEntity.Website)}, {nameof(PersonalProfileEntity.GitHub)}) " +
                    $"values (@{nameof(PersonalProfileEntity.UserId)}, @{nameof(PersonalProfileEntity.FirstName)}, @{nameof(PersonalProfileEntity.LastName)}," +
                    $"@{nameof(PersonalProfileEntity.Description)}, @{nameof(PersonalProfileEntity.Phone)}, @{nameof(PersonalProfileEntity.Email)}," +
                    $"@{nameof(PersonalProfileEntity.Website)}, @{nameof(PersonalProfileEntity.GitHub)} );" +
                    $"Select CAST(SCOPE_IDENTITY() as int);";
            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            var newId = (await connection.QueryAsync<int>(sql, perfilPersonal)).First();
            return PersonalProfileEntity.UpdateId(perfilPersonal, newId);
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

            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            int filasAfectadas = await connection.ExecuteAsync(sql, perfilPersonal);
            return perfilPersonal;
        }

        public async Task<int> Delete(int id)
        {
            string sql = $"delete from {TableName} Where {nameof(PersonalProfileEntity.Id)} = @id";

            DbConnection connection = await _conexionWrapper.GetConnectionAsync();
            await connection.ExecuteAsync(sql, new { id = id });
            return id;
        }


    }
}
