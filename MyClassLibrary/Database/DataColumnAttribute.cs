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



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.