using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace WebPersonal.BackEnd.Model.Repositories
{
    public abstract class BaseRepository<T>
        where T : class
    {

        public abstract string TableName { get; }
        public abstract T? Create(DbDataReader reader);
        //Temporal Until I decide if i go with dapper or EF o que 
        protected string ConnectionString = "Server=127.0.0.1;Port=3306;Database=webpersonal;Uid=webpersonaluser;password=webpersonalpass;";

        public async Task<T?> GetByUserId(int userId)
        {
            using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandText = $"select * from {TableName} where UserId = ?userId";
                cmd.Parameters.Add("?userId", MySqlDbType.Int32).Value = userId;
                T? result = null;
                DbDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (await reader.ReadAsync())
                {
                    result = Create(reader);
                }
                return result;
            }
        }

        public async Task<List<T>> GetListByUserId(int userId)
        {
            using (MySqlConnection conexion = new MySqlConnection(ConnectionString))
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandText = $"select * from {TableName} where UserId = ?userId";
                cmd.Parameters.Add("?userId", MySqlDbType.Int32).Value = userId;
                List<T> result = new List<T>();
                DbDataReader reader = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection);

                while (await reader.ReadAsync())
                {
                    T? conversion = Create(reader);
                    if (conversion != null)
                        result.Add(conversion);
                }
                return result;
            }

        }
    }
}
