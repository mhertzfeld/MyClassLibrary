using System;
using System.Data.Odbc;


namespace MyClassLibrary.Database.Odbc
{
    public abstract class OdbcDataObjectDictionaryReaderProcessBase<T_Key, T_DataObject, T_DataObjectDictionary>
        : Database.DataObjectDictionaryReaderProcessBase<T_Key, T_DataObject, T_DataObjectDictionary, OdbcDatabaseClient, OdbcParameter, OdbcDataReader, OdbcCommand, OdbcConnection, OdbcDataAdapter, OdbcTransaction>
        where T_DataObjectDictionary : System.Collections.Generic.IDictionary<T_Key, T_DataObject>, new()

    {

    }
}