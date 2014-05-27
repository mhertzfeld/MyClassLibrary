using System;


namespace MyClassLibrary.IO
{
    public abstract class DataObjectDictionaryReaderProcessBase<T_DataObject, T_DataObjectDictionary, T_LogWriter, T_Key>
        : IO.ReaderProcessBase<T_LogWriter>
        //where T_DataObject : new()
        where T_DataObjectDictionary : System.Collections.Generic.IDictionary<T_Key, T_DataObject>, new()
        where T_LogWriter : Logging.ILogWriter, new()
    {
        //FIELDS
        protected T_DataObjectDictionary dataObjectDictionary;


        //PROPERTIES
        public virtual T_DataObjectDictionary DataObjectDictionary
        {
            get { return dataObjectDictionary; }

            protected set { dataObjectDictionary = value; }
        }


        //INITIALIZE
        public DataObjectDictionaryReaderProcessBase()
        {
            dataObjectDictionary = default(T_DataObjectDictionary);
        }
        

        //FUNCTIONS
        protected abstract void AddDataObjectToDataObjectDictionary(T_DataObject dataObject);

        protected abstract T_DataObject CreateDataObject(String line);

        protected override bool ReadFile(out string[] fileData)
        {
            if (base.ReadFile(out fileData))
            {
                DataObjectDictionary = new T_DataObjectDictionary();

                return true;
            }
            else
            { return false; }
        }

        protected override void ProcessLine(string line)
        {
            AddDataObjectToDataObjectDictionary(CreateDataObject(line));
        }

        protected override void ResetProcess()
        {
            base.ResetProcess();

            DataObjectDictionary = default(T_DataObjectDictionary);
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.