using MyClassLibrary.Data;
using System;


namespace MyClassLibrary.IO
{
    public abstract class DataObjectCollectionReaderProcessBase<T_DataObject, T_DataObjectCollection, T_LogWriter>
        : IO.ReaderProcessBase<T_LogWriter>
        where T_DataObject : new()
        where T_DataObjectCollection : System.Collections.Generic.ICollection<T_DataObject>, new()
        where T_LogWriter : Logging.ILogWriter, new()
    {
        //FIELDS
        protected T_DataObjectCollection dataObjectCollection;


        //PROPERTIES
        public virtual T_DataObjectCollection DataObjectCollection
        {
            get { return dataObjectCollection; }

            protected set { dataObjectCollection = value; }
        }


        //INITIALIZE
        public DataObjectCollectionReaderProcessBase()
        {
            dataObjectCollection = default(T_DataObjectCollection);
        }


        //FUNCTIONS
        protected virtual void AddDataObject(T_DataObject dataObject)
        {
            dataObjectCollection.Add(dataObject);
        }

        protected override bool ReadFile(out string[] fileData)
        {
            if (base.ReadFile(out fileData))
            {
                DataObjectCollection = new T_DataObjectCollection();

                return true;
            }
            else
            { return false; }
        }

        protected override void ResetProcess()
        {
            base.ResetProcess();

            DataObjectCollection = default(T_DataObjectCollection);
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.