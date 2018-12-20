using System;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public abstract class SqlServerDataObjectWriterProcessBase<T_DataObject>
        : Database.DataObjectWriterProcessBase<T_DataObject, SqlParameter, SqlCommand, SqlConnection, SqlTransaction>
    {

    }
}