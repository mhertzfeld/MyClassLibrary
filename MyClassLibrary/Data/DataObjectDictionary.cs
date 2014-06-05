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