using System;
using System.Data.Odbc;


namespace MyClassLibrary.Database.Odbc
{
    public class OdbcDataObjectWriterProcess<T_DataObject>
        : Database.DataObjectWriterProcess<T_DataObject, OdbcParameter, OdbcCommand, OdbcConnection, OdbcTransaction>
        where T_DataObject : DataObjectWriterInterface
    {
    }
}