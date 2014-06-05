using System;
using System.Data.Odbc;


namespace MyClassLibrary.Database.Odbc
{
    public abstract class OdbcDataObjectReaderProcessBase<T_DataObject, T_LogWriter>
        : Database.DataObjectReaderProcessBase<OdbcDatabaseClient<T_LogWriter>, T_DataObject, OdbcParameter, OdbcDataReader, OdbcCommand, OdbcConnection, OdbcDataAdapter, OdbcTransaction, T_LogWriter>
        //where T_DataObject : new()
        where T_LogWriter : Logging.ILogWriter, new()
    {

    }
}