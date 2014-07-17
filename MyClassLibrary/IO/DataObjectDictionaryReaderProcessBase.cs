using System;


namespace MyClassLibrary.IO
{
    public abstract class DataObjectDictionaryReaderProcessBase<T_Key, T_DataObject, T_DataObjectDictionary>
        : IO.ReaderProcessBase
        where T_DataObjectDictionary : System.Collections.Generic.IDictionary<T_Key, T_DataObject>, new()
    {
        //FIELDS
        protected T_DataObjectDictionary dataObjectDictionary;


        //PROPERTIES
        public virtual T_DataObjectDictionary DataObjectDictionary
        {
            get { return dataObjectDictionary; }

            protected set { dataObjectDictionary = value; }
        }


        //INITIALIZE
        public DataObjectDictionaryReaderProcessBase()
        {
            dataObjectDictionary = default(T_DataObjectDictionary);
        }


        //METHODS
        public override bool ExecuteProcess()
        {
            DataObjectDictionary = default(T_DataObjectDictionary);

            return base.ExecuteProcess();
        }
        

        //FUNCTIONS
        protected abstract void AddDataObjectToDataObjectDictionary(T_DataObject dataObject);

        protected abstract T_DataObject CreateDataObject(String[] stringArray);

        protected override bool ReadFile(out string[] fileData)
        {
            if (base.ReadFile(out fileData))
            {
                DataObjectDictionary = new T_DataObjectDictionary();

                return true;
            }
            else
            { return false; }
        }

        protected override void ProcessLine(string line)
        {
            T_DataObject dataObject = CreateDataObject(line.Split(Deliminter.ToCharArray()));

            AddDataObjectToDataObjectDictionary(dataObject);
        }
    }
}