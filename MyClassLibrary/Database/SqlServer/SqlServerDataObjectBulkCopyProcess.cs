using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace MyClassLibrary.Database.SqlServer
{
    public class SqlServerDataObjectBulkCopyProcess<T_DataObject>
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

        protected List<PropertyColumnData> propertyColumnDataList;

        protected SqlBulkCopy _SqlBulkCopy;


        //PROPERTIES
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

        public virtual List<PropertyColumnData> PropertyColumnDataList
        {
            get { return propertyColumnDataList; }

            set
            {
                if (value == default(List<PropertyColumnData>))
                { throw new PropertySetToDefaultException("PropertyLinkedColumnDataList"); }

                propertyColumnDataList = value;
            }
        }


        //INITIALIZE
        public SqlServerDataObjectBulkCopyProcess()
        {
            batchSize = 2048;

            bulkCopyDataTable = null;

            bulkCopyTimeout = 0;

            connectionString = null;

            dataObjectEnumerable = null;

            destinationTableName = null;

            disposed = false;

            propertyColumnDataList = new List<PropertyColumnData>();

            _SqlBulkCopy = null;
        }
        

        //METHODS
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        public virtual Boolean ExecuteProcess()
        {
            try
            {
                if (BatchSize < 1)
                { throw new InvalidOperationException("BatchSize cannot be less than 1."); }

                if (BulkCopyTimeout < 1)
                { throw new InvalidOperationException("BulkCopyTimeout cannot be less than 1."); }

                if (ConnectionString == null)
                { throw new InvalidOperationException("ConnectionString cannot be null."); }

                if (DataObjectEnumerable == default(IEnumerable<T_DataObject>))
                { throw new InvalidOperationException("DataObjectEnumerable cannot be null."); }

                if (DestinationTableName == null)
                { throw new InvalidOperationException("DestinationTableName cannot be null."); }

                if (PropertyColumnDataList == null)
                { throw new InvalidOperationException("PropertyColumnDataList cannot be null."); }

                CreateBulkCopyDataTable();

                AddRowsToDataTable();

                CreateSqlBulkCopy();

                WriteRecords();

                bulkCopyDataTable.Dispose();
                bulkCopyDataTable = null;

                return true;
            }
            catch (Exception exception)
            { Trace.WriteLine(exception); }

            MyTrace.WriteMethodError(System.Reflection.MethodBase.GetCurrentMethod());

            return false;
        }
        

        //FUNCTIONS
        protected virtual void AddRowsToDataTable()
        {
            foreach (T_DataObject dataObject in DataObjectEnumerable)
            {
                DataRow dataRow = bulkCopyDataTable.NewRow();

                for (Int32 counter = 0; counter <= PropertyColumnDataList.Count - 1; counter++)
                { dataRow[counter] = (typeof(T_DataObject).GetProperty(PropertyColumnDataList[counter].PropertyName).GetValue(dataObject, null) ?? DBNull.Value); }

                bulkCopyDataTable.Rows.Add(dataRow);
            }
        }

        protected virtual void CreateBulkCopyDataTable()
        {
            bulkCopyDataTable = new DataTable(DestinationTableName);

            PropertyColumnDataList.ForEach(element => bulkCopyDataTable.Columns.Add(new DataColumn(element.ColumnName, element.DataType)));
        }
        
        protected virtual void CreateSqlBulkCopy()
        {
            _SqlBulkCopy = new SqlBulkCopy(ConnectionString, SqlBulkCopyOptions.TableLock);
            _SqlBulkCopy.BatchSize = BatchSize;
            _SqlBulkCopy.BulkCopyTimeout = BulkCopyTimeout;
            for (Int32 counter = 0; counter <= PropertyColumnDataList.Count - 1; counter++)
            { _SqlBulkCopy.ColumnMappings.Add(counter, counter); }
            _SqlBulkCopy.DestinationTableName = DestinationTableName;
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
        
        protected virtual void WriteRecords()
        {
            _SqlBulkCopy.WriteToServer(bulkCopyDataTable);
        }
    }
}