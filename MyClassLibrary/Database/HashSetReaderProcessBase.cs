using MyClassLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Data;


namespace MyClassLibrary.Database
{
    public abstract class HashSetReaderProcessBase<T_DatabaseClient, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction, T_HashSetType, T_LogWriter>
        : Process.ProcessWorkerBase
        where T_DatabaseClient : Database.DatabaseClient<T_DataParameter, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction, T_LogWriter>, new()
        where T_DataParameter : System.Data.IDataParameter
        where T_DataReader : System.Data.IDataReader
        where T_DbCommand : System.Data.IDbCommand, new()
        where T_DbConnection : System.Data.IDbConnection, new()
        where T_DbDataAdapter : System.Data.IDbDataAdapter, new()
        where T_DbTransaction : System.Data.IDbTransaction
        where T_LogWriter : Logging.ILogWriter, new()
    {
        //FIELDS
        protected HashSet<T_HashSetType> hashSet;


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

        protected abstract String SqlCommandText
        {
            get;
        }


        //PUBLIC PROPERTIES
        public virtual HashSet<T_HashSetType> HashSet
        {
            get { return hashSet; }
        }


        //INITIALIZE
        public HashSetReaderProcessBase()
        {
            hashSet = null;
        }


        //FINALIZE
        protected override void Dispose(bool disposeManagedResources)
        {
            if (!disposed)
            {
                if (disposeManagedResources)
                {
                    hashSet = null;
                }
            }

            base.Dispose(disposeManagedResources);
        }


        //METHODS
        public override Boolean ProcessExecution()
        {
            Boolean returnState = false;

            hashSet = new HashSet<T_HashSetType>();

            using (T_DatabaseClient databaseClient = new T_DatabaseClient())
            {
                databaseClient.ConnectionString = ConnectionString;

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
        protected virtual void AddToHashSet(T_DataReader dataReader)
        {
            throw new NotImplementedException();
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
                AddToHashSet(dataReader);
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

                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);
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

        protected override void ResetProcess()
        {
            hashSet = null;

            base.ResetProcess();
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.