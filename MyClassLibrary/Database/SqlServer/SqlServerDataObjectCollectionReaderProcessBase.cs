using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public abstract class SqlServerDataObjectCollectionReaderProcessBase<T_DataObject, T_DataObjectCollection, T_LogWriter>
        : Database.DataObjectCollectionReaderProcessBase<SqlServerDatabaseClient<T_LogWriter>, T_DataObject, T_DataObjectCollection, SqlParameter, SqlDataReader, SqlCommand, SqlConnection, SqlDataAdapter, SqlTransaction, T_LogWriter>
        //where T_DataObject : new()
        where T_DataObjectCollection : System.Collections.Generic.ICollection<T_DataObject>, new()
        where T_LogWriter : Logging.ILogWriter, new ()
    {

    }
}