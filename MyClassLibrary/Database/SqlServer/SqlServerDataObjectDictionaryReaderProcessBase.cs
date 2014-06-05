using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public abstract class SqlServerDataObjectDictionaryReaderProcessBase<T_DataObject, T_DataObjectDictionary, T_LogWriter, T_Key>
        : Database.DataObjectDictionaryReaderProcessBase<SqlServerDatabaseClient<T_LogWriter>, T_DataObject, T_DataObjectDictionary, SqlParameter, SqlDataReader, SqlCommand, SqlConnection, SqlDataAdapter, SqlTransaction, T_LogWriter, T_Key>
        //where T_DataObject : new()
        where T_DataObjectDictionary : System.Collections.Generic.IDictionary<T_Key, T_DataObject>, new()
        where T_LogWriter : Logging.ILogWriter, new()
    {

    }
}