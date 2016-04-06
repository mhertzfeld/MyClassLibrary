using System;
using System.Data.Odbc;


namespace MyClassLibrary.Database.Odbc
{
    public abstract class OdbcDataObjectReaderProcessBase<T_DataObject>
        : Database.DataObjectReaderProcessBase<T_DataObject, OdbcParameter, OdbcDataReader, OdbcCommand, OdbcConnection, OdbcDataAdapter, OdbcTransaction>
    {

    }
}