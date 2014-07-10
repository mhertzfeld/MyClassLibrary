using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public abstract class SqlServerDataObjectCollectionReaderProcessBase<T_DataObject, T_DataObjectCollection>
        : Database.DataObjectCollectionReaderProcessBase<T_DataObject, SqlServerDatabaseClient, T_DataObjectCollection, SqlParameter, SqlDataReader, SqlCommand, SqlConnection, SqlDataAdapter, SqlTransaction>
        where T_DataObjectCollection : System.Collections.Generic.ICollection<T_DataObject>, new()
    {

    }
}