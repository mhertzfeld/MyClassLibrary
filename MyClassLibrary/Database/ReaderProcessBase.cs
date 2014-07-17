using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace MyClassLibrary.Database
{
    public abstract class ReaderProcessBase<T_DatabaseClient, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>
        where T_DatabaseClient : Database.DatabaseClient<T_DataParameter, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>, new()
        where T_DataParameter : System.Data.IDataParameter
        where T_DataReader : System.Data.IDataReader
        where T_DbCommand : System.Data.IDbCommand, new()
        where T_DbConnection : System.Data.IDbConnection, new()
        where T_DbDataAdapter : System.Data.IDbDataAdapter, new()
        where T_DbTransaction : System.Data.IDbTransaction
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

            dataParameterList = new List<T_DataParameter>();

            IsolationLevel = IsolationLevel.ReadCommitted;

            sqlCommandText = null;
        }


        //METHODS
        public virtual Boolean ExecuteProcess()
        {
            Boolean returnState = false;

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

                Trace.WriteLine(exception);
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