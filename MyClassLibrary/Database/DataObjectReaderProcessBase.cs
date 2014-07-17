using System;


namespace MyClassLibrary.Database
{
    public abstract class DataObjectReaderProcessBase<T_DataObject, T_DatabaseClient, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>
        : ReaderProcessBase<T_DatabaseClient, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction> 
        where T_DatabaseClient : Database.DatabaseClient<T_DataParameter, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>, new()
        where T_DataParameter : System.Data.IDataParameter
        where T_DataReader : System.Data.IDataReader
        where T_DbCommand : System.Data.IDbCommand, new()
        where T_DbConnection : System.Data.IDbConnection, new()
        where T_DbDataAdapter : System.Data.IDbDataAdapter, new()
        where T_DbTransaction : System.Data.IDbTransaction
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


        //METHODS
        public override bool ExecuteProcess()
        {
            DataObject = default(T_DataObject);

            return base.ExecuteProcess();
        }


        //FUNCTIONS
        protected abstract T_DataObject CreateDataObject(T_DataReader dataReader);
        
        protected override void ProcessRecord(T_DataReader dataReader)
        {
            DataObject = CreateDataObject(dataReader);
        }
    }
}