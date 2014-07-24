using MyClassLibrary;
using MyClassLibrary.Database.SqlServer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace MyClassLibrary.Database.SqlServer
{
    public abstract class ByAttributeSqlServerDataObjectBulkCopyProcessBase<T_DataObject>
    {
        //FIELDS
        protected Int32 bulkCopyTimeout;

        protected List<String> columnList;

        protected String connectionString;

        protected DataTable dataTable;

        protected IEnumerable<T_DataObject> dataObjectEnumerable;

        protected String destinationTableName;


        //PROPERTIES
        public virtual Int32 BulkCopyTimeout
        {
            get { return bulkCopyTimeout; }

            set
            {
                if (value < 0) { throw new PropertySetToOutOfRangeValueException("BulkCopyTimeout"); }

                bulkCopyTimeout = value;
            }
        }

        public virtual List<String> ColumnList
        {
            get { return columnList; }

            set 
            {
                if (value == default(List<String>))
                {
                    throw new PropertySetToDefaultException("ColumnList");
                }

                columnList = value; 
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

        public virtual DataTable DataTable
        {
            get { return dataTable; }
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
        public ByAttributeSqlServerDataObjectBulkCopyProcessBase()
        {
            bulkCopyTimeout = 0;

            columnList = null;

            connectionString = null;

            dataObjectEnumerable = null;

            dataTable = null;

            destinationTableName = null;
        }


        //METHODS
        public virtual Boolean ExecuteProcess()
        {
            if (DataObjectEnumerable == default(IEnumerable<T_DataObject>))
            {
                throw new InvalidOperationException("DataObjectEnumerable");
            }
            
            BuildColumnList();

            BuildDataTable();

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
        protected virtual void AddColumnsToDataTable()
        {
            for (Int32 counter = 0; counter <= ColumnList.Count - 1; counter++)
            {
                DataColumnAttribute dataColumnAttribute = DataColumnAttribute.GetColumnNameFromProperty<T_DataObject>(ColumnList[counter]);

                if (dataColumnAttribute == null)
                {
                    throw new Exception("DataColumnAttribute for " + ColumnList[counter] + " does not exist.");
                }

                dataTable.Columns.Add(dataColumnAttribute.ColumnName, dataColumnAttribute.ColumnType);
            }
        }

        protected virtual void AddRowsToDataTable()
        {
            foreach (T_DataObject dataObject in DataObjectEnumerable)
            {
                DataRow dataRow = dataTable.NewRow();

                for (Int32 counter = 0; counter <= ColumnList.Count - 1; counter++)
                {
                    dataRow[counter] = (typeof(T_DataObject).GetProperty(ColumnList[counter]).GetValue(dataObject, null) ?? DBNull.Value);
                }

                dataTable.Rows.Add(dataRow);
            }
        }

        protected abstract void BuildColumnList();

        protected virtual void BuildDataTable()
        {
            dataTable = new DataTable(DestinationTableName);

            AddColumnsToDataTable();

            AddRowsToDataTable();
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

        protected virtual SqlBulkCopy CreateSqlBulkCopy()
        {
            SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(ConnectionString, SqlBulkCopyOptions.TableLock);
            sqlBulkCopy.BatchSize = 2048;
            sqlBulkCopy.BulkCopyTimeout = BulkCopyTimeout;
            for (Int32 counter = 0; counter <= ColumnList.Count - 1; counter++)
            {
                sqlBulkCopy.ColumnMappings.Add(counter, counter);
            }
            sqlBulkCopy.DestinationTableName = DestinationTableName;

            return sqlBulkCopy;
        }

        protected virtual Boolean WriteRecords()
        {
            using (SqlBulkCopy sqlBulkCopy = CreateSqlBulkCopy())
            {
                try
                {
                    sqlBulkCopy.WriteToServer(dataTable);
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