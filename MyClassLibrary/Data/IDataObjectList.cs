using System;


namespace MyClassLibrary.Data
{
    public interface IDataObjectList<T_DataObject>
        : System.Collections.Generic.ICollection<T_DataObject>, Data.IDataObject, Data.IDataObjectCollection<T_DataObject>, System.Collections.Generic.IEnumerable<T_DataObject>, System.Collections.Generic.IList<T_DataObject>
        where T_DataObject : Data.IDataObject
    {
        
    }
}