using MyClassLibrary.Data;
using MyClassLibrary.Logging;
using System;
using System.IO;


namespace MyClassLibrary.IO
{
    public abstract class DataObjectReaderProcessBase<T_DataObject>
        : MyClassLibrary.Process.ProcessWorkerBase
        where T_DataObject : IDataObject, new()
    {
        //FIELDS
        protected String filePath;

        protected String[] fileDataStringArray;


        //PROTECTED PROPERTIES
        protected String[] FileDataStringArray
        {
            get { return fileDataStringArray; }
        }

        protected abstract String Deliminter
        {
            get;
        }


        //PUBLIC PROPERTIES
        public virtual String FilePath
        {
            get { return filePath; }

            set
            {
                if (value == default(String))
                {
                    throw new PropertySetToDefaultException("FilePath");
                }

                filePath = value;
            }
        }


        //INITIALIZE
        public DataObjectReaderProcessBase()
        {
            filePath = null;

            fileDataStringArray = null;
        }


        //METHODS
        public override bool ProcessExecution()
        {
            if (FilePath == default(String))
            {
                throw new RequiredPropertySetToDefaultException();
            }

            Boolean returnState = false;

            if (ReadFile(out fileDataStringArray))
            {
                LoopFileDataStringArray();

                returnState = true;
            }

            return returnState;
        }

        public Boolean ProcessExecution(String FilePath)
        {
            this.FilePath = FilePath;

            return ProcessExecution();
        }

        public void RunWorker(String FilePath)
        {
            this.FilePath = FilePath;

            RunWorker();
        }


        //FUNCTIONS
        protected virtual void AddDataObject(T_DataObject dataObject)
        {
            throw new NotImplementedException();
        }

        protected virtual T_DataObject CreateDataObject(String lineFileDataString)
        {
            T_DataObject dataObject = new T_DataObject();
            dataObject.SetDataObjectFieldsAndProperties(lineFileDataString.Split(Deliminter.ToCharArray()));

            return dataObject;
        }

        protected virtual void LoopFileDataStringArray()
        {
            foreach (String lineFileDataString in FileDataStringArray)
            {
                T_DataObject dataObject = CreateDataObject(lineFileDataString);

                AddDataObject(dataObject);
            }
        }

        protected virtual Boolean ReadFile(out String[] fileData)
        {
            Boolean returnState = true;

            try
            {
                fileData = File.ReadAllLines(FilePath);
            }
            catch (Exception exception)
            {
                EnterpriseLibraryLogWriter.WriteExceptionLogEntry(exception);

                returnState = false;

                fileData = null;
            }

            return returnState;
        }
    }
}



//MyClassLibrary
//Copyright (C) 2013 Matthew Hertzfeld https://github.com/mhertzfeld
//This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.
//You should have received a copy of the GNU General Public License along with this program.  If not, see <http://www.gnu.org/licenses/>.