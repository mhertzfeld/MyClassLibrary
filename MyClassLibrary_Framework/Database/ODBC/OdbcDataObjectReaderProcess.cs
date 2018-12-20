using System;
using System.Data.Odbc;


namespace MyClassLibrary.Database.Odbc
{
    public class OdbcDataObjectReaderProcess<T_DataObject>
        : DataObjectReaderProcess<T_DataObject, OdbcParameter, OdbcDataReader, OdbcCommand, OdbcConnection, OdbcDataAdapter, OdbcTransaction>
        where T_DataObject : DataObjectReaderInterface, new()
    {

    }
}