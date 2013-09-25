using MyClassLibrary.Data;
using System;


namespace MyClassLibrary.IO
{
    public abstract class DataObjectListReaderProcessBase<T_DataObject, T_DataObjectList, T_LogWriter>
        : IO.DataObjectReaderProcessBase<T_DataObject, T_LogWriter>
        where T_DataObject : IO.IDataObject, new()
        where T_DataObjectList : Data.IDataObjectList<T_DataObject>, new()
        where T_LogWriter : Logging.ILogWriter, new()
    {
        //FIELDS
        protected T_DataObjectList dataObjectList;


        //PROPERTIES
        public virtual T_DataObjectList DataObjectList
        {
            get { return dataObjectList; }
        }


        //INITIALIZE
        public DataObjectListReaderProcessBase()
        {
            dataObjectList = default(T_DataObjectList);
        }


        //METHODS
        public override bool ProcessExecution()
        {
            dataObjectList = new T_DataObjectList();

            return base.ProcessExecution();
        }


        //FUNCTIONS
        protected override void AddDataObject(T_DataObject dataObject)
        {
            dataObjectList.Add(dataObject);
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.