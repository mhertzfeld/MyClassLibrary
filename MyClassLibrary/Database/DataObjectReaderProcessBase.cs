using MyClassLibrary.Logging;
using MyClassLibrary.Process;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;


namespace MyClassLibrary.Database
{
    public abstract class DataObjectReaderProcessBase<T_DatabaseClient, T_DataObject, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction> 
        : ProcessWorkerBase
        where T_DatabaseClient : DatabaseClient<T_DataParameter, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>, new()
        where T_DataObject : IDataObject, new()
        where T_DataParameter : IDataParameter
        where T_DataReader : IDataReader
        where T_DbCommand : IDbCommand, new()
        where T_DbConnection : IDbConnection, new()
        where T_DbDataAdapter : IDbDataAdapter, new()
        where T_DbTransaction : IDbTransaction
    {
        //PROTECTED PROPERTIES
        protected abstract Int32 CommandTimeout
        {
            get;
        }

        protected abstract String ConnectionString
        {
            get;
        }

        protected virtual CommandType DatabaseCommandType
        {
            get { return CommandType.Text; }
        }

        protected virtual T_DataParameter[] DataParameterArray
        {
            get { return null; }
        }

        protected virtual IsolationLevel IsolationLevel
        {
            get { return IsolationLevel.ReadUncommitted; }
        }

        protected abstract String SqlCommandText
        {
            get;
        }


        //INITIALIZE
        public DataObjectReaderProcessBase()
        {
            
        }


        //METHODS
        public override Boolean ProcessExecution()
        {
            Boolean returnState = false;

            ResetProcess();

            using (T_DatabaseClient databaseClient = new T_DatabaseClient())
            {
                databaseClient.ConnectionString = ConnectionString;
                databaseClient.IsolationLevel = IsolationLevel;

                if (databaseClient.OpenConnection())
                {
                    if (ExecutePreDataReaderCommand(databaseClient))
                    {
                        using (T_DbCommand dbCommand = CreateDbCommand(databaseClient))
                        {
                            returnState = ExecuteDataReaderCommand(dbCommand);
                        }

                        if (!ExecutePostDataReaderCommand(databaseClient))
                        {
                            returnState = false;
                        }
                    }

                    if (!databaseClient.CloseConnection())
                    {
                        returnState = false;
                    }
                }
            }

            error = !returnState;

            completed = true;

            return returnState;
        }


        //FUNCTIONS
        protected virtual void AddDataObject(T_DataObject dataObject)
        {
            throw new NotImplementedException();
        }

        protected virtual T_DataObject CreateDataObject(T_DataReader dataReader)
        {
            T_DataObject dataObject = new T_DataObject();
            dataObject.SetDataObjectFieldsAndProperties(dataReader);

            return dataObject;
        }

        protected virtual T_DbCommand CreateDbCommand(T_DatabaseClient databaseClient)
        {
            T_DbCommand dbCommand = databaseClient.CreateDatabaseCommand(SqlCommandText, DataParameterArray);
            dbCommand.CommandTimeout = CommandTimeout;
            dbCommand.CommandType = DatabaseCommandType;
            dbCommand.Connection = databaseClient.DatabaseConnetion;
            dbCommand.Transaction = databaseClient.DbTransaction;

            return dbCommand;
        }

        protected virtual void DataReaderReadLoop(T_DataReader dataReader)
        {
            while (dataReader.Read())
            {
                T_DataObject dataObject = CreateDataObject(dataReader);

                AddDataObject(dataObject);
            }
        }

        protected virtual Boolean ExecuteDataReaderCommand(T_DbCommand dbCommand)
        {
            Boolean returnState = true;

            T_DataReader dataReader = default(T_DataReader);

            try
            {
                dataReader = (T_DataReader)dbCommand.ExecuteReader();

                DataReaderReadLoop(dataReader);
            }
            catch (Exception exception)
            {
                returnState = false;

                EnterpriseLibraryLogWriter.WriteExceptionLogEntry(exception);
            }
            finally
            {
                if (dataReader != null)
                {
                    dataReader.Close();
                    dataReader.Dispose();
                }
            }

            return returnState;
        }

        protected virtual Boolean ExecutePostDataReaderCommand(T_DatabaseClient databaseClient)
        {
            return databaseClient.CommitTransaction();
        }

        protected virtual Boolean ExecutePreDataReaderCommand(T_DatabaseClient databaseClient)
        {
            return databaseClient.BeginTransaction();
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.