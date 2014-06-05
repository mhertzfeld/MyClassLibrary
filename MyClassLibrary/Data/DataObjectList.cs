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