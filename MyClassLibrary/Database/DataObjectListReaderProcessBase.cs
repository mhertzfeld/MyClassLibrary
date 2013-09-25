using System;
using System.Collections.Generic;
using System.Data;


namespace MyClassLibrary.Database
{
    public abstract class DataObjectListReaderProcessBase<T_Database, T_DataObject, T_DataObjectList, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction> 
        : DataObjectReaderProcessBase<T_Database, T_DataObject, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>
        where T_Database : DatabaseClient<T_DataParameter, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>, new()
        where T_DataObject : IDataObject, new()
        where T_DataObjectList : Data.IDataObjectList<T_DataObject>, new()
        where T_DataParameter : IDataParameter
        where T_DataReader : IDataReader
        where T_DbCommand : IDbCommand, new()
        where T_DbConnection : IDbConnection, new()
        where T_DbDataAdapter : IDbDataAdapter, new()
        where T_DbTransaction : IDbTransaction
    {
        //FIELDS
        protected T_DataObjectList dataObjectList;


        //PUBLIC PROPERTIES
        public virtual T_DataObjectList DataObjectList
        {
            get { return dataObjectList; }
        }


        //INITIALIZE
        public DataObjectListReaderProcessBase()
        {
            dataObjectList = default(T_DataObjectList);
        }


        //FINALIZE
        protected override void Dispose(bool disposeManagedResources)
        {
            if (!disposed)
            {
                if (disposeManagedResources)
                {
                    dataObjectList = default(T_DataObjectList);
                }
            }

            base.Dispose(disposeManagedResources);
        }


        //METHODS
        public override bool ProcessExecution()
        {
            return base.ProcessExecution();
        }


        //FUNCTIONS
        protected override void AddDataObject(T_DataObject dataObject)
        {
            dataObjectList.Add(dataObject);
        }

        protected override void ResetProcess()
        {
            base.ResetProcess();

            dataObjectList = default(T_DataObjectList);
            dataObjectList = new T_DataObjectList();
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.