using System;
using System.Data.Odbc;


namespace MyClassLibrary.Database.Odbc
{
    public abstract class OdbcReaderProcessBase<T_LogWriter>
        : ReaderProcessBase<OdbcDatabaseClient<T_LogWriter>, OdbcParameter, OdbcDataReader, OdbcCommand, OdbcConnection, OdbcDataAdapter, OdbcTransaction, T_LogWriter>
        where T_LogWriter : Logging.ILogWriter, new()
    {
    }
}