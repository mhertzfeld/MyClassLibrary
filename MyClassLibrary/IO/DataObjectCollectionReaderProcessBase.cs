using MyClassLibrary.Data;
using System;


namespace MyClassLibrary.IO
{
    public abstract class DataObjectCollectionReaderProcessBase<T_DataObject, T_DataObjectCollection>
        : IO.ReaderProcessBase
        where T_DataObjectCollection : System.Collections.Generic.ICollection<T_DataObject>, new()
    {
        //FIELDS
        protected T_DataObjectCollection dataObjectCollection;


        //PROPERTIES
        public virtual T_DataObjectCollection DataObjectCollection
        {
            get { return dataObjectCollection; }

            protected set { dataObjectCollection = value; }
        }


        //INITIALIZE
        public DataObjectCollectionReaderProcessBase()
        {
            dataObjectCollection = default(T_DataObjectCollection);
        }


        //FUNCTIONS
        protected virtual void AddDataObject(T_DataObject dataObject)
        {
            dataObjectCollection.Add(dataObject);
        }

        protected abstract T_DataObject CreateDataObject(String[] stringArray);

        protected override void ProcessLine(string line)
        {
            T_DataObject dataObject = CreateDataObject(line.Split(Deliminter.ToCharArray()));

            AddDataObject(dataObject);
        }

        protected override bool ReadFile(out string[] fileData)
        {
            if (base.ReadFile(out fileData))
            {
                DataObjectCollection = new T_DataObjectCollection();

                return true;
            }
            else
            { return false; }
        }

        protected override void ResetProcess()
        {
            base.ResetProcess();

            DataObjectCollection = default(T_DataObjectCollection);
        }
    }
}