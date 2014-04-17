using System;


namespace MyClassLibrary.Database
{
    public abstract class DataObjectReaderProcessBase<T_DatabaseClient, T_DataObject, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction, T_LogWriter>
        : ReaderProcessBase<T_DatabaseClient, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction, T_LogWriter> 
        where T_DatabaseClient : Database.DatabaseClient<T_DataParameter, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction, T_LogWriter>, new()
        where T_DataObject : new()
        where T_DataParameter : System.Data.IDataParameter
        where T_DataReader : System.Data.IDataReader
        where T_DbCommand : System.Data.IDbCommand, new()
        where T_DbConnection : System.Data.IDbConnection, new()
        where T_DbDataAdapter : System.Data.IDbDataAdapter, new()
        where T_DbTransaction : System.Data.IDbTransaction
        where T_LogWriter : Logging.ILogWriter, new()
    {
        //FIELDS
        protected T_DataObject dataObject;


        //PROPERTIES
        public virtual T_DataObject DataObject
        {
            get { return dataObject; }

            protected set { dataObject = value; }
        }


        //INITIALIZE
        public DataObjectReaderProcessBase()
        {
            dataObject = default(T_DataObject);
        }


        //FUNCTIONS
        protected abstract T_DataObject CreateDataObject(T_DataReader dataReader);

        protected override void ProcessRecord(T_DataReader dataReader)
        {
            DataObject = CreateDataObject(dataReader);
        }
    }
}