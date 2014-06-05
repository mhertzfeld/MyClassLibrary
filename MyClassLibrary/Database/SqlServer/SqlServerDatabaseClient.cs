using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public class SqlServerDatabaseClient<T_LogWriter>
        : Database.DatabaseClient<SqlParameter, SqlCommand, SqlConnection, SqlDataAdapter, SqlTransaction, T_LogWriter>
        where T_LogWriter : Logging.ILogWriter, new()
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