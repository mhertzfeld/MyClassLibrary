using MyClassLibrary;
using MyClassLibrary.Database.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace MyClassLibrary.Database.SqlServer
{
    public abstract class SqlServerDataObjectBulkCopyProcessBase<T_DataObject>
        : IDisposable
    {
        //FIELDS
        protected Int32 batchSize;

        protected DataTable bulkCopyDataTable;

        protected Int32 bulkCopyTimeout;

        protected String connectionString;
        
        protected IEnumerable<T_DataObject> dataObjectEnumerable;

        protected String destinationTableName;

        protected bool disposed;

        protected List<PropertyLinkedColumnData> propertyLinkedColumnDataList;


        //PROTECTED PROPERTIES
        public virtual List<PropertyLinkedColumnData> PropertyLinkedColumnDataList
        {
            get { return propertyLinkedColumnDataList; }

            set
            {
                if (value == default(List<PropertyLinkedColumnData>))
                { throw new PropertySetToDefaultException("PropertyLinkedColumnDataList"); }

                propertyLinkedColumnDataList = value;
            }
        }

        public virtual DataTable BulkCopyDataTable
        {
            get { return bulkCopyDataTable; }

            protected set
            {
                if (value == default(DataTable))
                { throw new PropertySetToDefaultException("BulkCopyDataTable"); }

                bulkCopyDataTable = value;
            }
        }


        //PUBLIC PROPERTIES
        public virtual Int32 BatchSize
        {
            get { return batchSize; }

            set
            {
                if (value < 1)
                { throw new ValueOutOfRangeException("BatchSize"); }

                batchSize = value;
            }
        }

        public virtual Int32 BulkCopyTimeout
        {
            get { return bulkCopyTimeout; }

            set
            {
                if (value < 0) { throw new PropertySetToOutOfRangeValueException("BulkCopyTimeout"); }

                bulkCopyTimeout = value;
            }
        }
        
        public virtual String ConnectionString
        {
            get { return connectionString; }

            set
            {
                if (value == default(String)) { throw new PropertySetToDefaultException("ConnectionString"); }

                connectionString = value;
            }
        }

        public virtual IEnumerable<T_DataObject> DataObjectEnumerable
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

        public virtual String DestinationTableName
        {
            get { return destinationTableName; }

            set
            {
                if (value == default(String)) { throw new PropertySetToDefaultException("DestinationTableName"); }

                destinationTableName = value;
            }
        }
        

        //INITIALIZE
        public SqlServerDataObjectBulkCopyProcessBase()
        {
            batchSize = 2048;

            bulkCopyDataTable = null;

            bulkCopyTimeout = 0;

            connectionString = null;

            dataObjectEnumerable = null;

            destinationTableName = null;

            disposed = false;

            propertyLinkedColumnDataList = null;
        }
        

        //METHODS
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public virtual Boolean ExecuteProcess()
        {
            if (DataObjectEnumerable == default(IEnumerable<T_DataObject>))
            {
                throw new InvalidOperationException("DataObjectEnumerable");
            }
            
            PropertyLinkedColumnDataList = CreatePropertyLinkedColumnData();
            
            BulkCopyDataTable = CreateBulkCopyDataTable();

            AddRowsToDataTable();

            if (CleanupRecords())
            {
                return WriteRecords();
            }
            else
            {
                return false;
            }
        }

        public virtual Boolean ExecuteProcess(IEnumerable<T_DataObject> DataObjectEnumerable)
        {
            this.DataObjectEnumerable = DataObjectEnumerable;

            return ExecuteProcess();
        }


        //FUNCTIONS
        protected virtual void AddRowsToDataTable()
        {
            foreach (T_DataObject dataObject in DataObjectEnumerable)
            {
                DataRow dataRow = bulkCopyDataTable.NewRow();

                for (Int32 counter = 0; counter <= PropertyLinkedColumnDataList.Count - 1; counter++)
                {
                    dataRow[counter] = (typeof(T_DataObject).GetProperty(PropertyLinkedColumnDataList[counter].PropertyName).GetValue(dataObject, null) ?? DBNull.Value);
                }

                bulkCopyDataTable.Rows.Add(dataRow);
            }
        }

        protected virtual DataTable CreateBulkCopyDataTable()
        {
            DataTable dataTable = new DataTable(DestinationTableName);

            PropertyLinkedColumnDataList.ForEach(element => dataTable.Columns.Add(element.DataColumn));

            return dataTable;
        }

        protected virtual Boolean CleanupRecords()
        {
            Boolean returnState = false;

            using (SqlServerDatabaseClient sqlServerDatabaseClient = new SqlServerDatabaseClient())
            {
                sqlServerDatabaseClient.ConnectionString = ConnectionString;

                if (sqlServerDatabaseClient.OpenConnection())
                {
                    using (SqlCommand sqlCommand = CreateCleanupSqlCommand(sqlServerDatabaseClient.DatabaseConnetion))
                    {
                        if (sqlCommand == null)
                        {
                            return true;
                        }
                        else
                        {
                            Int32 rowsAffected;

                            returnState = SqlServerDatabaseClient.ExecuteNonQueryDbCommand(sqlCommand, out rowsAffected);
                        }
                    }

                    if (!sqlServerDatabaseClient.CloseConnection())
                    {
                        returnState = false;
                    }
                }
            }

            return returnState;
        }

        protected abstract SqlCommand CreateCleanupSqlCommand(SqlConnection sqlConnection);

        protected abstract List<PropertyLinkedColumnData> CreatePropertyLinkedColumnData();

        protected virtual SqlBulkCopy CreateSqlBulkCopy()
        {
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(ConnectionString, SqlBulkCopyOptions.TableLock);
            sqlBulkCopy.BatchSize = BatchSize;
            sqlBulkCopy.BulkCopyTimeout = BulkCopyTimeout;
            for (Int32 counter = 0; counter <= PropertyLinkedColumnDataList.Count - 1; counter++)
            {
                sqlBulkCopy.ColumnMappings.Add(counter, counter);
            }
            sqlBulkCopy.DestinationTableName = DestinationTableName;

            return sqlBulkCopy;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            { return; }

            if (disposing)
            {
                if (bulkCopyDataTable != null)
                { bulkCopyDataTable.Dispose(); }
            }

            disposed = true;
        }

        protected virtual Boolean WriteRecords()
        {
            using (SqlBulkCopy sqlBulkCopy = CreateSqlBulkCopy())
            {
                try
                {
                    sqlBulkCopy.WriteToServer(bulkCopyDataTable);
                }
                catch (Exception exception)
                {
                    Trace.WriteLine(exception);

                    return false;
                }

                return true;
            }
        }
    }
}