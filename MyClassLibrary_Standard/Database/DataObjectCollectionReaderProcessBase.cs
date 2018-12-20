using System;


namespace MyClassLibrary.Database
{
    public abstract class DataObjectCollectionReaderProcessBase<T_DataObject, T_DataObjectCollection, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>
        : ReaderProcessBase<T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>
        where T_DataObjectCollection : System.Collections.Generic.ICollection<T_DataObject>, new()
        where T_DataParameter : System.Data.IDataParameter
        where T_DataReader : System.Data.IDataReader
        where T_DbCommand : System.Data.IDbCommand, new()
        where T_DbConnection : System.Data.IDbConnection, new()
        where T_DbDataAdapter : System.Data.IDbDataAdapter, new()
        where T_DbTransaction : System.Data.IDbTransaction
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


        //METHODS
        public override bool ExecuteProcess()
        {
            DataObjectCollection = new T_DataObjectCollection();

            return base.ExecuteProcess();
        }


        //FUNCTIONS
        protected virtual void AddDataObjectToDataObjectCollection(T_DataObject dataObject)
        {
            DataObjectCollection.Add(dataObject);
        }

        protected abstract T_DataObject CreateDataObject(T_DataReader dataReader);
        
        protected override void ProcessRecord(T_DataReader dataReader)
        {
            T_DataObject dataObject = CreateDataObject(dataReader);

            AddDataObjectToDataObjectCollection(dataObject);
        }
    }
}