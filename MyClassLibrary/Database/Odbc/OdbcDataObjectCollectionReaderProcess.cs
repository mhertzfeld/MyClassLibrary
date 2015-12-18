using System;
using System.Data.Odbc;


namespace MyClassLibrary.Database.Odbc
{
    public class OdbcDataObjectCollectionReaderProcess<T_DataObject, T_DataObjectCollection>
        : Database.DataObjectCollectionReaderProcess<T_DataObject, OdbcDatabaseClient, T_DataObjectCollection, OdbcParameter, OdbcDataReader, OdbcCommand, OdbcConnection, OdbcDataAdapter, OdbcTransaction>
        where T_DataObject : DataObjectInterface, new()
        where T_DataObjectCollection : System.Collections.Generic.ICollection<T_DataObject>, new()
    {

    }
}