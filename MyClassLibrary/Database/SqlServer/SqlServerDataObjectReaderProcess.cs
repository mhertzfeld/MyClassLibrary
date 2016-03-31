using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public class SqlServerDataObjectReaderProcess<T_DataObject>
        : Database.DataObjectReaderProcess<T_DataObject, SqlServerDatabaseClient, SqlParameter, SqlDataReader, SqlCommand, SqlConnection, SqlDataAdapter, SqlTransaction>
        where T_DataObject : DataObjectInterface, new()
    {

    }
}