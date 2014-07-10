using System;
using System.Data.Odbc;


namespace MyClassLibrary.Database.Odbc
{
    public abstract class OdbcDataObjectWriterProcessBase<T_DataObject>
        : Database.DataObjectWriterProcessBase<T_DataObject, OdbcDatabaseClient, OdbcParameter, OdbcDataReader, OdbcCommand, OdbcConnection, OdbcDataAdapter, OdbcTransaction>
    {
    }
}