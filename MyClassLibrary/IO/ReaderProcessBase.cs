using System;
using System.Diagnostics;
using System.IO;


namespace MyClassLibrary.IO
{
    public abstract class ReaderProcessBase
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
        public virtual Boolean ExecuteProcess()
        {
            if (FilePath == default(String))
            {
                throw new ValueOutOfRangeException("FilePath");
            }

            Boolean returnState = false;

            if (ReadFile(out fileDataStringArray))
            {
                LoopFileDataStringArray();

                returnState = true;
            }

            return returnState;
        }

        public virtual Boolean ExecuteProcess(String FilePath)
        {
            this.FilePath = FilePath;

            return ExecuteProcess();
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
                Trace.WriteLine(exception);

                returnState = false;

                fileData = null;
            }

            return returnState;
        }
    }
}