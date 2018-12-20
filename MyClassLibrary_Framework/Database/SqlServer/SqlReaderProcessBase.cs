using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public abstract class SqlReaderProcessBase
        : ReaderProcessBase<SqlParameter, SqlDataReader, SqlCommand, SqlConnection, SqlDataAdapter, SqlTransaction>
    {
    }
}