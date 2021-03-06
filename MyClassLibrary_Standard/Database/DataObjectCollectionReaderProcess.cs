﻿using System;


namespace MyClassLibrary.Database
{
    public class DataObjectCollectionReaderProcess<T_DataObject, T_DataObjectCollection, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>
        : DataObjectCollectionReaderProcessBase<T_DataObject, T_DataObjectCollection, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>
        where T_DataObject : DataObjectReaderInterface, new()
        where T_DataObjectCollection : System.Collections.Generic.ICollection<T_DataObject>, new()
        where T_DataParameter : System.Data.IDataParameter
        where T_DataReader : System.Data.IDataReader
        where T_DbCommand : System.Data.IDbCommand, new()
        where T_DbConnection : System.Data.IDbConnection, new()
        where T_DbDataAdapter : System.Data.IDbDataAdapter, new()
        where T_DbTransaction : System.Data.IDbTransaction
    {
        protected override T_DataObject CreateDataObject(T_DataReader dataReader)
        {
            T_DataObject _DataObject = new T_DataObject();
            _DataObject.SetFields(dataReader);

            return _DataObject;
        }
    }
}