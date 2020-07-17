using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using WebPersonal.BackEnd.Model.Entity;
using WebPersonal.BackEnd.Model.Repositories.Queries;
#nullable enable

namespace WebPersonal.BackEnd.Model.Repositories
{
    public class UserIdRepository : BaseRepository<UserIdEntity>
    {
        public override string TableName => TableNames.UserId;

        public override UserIdEntity Create(DbDataReader reader)
        {
            return UserIdEntity.Create(reader[nameof(UserIdEntity.UserName)].ToString() ?? "",
               Convert.ToInt32(reader[nameof(PersonalProfileEntity.UserId)]));
        }


        public async Task<UserIdEntity?> GetByUserName(string userName)
        {
            using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandText = $"select * from {TableName} where UserName = ?UserName";
                cmd.Parameters.Add("?UserName", MySqlDbType.VarChar).Value = userName;
                UserIdEntity? result = null;
                DbDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (await reader.ReadAsync())
                {
                    result = Create(reader);
                }
                return result;
            }
        }
    }
}
