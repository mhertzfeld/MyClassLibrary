using MyClassLibrary.Logging;
using System;
using System.Data;


namespace MyClassLibrary.Database
{
    public class DatabaseClient<T_DataParameter, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction, T_LogWriter> 
        : System.IDisposable
        where T_DataParameter : System.Data.IDataParameter
        where T_DbCommand : System.Data.IDbCommand, new()
        where T_DbConnection : System.Data.IDbConnection, new()
        where T_DbDataAdapter : System.Data.IDbDataAdapter, new()
        where T_DbTransaction : System.Data.IDbTransaction
        where T_LogWriter : Logging.ILogWriter, new()
    {
        //CONSTANTS
        protected const Int32 defaultTimeout = 900;


        //FIELDS
        protected Int32 commandTimeout;

        protected String connectionString;

        protected Boolean disposed;

        protected T_DbConnection dbConnection;

        protected T_DbTransaction dbTransaction;

        protected IsolationLevel isolationLevel;

        
        //PROPERTIES
        public virtual Int32 CommandTimeout
        {
            get { return commandTimeout; }
        }

        public virtual ConnectionState ConnectionState
        {
            get { return dbConnection.State; }
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

        public virtual T_DbConnection DatabaseConnetion
        {
            get { return dbConnection; }
        }

        public virtual T_DbTransaction DbTransaction
        {
            get { return dbTransaction; }

            set
            {
                if (value == null)
                {
                    throw new PropertySetToDefaultException("DbTransaction");
                }

                dbTransaction = value;
            }
        }

        public virtual IsolationLevel IsolationLevel
        {
            get { return isolationLevel; }

            set { isolationLevel = value; }
        }
        

        //INITIALIZE
        public DatabaseClient()
        {
            disposed = false;

            commandTimeout = defaultTimeout;

            connectionString = null;

            dbConnection = new T_DbConnection();

            dbTransaction = default(T_DbTransaction);

            isolationLevel = IsolationLevel.ReadUncommitted;
        }


        //FINALIZE
        ~DatabaseClient()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(Boolean disposeManagedResources)
        {
            if (!disposed)
            {
                if (dbConnection.State != ConnectionState.Closed)
                {
                    CloseConnection();
                }

                MyTools.DisposeObject(dbConnection);

                MyTools.DisposeObject(dbTransaction);                

                if (disposeManagedResources)
                {
                    connectionString = null;

                    dbConnection = default(T_DbConnection);

                    dbTransaction = default(T_DbTransaction);
                }

                disposed = true;
            }
        }


        //METHODS BeginTransaction
        public Boolean BeginTransaction()
        {
            return BeginTransaction(dbConnection, isolationLevel, out dbTransaction);
        }

        public Boolean BeginTransaction(IsolationLevel IsolationLevel)
        {
            this.IsolationLevel = IsolationLevel;

            return BeginTransaction();
        }


        //METHODS CloseConnection
        public Boolean CloseConnection()
        {
            if (CloseConnection(dbConnection))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

        //METHODS CommitTransaction
        public Boolean CommitTransaction()
        {
            Boolean returnState = CommitTransaction(dbTransaction);

            dbTransaction.Dispose();
            dbTransaction = default(T_DbTransaction);

            return returnState;
        }

       
        //METHODS CreateDatabaseComman
        public T_DbCommand CreateDatabaseCommand(String sqlCommandText)
        {
            if (sqlCommandText == null)
            {
                throw new ArgumentNullException("sqlCommandText");
            }

            T_DbCommand dbCommand = new T_DbCommand();
            dbCommand.CommandText = sqlCommandText;
            dbCommand.Connection = dbConnection;
            dbCommand.CommandTimeout = defaultTimeout;

            return dbCommand;
        }

        public T_DbCommand CreateDatabaseCommand(String sqlCommandText, T_DataParameter[] dataParameterArray)
        {
            if (sqlCommandText == null)
            {
                throw new ArgumentNullException("sqlCommandText");
            }

            T_DbCommand dbCommand = CreateDatabaseCommand(sqlCommandText);

            if ((dataParameterArray != null) && (dataParameterArray.Length > 0))
            {
                foreach (T_DataParameter dataParameter in dataParameterArray)
                {
                    dbCommand.Parameters.Add(dataParameter);
                }
            }

            return dbCommand;
        }


        //METHODS ExecuteNonQuery
        public Boolean ExecuteNonQuery(T_DbCommand dbCommand)
        {
            Int32 junk;

            return ExecuteNonQuery(dbCommand, out junk);
        }

        public Boolean ExecuteNonQuery(T_DbCommand dbCommand, out Int32 rowsAffected)
        {
            return ExecuteNonQueryDbCommand(dbCommand, out rowsAffected);
        }

        public Boolean ExecuteNonQuery(String sqlQuery)
        {
            Int32 junk;

            return ExecuteNonQuery(CreateDatabaseCommand(sqlQuery), out junk);
        }

        public Boolean ExecuteNonQuery(String sqlQuery, out Int32 rowsAffected)
        {
            return ExecuteNonQuery(CreateDatabaseCommand(sqlQuery), out rowsAffected);
        }

        public Boolean ExecuteNonQuery(String sqlQuery, T_DataParameter[] dataParameterArray)
        {
            return ExecuteNonQuery(CreateDatabaseCommand(sqlQuery, dataParameterArray));
        }

        public Boolean ExecuteNonQuery(String sqlQuery, T_DataParameter[] dataParameterArray, out Int32 rowsAffected)
        {
            return ExecuteNonQuery(CreateDatabaseCommand(sqlQuery, dataParameterArray), out rowsAffected);
        }


        //METHODS ExecuteScalar
        public Boolean ExecuteScalar(T_DbCommand dbCommand, out Object result)
        {
            Boolean returnState = false;

            result = null;

            if (BeginTransaction())
            {
                dbCommand.Transaction = dbTransaction;

                if (ExecuteScalarDbCommand(dbCommand, out result))
                {
                    returnState = true;
                }
                else
                {
                    result = null;
                }

                if (!CommitTransaction())
                {
                    returnState = false;
                }
            }

            return returnState;
        }

        public Boolean ExecuteScalar(String sqlQuery, out Object result)
        {
            return ExecuteScalar(CreateDatabaseCommand(sqlQuery), out result);
        }

        public Boolean ExecuteScalar(String sqlQuery, T_DataParameter[] dataParameterArray, out Object result)
        {
            return ExecuteScalar(CreateDatabaseCommand(sqlQuery, dataParameterArray), out result);
        }


        //METHODS FillDataTable
        public Boolean FillDataTable(T_DbCommand dbCommand, out DataTable dataTable)
        {
            Boolean returnState = false;

            dataTable = null;

            if (BeginTransaction())
            {
                dbCommand.Transaction = dbTransaction;

                DataSet dataSet = null;

                if (FillDataSet(dbCommand, out dataSet))
                {
                    returnState = true;

                    if (dataSet.Tables.Count > 0)
                    {
                        dataTable = dataSet.Tables[0].Copy();
                    }
                }

                if (!CommitTransaction())
                {
                    returnState = false;
                }

                MyTools.DisposeObject(dataSet);
            }

            return returnState;
        }

        public Boolean FillDataTable(String sqlQuery, out DataTable dataTable)
        {
            return FillDataTable(CreateDatabaseCommand(sqlQuery), out dataTable);
        }

        public Boolean FillDataTable(String sqlQuery, T_DataParameter[] dataParameterArray, out DataTable dataTable)
        {
            return FillDataTable(CreateDatabaseCommand(sqlQuery, dataParameterArray), out dataTable);
        }


        //METHODS OpenConnection
        public Boolean OpenConnection()
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            return OpenConnection(connectionString, out dbConnection);
        }


        //METHODS RollbackTransaction
        public Boolean RollbackTransaction()
        {
            Boolean returnState = RollbackTransaction(dbTransaction);

            MyTools.DisposeObject(dbTransaction);
            dbTransaction = default(T_DbTransaction);

            return returnState;
        }


        //STATIC METHODS BeginTransaction
        public static Boolean BeginTransaction(T_DbConnection dbConnection, IsolationLevel isolationLevel, out T_DbTransaction dbTransaction)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException("dbConnection");
            }

            Boolean returnState = true;

            try
            {
                dbTransaction = (T_DbTransaction)dbConnection.BeginTransaction(isolationLevel);
            }
            catch (Exception exception)
            {
                returnState = false;

                dbTransaction = default(T_DbTransaction);

                WriteLogEntry(exception);
            }

            return returnState;
        }


