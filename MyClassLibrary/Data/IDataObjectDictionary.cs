using System;
using System.Collections.Generic;


namespace MyClassLibrary.Data
{
    public interface IDataObjectDictionary<T_DataObject>
        : ICollection<KeyValuePair<String, T_DataObject>>, IDataObjectCollection<T_DataObject>, IDictionary<String, T_DataObject>, IEnumerable<KeyValuePair<String, T_DataObject>>
        where T_DataObject : IDataObject
    {
        Boolean Add(T_DataObject dataObject);

        Boolean AddRange(IEnumerable<T_DataObject> collection, Boolean ignoreDuplicates);
        
        T_DataObjectList CopyToDataObjectList<T_DataObjectList>()
            where T_DataObjectList : IDataObjectList<T_DataObject>, new();
    }
}