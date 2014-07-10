using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public abstract class SqlServerDataObjectDictionaryReaderProcessBase<T_Key, T_DataObject, T_DataObjectDictionary>
        : Database.DataObjectDictionaryReaderProcessBase<T_Key, T_DataObject, T_DataObjectDictionary, SqlServerDatabaseClient, SqlParameter, SqlDataReader, SqlCommand, SqlConnection, SqlDataAdapter, SqlTransaction>
        where T_DataObjectDictionary : System.Collections.Generic.IDictionary<T_Key, T_DataObject>, new()
    {

    }
}