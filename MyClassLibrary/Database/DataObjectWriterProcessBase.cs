using MyClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace MyClassLibrary.Database
{
    public abstract class DataObjectWriterProcessBase<T_DataObject, T_DatabaseClient, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction> 
        : MyClassLibrary.Process.ProcessWorkerBase
        where T_DatabaseClient : Database.DatabaseClient<T_DataParameter, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>, new()
        where T_DataParameter : System.Data.IDataParameter
        where T_DataReader : System.Data.IDataReader
        where T_DbCommand : System.Data.IDbCommand, new()
        where T_DbConnection : System.Data.IDbConnection, new()
        where T_DbDataAdapter : System.Data.IDbDataAdapter, new()
        where T_DbTransaction : System.Data.IDbTransaction
    {
        //FIELDS
        protected IEnumerable<T_DataObject> dataObjectEnumerable;


        //PROTECTED PROPERTIES
        protected abstract String ConnectionString { get; }


        //PUBLIC PROPERTIES
        public IEnumerable<T_DataObject> DataObjectEnumerable
        {
            get { return dataObjectEnumerable; }

            set
            {
                if (value == default(IEnumerable<T_DataObject>))
                {
                    throw new PropertySetToDefaultException("DataObjectEnumerable");
                }

                dataObjectEnumerable = value;
            }
        }


        //INITIALIZE
        public DataObjectWriterProcessBase()
        {
            dataObjectEnumerable = null;
        }


        //METHODS
        public override bool ProcessExecution()
        {
            if (DataObjectEnumerable == default(IEnumerable<T_DataObject>))
            {
                throw new PropertySetToDefaultException("DataObjectEnumerable");
            }

            Boolean returnState = false;

            using (DatabaseClient<T_DataParameter, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction> databaseClient = new DatabaseClient<T_DataParameter,T_DbCommand,T_DbConnection,T_DbDataAdapter,T_DbTransaction>())
            {
                databaseClient.ConnectionString = ConnectionString;

                if (databaseClient.OpenConnection())
                {
                    if (CleanupRecords(databaseClient.DatabaseConnetion))
                    {
                        if (WriteRecords(databaseClient.DatabaseConnetion))
                        {
                            returnState = true;
                        }
                    }

                    if (!databaseClient.CloseConnection())
                    {
                        returnState = false;
                    }
                }
            }

            return returnState;
        }

        public virtual Boolean ProcessExecution(IEnumerable<T_DataObject> DataObjectEnumerable)
        {
            this.DataObjectEnumerable = DataObjectEnumerable;

            return ProcessExecution();
        }


        //FUNCTIONS
        protected virtual Boolean CleanupRecords(T_DbConnection dbConnection)
        {
            Int32 rowsAffected;

            using (T_DbCommand dbCommand = CreateCleanupSqlCommand(dbConnection))
            {
                return DatabaseClient<T_DataParameter, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>.ExecuteNonQueryDbCommand(dbCommand, out rowsAffected);
            }
        }

        protected abstract T_DbCommand CreateWriteSqlCommand(T_DbConnection dbConnection, T_DataObject dataObject);

        protected abstract T_DbCommand CreateCleanupSqlCommand(T_DbConnection dbConnection);

        protected virtual Boolean WriteRecords(T_DbConnection dbConnection)
        {
            Int32 rowsAffected;

            foreach (T_DataObject dataObject in DataObjectEnumerable)
            {
                using (T_DbCommand dbCommand = CreateWriteSqlCommand(dbConnection, dataObject))
                {
                    if (!DatabaseClient<T_DataParameter, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>.ExecuteNonQueryDbCommand(dbCommand, out rowsAffected))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.