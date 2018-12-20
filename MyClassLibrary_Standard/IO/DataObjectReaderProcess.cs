using System;


namespace MyClassLibrary.IO
{
    public class DataObjectReaderProcess<T_DataObject>
        : DataObjectReaderProcessBase<T_DataObject>
        where T_DataObject : DataObjectReaderInterface, new()
    {
        protected override T_DataObject CreateDataObject(string[] stringArray)
        {
            T_DataObject _DataObject = new T_DataObject();
            _DataObject.SetFields(stringArray);

            return _DataObject;
        }
    }
}