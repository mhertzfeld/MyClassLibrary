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



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.
