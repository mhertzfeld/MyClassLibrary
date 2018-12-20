using System;


namespace MyClassLibrary.IO
{
    public class DataObjectCollectionReaderProcess<T_DataObject, T_DataObjectCollection>
        : DataObjectCollectionReaderProcessBase<T_DataObject, T_DataObjectCollection>
        where T_DataObject : DataObjectReaderInterface, new()
        where T_DataObjectCollection : System.Collections.Generic.ICollection<T_DataObject>, new()
    {
        protected override T_DataObject CreateDataObject(string[] stringArray)
        {
            T_DataObject _DataObject = new T_DataObject();
            _DataObject.SetFields(stringArray);

            return _DataObject;
        }
    }
}