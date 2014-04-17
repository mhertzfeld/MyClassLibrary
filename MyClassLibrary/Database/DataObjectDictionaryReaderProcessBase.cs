using System;


namespace MyClassLibrary.Database
{
    public abstract class DataObjectDictionaryReaderProcessBase<T_Database, T_DataObject, T_DataObjectDictionary, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction, T_LogWriter, T_Key> 
        : Database.ReaderProcessBase<T_Database, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction, T_LogWriter>
        where T_Database : Database.DatabaseClient<T_DataParameter, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction, T_LogWriter>, new()
        where T_DataObject : new()
        where T_DataObjectDictionary : System.Collections.Generic.IDictionary<T_Key, T_DataObject>, new()
        where T_DataParameter : System.Data.IDataParameter
        where T_DataReader : System.Data.IDataReader
        where T_DbCommand : System.Data.IDbCommand, new()
        where T_DbConnection : System.Data.IDbConnection, new()
        where T_DbDataAdapter : System.Data.IDbDataAdapter, new()
        where T_DbTransaction : System.Data.IDbTransaction
        where T_LogWriter : Logging.ILogWriter, new()
        where T_Key : new ()
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


        //FUNCTIONS
        protected abstract void AddDataObjectToDataObjectDictionary(T_DataObject dataObject);

        protected abstract T_DataObject CreateDataObject(T_DataReader dataReader);

        protected override bool ExecuteDataReaderCommand(T_DbCommand dbCommand)
        {
            DataObjectDictionary = new T_DataObjectDictionary();

            if (base.ExecuteDataReaderCommand(dbCommand))
            { return true; }
            else
            {
                DataObjectDictionary = default(T_DataObjectDictionary);

                return false;
            }
        }

        protected override void ProcessRecord(T_DataReader dataReader)
        {
            AddDataObjectToDataObjectDictionary(CreateDataObject(dataReader));
        }

        protected override void ResetProcess()
        {
            base.ResetProcess();

            DataObjectDictionary = default(T_DataObjectDictionary);
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.