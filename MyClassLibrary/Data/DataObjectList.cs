using System;
using System.Collections.Generic;
using System.Data;


namespace MyClassLibrary.Data
{
    public class DataObjectList<T_DataObject>
        : System.Collections.Generic.List<T_DataObject>, Data.IDataObjectList<T_DataObject>
        where T_DataObject : Data.IDataObject
    {
        //FIELDS
        protected String key;


        //PROPERTIES
        public virtual String Key
        {
            get { return key; }

            protected set
            {
                if (value == null)
                {
                    throw new PropertySetToDefaultException("Key");
                }

                key = value;
            }
        }


        //INITIALIZE
        public DataObjectList()
        {
            key = null;
        }

        public DataObjectList(String Key)
        {
            this.Key = Key;
        }

        public DataObjectList(IEnumerable<T_DataObject> collection)
            : base(collection)
        {
            
        }

        public DataObjectList(String Key, IEnumerable<T_DataObject> collection)
            : base(collection)
        {
            this.Key = Key;
        }


        //METHODS
        public virtual T_DataObject[] CopyToArray()
        {
            T_DataObject[] dataObjectArray = new T_DataObject[this.Count];

            this.CopyTo(dataObjectArray);

            return dataObjectArray;
        }

        public virtual T_DataObject GetDataObject(String key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("key");
            }

            return this.Find(dataObject => dataObject.Key == key);
        }

        public virtual void SetDataObjectFieldsAndProperties(IDataReader dataReader)
        {
            throw new NotImplementedException();
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.