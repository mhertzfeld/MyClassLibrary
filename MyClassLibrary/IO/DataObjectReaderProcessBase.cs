using System;


namespace MyClassLibrary.IO
{
    public abstract class DataObjectReaderProcessBase<T_DataObject, T_LogWriter>
        : ReaderProcessBase<T_LogWriter>
        where T_DataObject : new()
        where T_LogWriter : Logging.ILogWriter, new()
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
        protected abstract T_DataObject CreateDataObject(String lineFileDataString);

        protected override void ProcessLine(string line)
        {
            DataObject = CreateDataObject(line);
        }

        protected override void ResetProcess()
        {
            base.ResetProcess();

            DataObject = default(T_DataObject);
        }
    }
}