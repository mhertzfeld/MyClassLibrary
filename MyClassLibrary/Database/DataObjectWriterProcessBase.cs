using MyClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace MyClassLibrary.Database
{
    public abstract class DataObjectWriterProcessBase<T_DataObject, T_DatabaseClient, T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>
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
        public virtual Boolean ExecuteProcess()
        {
            if (DataObjectEnumerable == default(IEnumerable<T_DataObject>))
            {
                throw new InvalidOperationException("DataObjectEnumerable can not be set to Default.");
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

        public virtual Boolean ExecuteProcess(IEnumerable<T_DataObject> DataObjectEnumerable)
        {
            this.DataObjectEnumerable = DataObjectEnumerable;

            return ExecuteProcess();
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