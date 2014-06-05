using System;


namespace MyClassLibrary.Data
{
    public interface IDataObjectCollection<T_DataObject>
        : IDataObject
        where T_DataObject : IDataObject
    {
        T_DataObject GetDataObject(String key);
    }
}