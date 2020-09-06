using Dapper;
using System;
using System.Collections.Generic;
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


        public override async Task<PersonalProfileEntity> InsertSingle(PersonalProfileEntity perfilPersonal)
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

        public override async Task<PersonalProfileEntity> UpdateSingle(PersonalProfileEntity perfilPersonal)
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
            int filasAfectadas = await connection.ExecuteAsync(sql, perfilPersonal);
            return perfilPersonal;
        }

        

        
    }
}
