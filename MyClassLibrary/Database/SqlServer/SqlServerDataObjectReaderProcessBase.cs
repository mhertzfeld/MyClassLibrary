using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public abstract class SqlServerDataObjectReaderProcessBase<T_DataObject, T_LogWriter>
        : Database.DataObjectReaderProcessBase<SqlServerDatabaseClient<T_LogWriter>, T_DataObject, SqlParameter, SqlDataReader, SqlCommand, SqlConnection, SqlDataAdapter, SqlTransaction, T_LogWriter>
        //where T_DataObject : new()
        where T_LogWriter : Logging.ILogWriter, new()
    {

    }
}