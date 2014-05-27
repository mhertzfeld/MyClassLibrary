using System;


namespace MyClassLibrary.Database
{
    public abstract class DataObjectCollectionReaderProcessBase<T_DatabaseClient, T_DataObject, T_DataObjectCollection, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction, T_LogWriter>
        : ReaderProcessBase<T_DatabaseClient, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction, T_LogWriter>
        where T_DatabaseClient : Database.DatabaseClient<T_DataParameter, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction, T_LogWriter>, new()
        //where T_DataObject : new()
        where T_DataObjectCollection : System.Collections.Generic.ICollection<T_DataObject>, new()
        where T_DataParameter : System.Data.IDataParameter
        where T_DataReader : System.Data.IDataReader
        where T_DbCommand : System.Data.IDbCommand, new()
        where T_DbConnection : System.Data.IDbConnection, new()
        where T_DbDataAdapter : System.Data.IDbDataAdapter, new()
        where T_DbTransaction : System.Data.IDbTransaction
        where T_LogWriter : Logging.ILogWriter, new()
    {
        //FIELDS
        protected T_DataObjectCollection dataObjectCollection;


        //PROPERTIES
        public virtual T_DataObjectCollection DataObjectCollection
        {
            get { return dataObjectCollection; }

            protected set { dataObjectCollection = value; }
        }


        //INTITIALIZE
        public DataObjectCollectionReaderProcessBase()
        {
            dataObjectCollection = default(T_DataObjectCollection);
        }


        //FUNCTIONS
        protected virtual void AddDataObjectToDataObjectCollection(T_DataObject dataObject)
        {
            DataObjectCollection.Add(dataObject);
        }

        protected abstract T_DataObject CreateDataObject(T_DataReader dataReader);

        protected override bool ExecuteDataReaderCommand(T_DbCommand dbCommand)
        {
            DataObjectCollection = new T_DataObjectCollection();

            return base.ExecuteDataReaderCommand(dbCommand);
        }

        protected override void ProcessRecord(T_DataReader dataReader)
        {
            T_DataObject dataObject = CreateDataObject(dataReader);

            AddDataObjectToDataObjectCollection(dataObject);
        }

        protected override void ResetProcess()
        {
            base.ResetProcess();

            DataObjectCollection = default(T_DataObjectCollection);
        }
    }
}