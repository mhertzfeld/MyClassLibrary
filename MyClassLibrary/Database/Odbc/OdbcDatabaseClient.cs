using System;
using System.Data.Odbc;


namespace MyClassLibrary.Database.Odbc
{
    public class OdbcDatabaseClient
        : Database.DatabaseClient<OdbcParameter, OdbcCommand, OdbcConnection, OdbcDataAdapter, OdbcTransaction>
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