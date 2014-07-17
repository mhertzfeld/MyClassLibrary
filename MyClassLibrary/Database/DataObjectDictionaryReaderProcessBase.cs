using System;


namespace MyClassLibrary.Database
{
    public abstract class DataObjectDictionaryReaderProcessBase<T_Key, T_DataObject, T_DataObjectDictionary, T_Database, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>
        : Database.ReaderProcessBase<T_Database, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>
        where T_Database : Database.DatabaseClient<T_DataParameter, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>, new()
        where T_DataObjectDictionary : System.Collections.Generic.IDictionary<T_Key, T_DataObject>, new()
        where T_DataParameter : System.Data.IDataParameter
        where T_DataReader : System.Data.IDataReader
        where T_DbCommand : System.Data.IDbCommand, new()
        where T_DbConnection : System.Data.IDbConnection, new()
        where T_DbDataAdapter : System.Data.IDbDataAdapter, new()
        where T_DbTransaction : System.Data.IDbTransaction
    {
        //FIELDS
        protected T_DataObjectDictionary dataObjectDictionary;


        //PROPERTIES
        public virtual T_DataObjectDictionary DataObjectDictionary
        {
            get { return dataObjectDictionary; }

            protected set { dataObjectDictionary = value; }
        }


        //INITIALIZE
        public DataObjectDictionaryReaderProcessBase()
        {
            dataObjectDictionary = default(T_DataObjectDictionary);
        }


        //METHODS
        public override bool ExecuteProcess()
        {
            DataObjectDictionary = default(T_DataObjectDictionary);

            return base.ExecuteProcess();
        }


        //FUNCTIONS
        protected abstract void AddDataObjectToDataObjectDictionary(T_DataObject dataObject);

        protected abstract T_DataObject CreateDataObject(T_DataReader dataReader);

        protected override bool ExecuteDataReaderCommand(T_DbCommand dbCommand)
        {
            DataObjectDictionary = new T_DataObjectDictionary();

            return base.ExecuteDataReaderCommand(dbCommand);
        }

        protected override void ProcessRecord(T_DataReader dataReader)
        {
            AddDataObjectToDataObjectDictionary(CreateDataObject(dataReader));
        }
    }
}