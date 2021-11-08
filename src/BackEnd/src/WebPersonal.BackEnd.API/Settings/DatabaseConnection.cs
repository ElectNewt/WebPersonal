using Microsoft.Extensions.Configuration;
using System.Text;

namespace WebPersonal.BackEnd.API.Settings
{
    public static class Database
    {
        public static string BuildConnectionString(IConfiguration config)
        {
            DatabaseSettings dbSettings = new DatabaseSettings();
            config.Bind("database", dbSettings);
            StringBuilder sb = new StringBuilder();
            sb.Append($"Server={dbSettings.Server};");
            sb.Append($"Port={dbSettings.Port};");
            sb.Append($"Database={dbSettings.DatabaseName};");
            sb.Append($"Uid={dbSettings.User};");
            sb.Append($"password={dbSettings.Password};");

            if (dbSettings.AllowUserVariables == true)
            {
                sb.Append("Allow User Variables=True;");
            }

            return sb.ToString();
        }

       

        public class DatabaseSettings
        {
            public string Server { get; set;  }
            public int Port { get; set; }
            public string DatabaseName { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
            public bool AllowUserVariables { get; set; }
        }
    }
}
