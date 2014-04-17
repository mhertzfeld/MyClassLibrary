using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public abstract class SqlReaderProcessBase<T_LogWriter>
        : ReaderProcessBase<SqlServerDatabaseClient<T_LogWriter>, SqlParameter, SqlDataReader, SqlCommand, SqlConnection, SqlDataAdapter, SqlTransaction, T_LogWriter>
        where T_LogWriter : Logging.ILogWriter, new()
    {
    }
}