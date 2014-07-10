using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public abstract class SqlReaderProcessBase
        : ReaderProcessBase<SqlServerDatabaseClient, SqlParameter, SqlDataReader, SqlCommand, SqlConnection, SqlDataAdapter, SqlTransaction>
    {
    }
}