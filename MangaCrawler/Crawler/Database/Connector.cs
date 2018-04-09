using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangaCrawler.Crawler.Database
{
    class Connector
    {
        private const string SQL_User = "mes";
        private const string SQL_Password = "qweqwe";
        private const string SQL_Host = "localhost";
        private const string SQL_Database = "crawler";
        private const int SQL_Port = 3600;
        
        public MySqlConnection GetConnection()
        {
            var connBuilder = new MySqlConnectionStringBuilder();

            connBuilder.Server = SQL_Host;
            connBuilder.UserID = SQL_User;
            connBuilder.Password = SQL_Password;
            connBuilder.Database = SQL_Database;
            connBuilder.Port = SQL_Port;
            connBuilder.Pooling = true;

            return new MySqlConnection(connBuilder.GetConnectionString(true));
        }
    }
}
