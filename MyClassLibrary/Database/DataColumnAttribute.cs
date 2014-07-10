using System;
using System.Reflection;


namespace MyClassLibrary.Database
{
	[AttributeUsage( AttributeTargets.Property )]
	public class DataColumnAttribute 
        : Attribute 
    {
        //FIELDS
		protected String columnName;

		protected Type columnType;


		//PROPERTIES
		public String ColumnName 
        {
			get { return columnName; }

			set { columnName = value; }
		}

		public Type ColumnType 
        {
			get { return columnType; }

			set { columnType = value; }
		}


        //INITIALIZE
		private DataColumnAttribute()
        {
			columnName = String.Empty;

			columnType = null;
		}

		public DataColumnAttribute(String ColumnName, Type ColumnType)
			: this() 
        {
			this.ColumnName = ColumnName;

			this.ColumnType = ColumnType;
		}


        //STATIC METHODS
        public static DataColumnAttribute GetColumnNameFromProperty<T>(String propertyName)
        {
            DataColumnAttribute dataColumnAttribute = null;

            object[] dataColumnAttributeArray = null;

            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.Name == propertyName)
                {
                    dataColumnAttributeArray = propertyInfo.GetCustomAttributes(typeof(DataColumnAttribute), true);

                    if (dataColumnAttributeArray.Length > 0)
                    {
                        dataColumnAttribute = (DataColumnAttribute)dataColumnAttributeArray[0];

                        break;
                    }
                }
            }

            return dataColumnAttribute;
        }
	}
}