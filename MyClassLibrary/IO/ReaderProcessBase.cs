using System;
using System.Diagnostics;
using System.IO;


namespace MyClassLibrary.IO
{
    public abstract class ReaderProcessBase
    {
        //FIELDS
        protected String deliminter;

        protected FileInfo fileInfo;

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

        public virtual FileInfo FileInfo
        {
            get { return fileInfo; }

            set
            {
                if (value == default(FileInfo))
                {
                    throw new PropertySetToDefaultException("FileInfo");
                }

                fileInfo = value;
            }
        }


        //INITIALIZE
        public ReaderProcessBase()
        {
            deliminter = null;

            fileDataStringArray = null;

            fileInfo = null;
        }


        //METHODS
        public virtual Boolean ExecuteProcess()
        {
            if (fileInfo == default(FileInfo))
            { throw new InvalidOperationException("FileInfo"); }

            Boolean returnState = false;

            if (ReadFile(out fileDataStringArray))
            {
                LoopFileDataStringArray();

                returnState = true;
            }

            return returnState;
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
                fileData = File.ReadAllLines(FileInfo.FullName);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                returnState = false;

                fileData = null;
            }

            return returnState;
        }
    }
}