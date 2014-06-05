using System;
using System.Data.Odbc;


namespace MyClassLibrary.Database.Odbc
{
    public abstract class OdbcDataObjectDictionaryReaderProcessBase<T_DataObject, T_DataObjectDictionary, T_LogWriter, T_Key>
        : Database.DataObjectDictionaryReaderProcessBase<OdbcDatabaseClient<T_LogWriter>, T_DataObject, T_DataObjectDictionary, OdbcParameter, OdbcDataReader, OdbcCommand, OdbcConnection, OdbcDataAdapter, OdbcTransaction, T_LogWriter, T_Key>
        //where T_DataObject : new()
        where T_DataObjectDictionary : System.Collections.Generic.IDictionary<T_Key, T_DataObject>, new()
        where T_LogWriter : Logging.ILogWriter, new()

    {

    }
}