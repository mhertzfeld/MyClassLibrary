using System;


namespace MyClassLibrary.IO
{
    public abstract class DataObjectReaderProcessBase<T_DataObject>
        : ReaderProcessBase
    {
        //FIELDS
        protected T_DataObject dataObject;


        //PROPERTIES
        public virtual T_DataObject DataObject
        {
            get { return dataObject; }

            protected set { dataObject = value; }
        }


        //INITIALIZE
        public DataObjectReaderProcessBase()
        {
            dataObject = default(T_DataObject);
        }


        //FUNCTIONS
        protected abstract T_DataObject CreateDataObject(String[] stringArray);

        protected override void ProcessLine(string line)
        {
            DataObject = CreateDataObject(line.Split(Deliminter.ToCharArray()));
        }

        protected override void ResetProcess()
        {
            base.ResetProcess();

            DataObject = default(T_DataObject);
        }
    }
}