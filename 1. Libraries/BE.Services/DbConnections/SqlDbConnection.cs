using System;
using System.Configuration;
using System.Data.SqlClient;

namespace BE.Services.DbConnections
{
    public class SqlDbConnection
    {
        public SqlConnection _sqlConn;
        private static SqlDbConnection _ConsString = null;
        private String _String = null;

        public SqlDbConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = ConfigurationManager.AppSettings["DataSource"];
            builder.InitialCatalog = ConfigurationManager.AppSettings["Database"];
            builder.UserID = ConfigurationManager.AppSettings["UID"];
            builder.Password = ConfigurationManager.AppSettings["Password"];
            // For Normal Sql Connection
            _sqlConn = new SqlConnection(builder.ConnectionString);
        }

        // For Entity FreamWrok Db Context
        public static string ConString
        {
            get
            {
                if (_ConsString == null)
                {
                    _ConsString = new SqlDbConnection { _String = SqlDbConnection.Connect() };
                    return _ConsString._String;
                }
                else
                    return _ConsString._String;
            }
        }

        public static string Connect()
        {
            //string conString = ConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
            string conString = string.Empty;
            string DataSource = ConfigurationManager.AppSettings["DataSource"];
            string Database = ConfigurationManager.AppSettings["Database"];
            string UID = ConfigurationManager.AppSettings["UID"];
            string Password = ConfigurationManager.AppSettings["Password"];
            conString = "Server=" + DataSource + "; Database=" + Database + "; UID=" + UID + "; PWD=" + Password + ";";

            if (conString.ToLower().StartsWith("metadata="))
            {
                System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder efBuilder = new System.Data.Entity.Core.EntityClient.EntityConnectionStringBuilder(conString);
                conString = efBuilder.ProviderConnectionString;
            }

            SqlConnectionStringBuilder cns = new SqlConnectionStringBuilder(conString);
            string dataSource = cns.DataSource;

            SqlConnectionStringBuilder sqlString = new SqlConnectionStringBuilder()
            {
                DataSource = cns.DataSource, // Server name
                InitialCatalog = cns.InitialCatalog,  //Database
                UserID = cns.UserID,         //Username
                Password = cns.Password,  //Password,
                //MultipleActiveResultSets = true,
                //ApplicationName = "EntityFramework",
            };
            return sqlString.ConnectionString;
            //Build an Entity Framework connection string
            //EntityConnectionStringBuilder entityString = new EntityConnectionStringBuilder()
            //{
            //    //Provider = "System.Data.SqlClient",
            //    //Metadata = @"..\..\bin\Debug\EntityModelDemo.csdl|..\..\bin\Debug\EntityModelDemo.ssdl|..\..\bin\Debug\EntityModelDemo.msl;",
            //    //ProviderConnectionString = sqlString.ToString()
            //};
            //return entityString.ConnectionString;
        }
    }
}
