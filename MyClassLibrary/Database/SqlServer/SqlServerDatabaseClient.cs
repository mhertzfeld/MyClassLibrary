using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public class SqlServerDatabaseClient
        : Database.DatabaseClient<SqlParameter, SqlCommand, SqlConnection, SqlDataAdapter, SqlTransaction>
    {
        //INITIALIZE
        public SqlServerDatabaseClient()
        {
            
        }

        public SqlServerDatabaseClient(String ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
    }
}