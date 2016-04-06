using System;
using System.Data;


namespace MyClassLibrary.Database
{
    public class DataObjectWriterProcess<T_DataObject, T_DataParameter, T_DbCommand, T_DbConnection, T_DbTransaction>
        : DataObjectWriterProcessBase<T_DataObject, T_DataParameter, T_DbCommand, T_DbConnection, T_DbTransaction>
        where T_DataObject : DataObjectWriterInterface
        where T_DataParameter : System.Data.IDataParameter
        where T_DbCommand : System.Data.IDbCommand, new()
        where T_DbConnection : System.Data.IDbConnection, new()
        where T_DbTransaction : System.Data.IDbTransaction
    {
        protected override T_DbCommand CreateWriteSqlCommand(T_DbConnection dbConnection, T_DataObject dataObject)
        {
            return dataObject.CreateDbCommand<T_DbCommand>(dbConnection);
        }
    }
}