using System;
using System.Data.Odbc;


namespace MyClassLibrary.Database.Odbc
{
    public abstract class OdbcDataObjectCollectionReaderProcessBase<T_DataObject, T_DataObjectCollection>
        : Database.DataObjectCollectionReaderProcessBase<T_DataObject, T_DataObjectCollection, OdbcParameter, OdbcDataReader, OdbcCommand, OdbcConnection, OdbcDataAdapter, OdbcTransaction>
        where T_DataObjectCollection : System.Collections.Generic.ICollection<T_DataObject>, new()
    {

    }
}