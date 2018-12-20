using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace MyClassLibrary.Database
{
    public abstract class ReaderProcessBase<T_DataParameter, T_DataReader, T_DbCommand, T_DbConnection, T_DbDataAdapter, T_DbTransaction>
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

        protected CommandType? databaseCommandType;

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

        public virtual CommandType? DatabaseCommandType
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

            databaseCommandType = null;

            dataParameterList = new List<T_DataParameter>();

            isolationLevel = IsolationLevel.ReadCommitted;
            
            sqlCommandText = null;
        }


        //METHODS
        public virtual Boolean ExecuteProcess()
        {
            try
            {
                if (ConnectionString == null)
                { throw new InvalidOperationException("ConnectionString cannot be null."); }

                if (DatabaseCommandType == null)
                { throw new InvalidOperationException("DatabaseCommandType cannot be null."); }

                if (SqlCommandText == null)
                { throw new InvalidOperationException("SqlCommandText cannot be null."); }

                using (T_DbConnection _DbConnection = new T_DbConnection())
                {
                    _DbConnection.ConnectionString = ConnectionString;
                    _DbConnection.Open();
                    
                    using (T_DbTransaction _DbTransaction = (T_DbTransaction)_DbConnection.BeginTransaction(this.IsolationLevel))
                    {
                        using (T_DbCommand _DbCommand = CreateDbCommand(_DbConnection, _DbTransaction))
                        {                            
                            using (T_DataReader _DataReader = (T_DataReader)_DbCommand.ExecuteReader())
                            {
                                while (_DataReader.Read())
                                { ProcessRecord(_DataReader); }
                            }

                            _DbTransaction.Commit();
                        }
                    }

                    _DbConnection.Close();
                }

                return true;
            }
            catch (Exception exception)
            { Trace.WriteLine(exception); }

            MyTrace.WriteMethodError(System.Reflection.MethodBase.GetCurrentMethod());

            return false;
        }


        //FUNCTIONS
        protected virtual T_DbCommand CreateDbCommand(T_DbConnection _DbConnection, T_DbTransaction _DbTransaction)
        {
            T_DbCommand dbCommand = new T_DbCommand();
            dbCommand.CommandText = SqlCommandText;
            dbCommand.CommandTimeout = CommandTimeout;
            dbCommand.CommandType = DatabaseCommandType.Value;
            dbCommand.Connection = _DbConnection;
            dbCommand.Transaction = _DbTransaction;
            if ((DataParameterList != null) && (DataParameterList.Count > 0))
            { DataParameterList.ForEach(element => dbCommand.Parameters.Add(element)); }

            return dbCommand;
        }

        protected abstract void ProcessRecord(T_DataReader dataReader);
    }
}