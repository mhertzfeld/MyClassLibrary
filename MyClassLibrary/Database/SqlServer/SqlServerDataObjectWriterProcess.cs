using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public abstract class SqlServerDataObjectWriterProcess<T_DataObject>
        : Database.DataObjectWriterProcess<T_DataObject, SqlParameter, SqlCommand, SqlConnection, SqlTransaction>
        where T_DataObject : DataObjectWriterInterface
    {

    }
}