using System;
using System.Collections.Generic;
using System.Data;


namespace MyClassLibrary.Data
{
    public class DataObjectDictionary<T_DataObject> 
        : System.Collections.Generic.Dictionary<String, T_DataObject>, IDataObjectDictionary<T_DataObject>
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
        public DataObjectDictionary()
        {
            key = null;
        }

        public DataObjectDictionary(String Key)
        {
            this.Key = Key;
        }
        

        //METHODS
        public virtual Boolean Add(T_DataObject dataObject)
        {
            if (ContainsKey(dataObject.Key))
            {
                return false;
            }
            else
            {
                Add(dataObject.Key, dataObject);

                return true;
            }
        }

        public virtual Boolean AddRange(IEnumerable<T_DataObject> collection, Boolean ignoreDuplicates = false)
        {
            foreach (T_DataObject dataObject in collection)
            {
                if ((!Add(dataObject)) && (!ignoreDuplicates))
                {
                    return false;
                }
            }

            return true;
        }

        public virtual T_DataObjectList CopyToDataObjectList<T_DataObjectList>()
            where T_DataObjectList : IDataObjectList<T_DataObject>, new()
        {
            T_DataObjectList dataObjectList = new T_DataObjectList();

            foreach (T_DataObject dataObject in Values)
            {
                dataObjectList.Add(dataObject);
            }
            
            return dataObjectList;
        }

        public virtual T_DataObject GetDataObject(String key)
        {
            T_DataObject dataObject;

            TryGetValue(key, out dataObject);

            return dataObject;                
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