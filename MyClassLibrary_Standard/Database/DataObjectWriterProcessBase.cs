using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace MyClassLibrary.Database
{
    public abstract class DataObjectWriterProcessBase<T_DataObject, T_DataParameter, T_DbCommand, T_DbConnection, T_DbTransaction>
        where T_DataParameter : System.Data.IDataParameter
        where T_DbCommand : System.Data.IDbCommand, new()
        where T_DbConnection : System.Data.IDbConnection, new()
        where T_DbTransaction : System.Data.IDbTransaction
    {
        //FIELDS
        protected Int32 commandTimeout;

        protected String connectionString;

        protected IEnumerable<T_DataObject> dataObjectEnumerable;


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
            CommandTimeout = 90;

            connectionString = null;

            dataObjectEnumerable = null;
        }


        //METHODS
        public virtual Boolean ExecuteProcess()
        {
            try
            {
                if (ConnectionString == null)
                { throw new InvalidOperationException("ConnectionString cannot be null."); }

                if (DataObjectEnumerable == null)
                { throw new InvalidOperationException("DataObjectEnumerable cannot be null."); }

                using (T_DbConnection _DbConnection = new T_DbConnection())
                {
                    _DbConnection.ConnectionString = ConnectionString;
                    _DbConnection.Open();

                    foreach (T_DataObject dataObject in DataObjectEnumerable)
                    {
                        using (T_DbCommand dbCommand = CreateWriteSqlCommand(_DbConnection, dataObject))
                        { dbCommand.ExecuteNonQuery(); }
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
        protected abstract T_DbCommand CreateWriteSqlCommand(T_DbConnection dbConnection, T_DataObject dataObject);
    }
}