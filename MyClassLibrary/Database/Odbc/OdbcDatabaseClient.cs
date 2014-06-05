using System;
using System.Data.Odbc;


namespace MyClassLibrary.Database.Odbc
{
    public class OdbcDatabaseClient<T_LogWriter>
        : Database.DatabaseClient<OdbcParameter, OdbcCommand, OdbcConnection, OdbcDataAdapter, OdbcTransaction, T_LogWriter>
        where T_LogWriter : Logging.ILogWriter, new()
    {
        //INITIALIZE
        public OdbcDatabaseClient()
        {

        }

        public OdbcDatabaseClient(String ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }
    }
}