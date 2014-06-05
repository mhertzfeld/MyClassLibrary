using System;
using System.Data;


namespace MyClassLibrary.Data
{
    public abstract class DataObjectBase
        : Data.IDataObject
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
        public DataObjectBase()
        {
            key = null;
        }

        public DataObjectBase(String Key)
        {
            this.Key = Key;
        }


        //METHODS
        public override string ToString()
        {
            return key;
        }
    }
}