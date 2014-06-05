using System;
using System.Data.Odbc;


namespace MyClassLibrary.Database.Odbc
{
    public abstract class OdbcDataObjectCollectionReaderProcessBase<T_DataObject, T_DataObjectCollection, T_LogWriter>
        : Database.DataObjectCollectionReaderProcessBase<OdbcDatabaseClient<T_LogWriter>, T_DataObject, T_DataObjectCollection, OdbcParameter, OdbcDataReader, OdbcCommand, OdbcConnection, OdbcDataAdapter, OdbcTransaction, T_LogWriter>
        //where T_DataObject : new()
        where T_DataObjectCollection : System.Collections.Generic.ICollection<T_DataObject>, new()
        where T_LogWriter : Logging.ILogWriter, new()
    {

    }
}