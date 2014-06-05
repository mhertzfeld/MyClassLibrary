using System;
using System.Data.Odbc;


namespace MyClassLibrary.Database.Odbc
{
    public abstract class OdbcDataObjectWriterProcessBase<T_DataObject, T_LogWriter>
        : Database.DataObjectWriterProcessBase<OdbcDatabaseClient<T_LogWriter>, T_DataObject, OdbcParameter, OdbcDataReader, OdbcCommand, OdbcConnection, OdbcDataAdapter, OdbcTransaction, T_LogWriter>
        where T_LogWriter : Logging.ILogWriter, new()
    {
    }
}