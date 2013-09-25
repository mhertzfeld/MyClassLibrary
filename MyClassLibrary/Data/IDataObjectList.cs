﻿using System;


namespace MyClassLibrary.Data
{
    public interface IDataObjectList<T_DataObject>
        : System.Collections.Generic.ICollection<T_DataObject>, IDataObject, IDataObjectCollection<T_DataObject>, System.Collections.Generic.IEnumerable<T_DataObject>, System.Collections.Generic.IList<T_DataObject>
        where T_DataObject : IDataObject
    {
        
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.