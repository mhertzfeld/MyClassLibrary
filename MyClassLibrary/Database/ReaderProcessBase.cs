using MyClassLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Data;


namespace MyClassLibrary.Database
{
    public abstract class ReaderProcessBase<T_DatabaseClient, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction, T_LogWriter> 
        : MyClassLibrary.Process.ProcessWorkerBase
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
        protected Int32 commandTimeout;

        protected String connectionString;

        protected CommandType databaseCommandType;

        protected List<T_DataParameter> dataParameterList;

        protected IsolationLevel isolationLevel;

        protected String sqlCommandText;


        //PROPERTIES
        public virtual Int32 CommandTimeout
        {
            get { return commandTimeout; }

            set
            {
                if (value < 0)
                {
                    throw new PropertySetToOutOfRangeValueException("CommandTimeout");
                }

                commandTimeout = value;
            }
        }

        public virtual String ConnectionString
        {
            get { return connectionString; }

            set
            {
                if (value == default(String))
                {
                    throw new PropertySetToDefaultException("ConnectionString");
                }

                connectionString = value;
            }
        }

        public virtual CommandType DatabaseCommandType
        {
            get { return databaseCommandType; }

            set { databaseCommandType = value; }
        }

        public virtual List<T_DataParameter> DataParameterList
        {
            get { return dataParameterList; }

            set
            {
                if (value == default(List<T_DataParameter>)) { throw new PropertySetToDefaultException("DataParameterArray"); }

                dataParameterList = value;
            }
        }

        public virtual IsolationLevel IsolationLevel
        {
            get { return isolationLevel; }

            set { isolationLevel = value; }
        }

        public virtual String SqlCommandText
        {
            get { return sqlCommandText; }

            set
            {
                if (value == default(String))
                {
                    throw new PropertySetToDefaultException("SqlCommandText");
                }

                sqlCommandText = value;
            }
        }


        //INITIALIZE
        public ReaderProcessBase()
        {
            CommandTimeout = 90;

            connectionString = null;

            DatabaseCommandType = CommandType.Text;

            dataParameterList = null;

            IsolationLevel = IsolationLevel.ReadCommitted;

            sqlCommandText = null;
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
        protected virtual T_DbCommand CreateDbCommand(T_DatabaseClient databaseClient)
        {
            T_DbCommand dbCommand = new T_DbCommand();
            dbCommand.CommandText = SqlCommandText;
            dbCommand.CommandTimeout = CommandTimeout;
            dbCommand.CommandType = DatabaseCommandType;
            dbCommand.Connection = databaseClient.DatabaseConnetion;            
            
            if (databaseClient.DbTransaction != null) 
            { dbCommand.Transaction = databaseClient.DbTransaction; }

            if ((DataParameterList != null) && (DataParameterList.Count > 0))
            { DataParameterList.ForEach(element => dbCommand.Parameters.Add(element)); }

            return dbCommand;
        }

        protected virtual void DataReaderReadLoop(T_DataReader dataReader)
        {
            while (dataReader.Read())
            {
                ProcessRecord(dataReader);
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

        protected abstract void ProcessRecord(T_DataReader dataReader);
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.