        //STATIC METHODS DbCommandTransactions
        public static Boolean DbCommandBeginTransaction(T_DbConnection dbConnection, T_DbCommand dbCommand, IsolationLevel isolationLevel)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException("dbConnection");
            }

            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            Boolean returnState = true;

            try
            {
                dbCommand.Transaction = dbConnection.BeginTransaction(isolationLevel);
            }
            catch (Exception exception)
            {
                returnState = false;

                WriteLogEntry(exception);
            }

            return returnState;
        }

        public static Boolean DbCommandCommitTransaction(T_DbCommand dbCommand)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException();
            }

            Boolean returnState = true;

            try
            {
                dbCommand.Transaction.Commit();
            }
            catch (Exception exception)
            {
                returnState = false;

                WriteLogEntry(exception);
            }

            return returnState;
        }

        public static Boolean DbCommandRollbackTransaction(T_DbCommand dbCommand)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException();
            }

            Boolean returnState = true;

            try
            {
                dbCommand.Transaction.Rollback();
            }
            catch (Exception exception)
            {
                returnState = false;

                WriteLogEntry(exception);                
            }

            return returnState;
        }


        //STATIC METHODS CloseConnection
        public static Boolean CloseConnection(T_DbConnection dbConnection)
        {
            if (dbConnection == null)
            {
                throw new ArgumentNullException();
            }

            Boolean returnState = true;

            try
            {
                dbConnection.Close();
            }
            catch (Exception exception)
            {
                returnState = false;

                WriteLogEntry(exception);
            }

            return returnState;
        }


        //STATIC METHODS CommitTransaction
        public static Boolean CommitTransaction(T_DbTransaction dbTransaction)
        {
            if (dbTransaction == null)
            {
                throw new ArgumentNullException();
            }

            Boolean returnState = true;

            try
            {
                dbTransaction.Commit();
            }
            catch (Exception exception)
            {
                returnState = false;

                WriteLogEntry(exception);
            }

            return returnState;
        }

                
        //STATIC METHODS ExecuteNonQuery
        public static Boolean ExecuteNonQueryDbCommand(T_DbCommand dbCommand, out Int32 rowsAffected)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            Boolean returnState = true;

            rowsAffected = 0;

            try
            {
                rowsAffected = dbCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                returnState = false;

                WriteLogEntry(exception);
            }

            return returnState;
        }

        public static Boolean ExecuteNonQuery(String connectionString, String sqlQuery)
        {
            Int32 rowsAffected;

            return ExecuteNonQuery(connectionString, sqlQuery, null, out rowsAffected);
        }

        public static Boolean ExecuteNonQuery(String connectionString, String sqlQuery, out Int32 rowsAffected)
        {
            return ExecuteNonQuery(connectionString, sqlQuery, null, out rowsAffected);
        }

        public static Boolean ExecuteNonQuery(String connectionString, String sqlCommandText, T_DataParameter[] dataParameterArray)
        {
            Int32 rowsAffected;

            return ExecuteNonQuery(connectionString, sqlCommandText, dataParameterArray, out rowsAffected);
        }

        public static Boolean ExecuteNonQuery(String connectionString, String sqlCommandText, T_DataParameter[] dataParameterArray, out Int32 rowsAffected)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (sqlCommandText == null)
            {
                throw new ArgumentNullException("sqlCommandText");
            }

            T_DbCommand dbCommand = new T_DbCommand();
            dbCommand.CommandText = sqlCommandText;
            dbCommand.CommandTimeout = defaultTimeout;

            if (dataParameterArray != null)
            {
                if (dataParameterArray.Length > 0)
                {
                    foreach (T_DataParameter dataParameter in dataParameterArray)
                    {
                        dbCommand.Parameters.Add(dataParameter);
                    }
                }
            }

            return ExecuteNonQuery(connectionString, dbCommand, out rowsAffected);
        }

        public static Boolean ExecuteNonQuery(String connectionString, T_DbCommand dbCommand, out Int32 rowsAffected)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            Boolean returnState = true;

            rowsAffected = 0;

            T_DbConnection dbConnection;

            if (OpenConnection(connectionString, out dbConnection))
            {
                dbCommand.Connection = dbConnection;

                if (!ExecuteNonQueryDbCommand(dbCommand, out rowsAffected))
                {
                    returnState = false;
                }

                dbCommand.Dispose();

                if (!CloseConnection(dbConnection))
                {
                    returnState = false;
                }
            }
            else
            {
                returnState = false;
            }

            dbConnection.Dispose();

            return returnState;
        }

        
        //STATIC METHODS ExecuteScalar
        public static Boolean ExecuteScalarDbCommand(T_DbCommand dbCommand, out Object result)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            Boolean returnState = true;

            result = null;

            try
            {
                result = dbCommand.ExecuteScalar();
            }
            catch (Exception exception)
            {
                returnState = false;

                WriteLogEntry(exception);
            }

            return returnState;
        }

        public static Boolean ExecuteScalar(String connectionString, String sqlCommandText, out Object result)
        {
            return ExecuteScalar(connectionString, sqlCommandText, null, IsolationLevel.ReadCommitted, out result);
        }

        public static Boolean ExecuteScalar(String connectionString, String sqlCommandText, IsolationLevel isolationLevel, out Object result)
        {
            return ExecuteScalar(connectionString, sqlCommandText, null, isolationLevel, out result);
        }

        public static Boolean ExecuteScalar(String connectionString, String sqlCommandText, T_DataParameter[] dataParameterArray, out Object result)
        {
            return ExecuteScalar(connectionString, sqlCommandText, dataParameterArray, IsolationLevel.ReadUncommitted, out result);
        }

        public static Boolean ExecuteScalar(String connectionString, String sqlCommandText, T_DataParameter[] dataParameterArray, IsolationLevel isolationLevel, out Object result)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (sqlCommandText == null)
            {
                throw new ArgumentNullException("sqlCommandText");
            }

            T_DbCommand dbCommand = new T_DbCommand();
            dbCommand.CommandText = sqlCommandText;
            dbCommand.CommandTimeout = defaultTimeout;

            if (dataParameterArray != null)
            {
                if (dataParameterArray.Length > 0)
                {
                    foreach (T_DataParameter dataParameter in dataParameterArray)
                    {
                        dbCommand.Parameters.Add(dataParameter);
                    }
                }
            }

            return ExecuteScalar(connectionString, dbCommand, isolationLevel, out result);
        }

        public static Boolean ExecuteScalar(String connectionString, T_DbCommand dbCommand, IsolationLevel isolationLevel, out Object result)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            Boolean returnState = true;

            result = null;

            T_DbConnection dbConnection;

            if (OpenConnection(connectionString, out dbConnection))
            {
                dbCommand.Connection = dbConnection;

                if (DbCommandBeginTransaction(dbConnection, dbCommand, isolationLevel))
                {
                    if (!ExecuteScalarDbCommand(dbCommand, out result))
                    {
                        returnState = false;
                    }

                    if (!DbCommandCommitTransaction(dbCommand))
                    {
                        returnState = false;
                    }
                }
                else
                {
                    returnState = false;
                }

                dbCommand.Dispose();

                if (!CloseConnection(dbConnection))
                {
                    returnState = false;
                }
            }
            else
            {
                returnState = false;
            }

            dbConnection.Dispose();

            return returnState;
        }


        //STATIC METHODS FillDataSet & FillDataTable
        public static Boolean FillDataSet(T_DbCommand dbCommand, out DataSet dataSet)
        {
            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            Boolean returnState = true;

            dataSet = new DataSet();

            T_DbDataAdapter dbDataAdapter = default(T_DbDataAdapter);

            try
            {
                dbDataAdapter = new T_DbDataAdapter();
                dbDataAdapter.SelectCommand = dbCommand;
                dbDataAdapter.Fill(dataSet);
            }
            catch (Exception exception)
            {
                returnState = false;

                dataSet = null;

                WriteLogEntry(exception);
            }

            return returnState;
        }

        public static Boolean FillDataTable(String connectionString, String sqlCommandText, out DataTable dataTable)
        {
            return FillDataTable(connectionString, sqlCommandText, null, IsolationLevel.ReadUncommitted, out dataTable);
        }

        public static Boolean FillDataTable(String connectionString, String sqlCommandText, IsolationLevel isolationLevel, out DataTable dataTable)
        {
            return FillDataTable(connectionString, sqlCommandText, null, isolationLevel, out dataTable);
        }

        public static Boolean FillDataTable(String connectionString, String sqlCommandText, T_DataParameter[] dataParameterArray, out DataTable dataTable)
        {
            return FillDataTable(connectionString, sqlCommandText, dataParameterArray, IsolationLevel.ReadUncommitted, out dataTable);
        }

        public static Boolean FillDataTable(String connectionString, String sqlCommandText, T_DataParameter[] dataParameterArray, IsolationLevel isolationLevel, out DataTable dataTable)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (sqlCommandText == null)
            {
                throw new ArgumentNullException("sqlCommandText");
            }

            T_DbCommand dbCommand = new T_DbCommand();
            dbCommand.CommandText = sqlCommandText;
            dbCommand.CommandTimeout = defaultTimeout;

            if (dataParameterArray != null)
            {
                if (dataParameterArray.Length > 0)
                {
                    foreach (T_DataParameter dataParameter in dataParameterArray)
                    {
                        dbCommand.Parameters.Add(dataParameter);
                    }
                }
            }

            return FillDataTable(connectionString, dbCommand, isolationLevel, out dataTable);
        }

        public static Boolean FillDataTable(String connectionString, T_DbCommand dbCommand, IsolationLevel isolationLevel, out DataTable dataTable)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            if (dbCommand == null)
            {
                throw new ArgumentNullException("dbCommand");
            }

            Boolean returnState = false;

            dataTable = null;

            T_DbConnection dbConnection;

            if (OpenConnection(connectionString, out dbConnection))
            {
                dbCommand.Connection = dbConnection;

                if (DbCommandBeginTransaction(dbConnection, dbCommand, isolationLevel))
                {
                    DataSet dataSet;

                    if (FillDataSet(dbCommand, out dataSet))
                    {
                        returnState = true;

                        if (dataSet.Tables.Count > 0)
                        {
                            dataTable = dataSet.Tables[0].Copy();
                        }
                    }

                    if (!DbCommandCommitTransaction(dbCommand))
                    {
                        returnState = false;
                    }

                    MyTools.DisposeObject(dataSet);
                }
                else
                {
                    returnState = false;
                }

                dbCommand.Dispose();

                if (!CloseConnection(dbConnection))
                {
                    returnState = false;
                }
            }

            MyTools.DisposeObject(dbConnection);

            return returnState;
        }


        //STATIC METHODS OpenConnection
        public static Boolean OpenConnection(String connectionString, out T_DbConnection dbConnection)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException("connectionString");
            }

            Boolean returnState = true;

            dbConnection = new T_DbConnection();
            dbConnection.ConnectionString = connectionString;
            
            try
            {
                dbConnection.Open();
            }
            catch (Exception exception)
            {
                returnState = false;

                WriteLogEntry(exception);
            }

            return returnState;
        }


        //STATIC METHODS Rollback Transaction
        public static Boolean RollbackTransaction(T_DbTransaction dbTransaction)
        {
            if (dbTransaction == null)
            {
                throw new ArgumentNullException("dbTransaction");
            }

            Boolean returnState = true;

            try
            {
                dbTransaction.Rollback();
            }
            catch (Exception exception)
            {
                returnState = false;

                WriteLogEntry(exception);
            }

            return returnState;
        }


        //STATIC FUNCTION WRITE LOGS
        public static void WriteLogEntry(Exception exception)
        {
            LoggingTools.WriteLogEntry<T_LogWriter>(exception);
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.