using MyClassLibrary.Data;
using System;


namespace MyClassLibrary.IO
{
    public abstract class DataObjectDictionaryReaderProcessBase<T_DataObject, T_DataObjectDictionary, T_LogWriter>
        : IO.DataObjectReaderProcessBase<T_DataObject, T_LogWriter>
        where T_DataObject : IO.IDataObject, new()
        where T_DataObjectDictionary : Data.IDataObjectDictionary<T_DataObject>, new()
        where T_LogWriter : Logging.ILogWriter, new()
    {
        //FIELDS
        protected T_DataObjectDictionary dataObjectDictionary;


        //PROPERTIES
        public virtual T_DataObjectDictionary DataObjectDictionary
        {
            get { return dataObjectDictionary; }
        }


        //INITIALIZE
        public DataObjectDictionaryReaderProcessBase()
        {
            dataObjectDictionary = default(T_DataObjectDictionary);
        }


        //METHODS
        public override bool ProcessExecution()
        {
            dataObjectDictionary = new T_DataObjectDictionary();

            return base.ProcessExecution();
        }


        //FUNCTIONS
        protected override void AddDataObject(T_DataObject dataObject)
        {
            dataObjectDictionary.Add(dataObject);
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.