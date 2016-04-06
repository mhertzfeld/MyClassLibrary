using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public class SqlServerDataObjectCollectionReaderProcess<T_DataObject, T_DataObjectCollection>
        : Database.DataObjectCollectionReaderProcess<T_DataObject, T_DataObjectCollection, SqlParameter, SqlDataReader, SqlCommand, SqlConnection, SqlDataAdapter, SqlTransaction>
        where T_DataObject : DataObjectReaderInterface, new()
        where T_DataObjectCollection : System.Collections.Generic.ICollection<T_DataObject>, new()
    {

    }
}