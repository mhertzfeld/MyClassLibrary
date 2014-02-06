using MyClassLibrary;
using MyClassLibrary.Database.SqlServer;
using MyClassLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace MyClassLibrary.Database.SqlServer
{
    public abstract class SqlServerDataObjectBulkCopyProcessBase<T_DataObject, T_LogWriter>
        : MyClassLibrary.Process.ProcessWorkerBase
        where T_DataObject : Data.IDataObject
        where T_LogWriter :  Logging.ILogWriter, new()
    {
        //FIELDS
        protected List<String> columnList;

        protected DataTable dataTable;

        protected IEnumerable<T_DataObject> dataObjectEnumerable;


        //PROTECTED PROPERTIES
        protected abstract Int32 BulkCopyTimeout { get; }

        protected virtual List<String> ColumnList
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

        protected abstract String ConnectionString { get; }

        protected virtual DataTable DataTable
        {
            get { return dataTable; }
        }

        protected abstract String DestinationTableName
        {
            get;
        }


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
        public SqlServerDataObjectBulkCopyProcessBase()
        {
            columnList = null;

            dataObjectEnumerable = null;

            dataTable = null;
        }


        //METHODS
        public override bool ProcessExecution()
        {
            if (DataObjectEnumerable == default(IEnumerable<T_DataObject>))
            {
                throw new PropertySetToDefaultException("DataObjectEnumerable");
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

        public virtual Boolean ProcessExecution(IEnumerable<T_DataObject> DataObjectEnumerable)
        {
            this.DataObjectEnumerable = DataObjectEnumerable;

            return ProcessExecution();
        }


        //FUNCTIONS
        public void AddColumnsToDataTable()
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

        public void AddRowsToDataTable()
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

            using (SqlServerDatabaseClient<T_LogWriter> sqlServerDatabaseClient = new SqlServerDatabaseClient<T_LogWriter>())
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

                            returnState = SqlServerDatabaseClient<T_LogWriter>.ExecuteNonQueryDbCommand(sqlCommand, out rowsAffected);
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
                    LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                    return false;
                }

                return true;
            }
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.