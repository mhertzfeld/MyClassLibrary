using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public abstract class SqlServerDataObjectWriterProcessBase<T_DataObject, T_LogWriter>
        : Database.DataObjectWriterProcessBase<SqlServerDatabaseClient<T_LogWriter>, T_DataObject, SqlParameter, SqlDataReader, SqlCommand, SqlConnection, SqlDataAdapter, SqlTransaction, T_LogWriter>
        where T_LogWriter : Logging.ILogWriter, new()
    {

    }
}