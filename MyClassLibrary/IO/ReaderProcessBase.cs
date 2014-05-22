using MyClassLibrary.Data;
using MyClassLibrary.Logging;
using System;
using System.IO;


namespace MyClassLibrary.IO
{
    public abstract class ReaderProcessBase<T_LogWriter>
        : MyClassLibrary.Process.ProcessWorkerBase
        where T_LogWriter : Logging.ILogWriter, new()
    {
        //FIELDS
        protected String deliminter;

        protected String filePath;

        protected String[] fileDataStringArray;


        //PROPERTIES
        public virtual String Deliminter
        {
            get { return deliminter; }

            set
            {
                if (value == default(String))
                { throw new PropertySetToDefaultException("Deliminter"); }

                deliminter = value;
            }
        }

        public virtual String[] FileDataStringArray
        {
            get { return fileDataStringArray; }
        }

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
        public ReaderProcessBase()
        {
            deliminter = null;

            fileDataStringArray = null;

            filePath = null;
        }


        //METHODS
        public override bool ProcessExecution()
        {
            if (FilePath == default(String))
            {
                throw new ValueOutOfRangeException("FilePath");
            }

            ResetProcess();

            Boolean returnState = false;

            if (ReadFile(out fileDataStringArray))
            {
                LoopFileDataStringArray();

                returnState = true;
            }

            return returnState;
        }

        public virtual Boolean ProcessExecution(String FilePath)
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
        protected virtual void LoopFileDataStringArray()
        {
            foreach (String lineFileDataString in FileDataStringArray)
            { ProcessLine(lineFileDataString); }
        }

        protected abstract void ProcessLine(String line);

        protected virtual Boolean ReadFile(out String[] fileData)
        {
            Boolean returnState = true;

            try
            {
                fileData = File.ReadAllLines(FilePath);
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

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