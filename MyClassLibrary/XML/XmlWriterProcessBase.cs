using MyClassLibrary.Logging;
using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;


namespace MyClassLibrary.XML
{
    public abstract class XmlWriterProcessBase<T_XmlObject, T_LogWriter>
        : MyClassLibrary.Process.ProcessWorkerBase
        where T_LogWriter : Logging.ILogWriter, new()
    {
        //FIELDS
        protected String filePath;

        protected T_XmlObject xmlObject;


        //PROPERTIES
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

        public virtual T_XmlObject XmlObject
        {
            get { return xmlObject; }

            set
            {
                if (value.Equals(default(T_XmlObject)))
                {
                    throw new PropertySetToDefaultException("XmlObject");
                }

                xmlObject = value;
            }
        }


        //INITIALIZE
        public XmlWriterProcessBase()
        {
            xmlObject = default(T_XmlObject);

            filePath = null;
        }


        //METHODS
        public override bool ProcessExecution()
        {
            if (XmlObject.Equals(default(T_XmlObject))) { throw new NullReferenceException("XmlObject"); }

            if (FilePath == null) { throw new NullReferenceException("FilePath"); }

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T_XmlObject));

                using (XmlWriter xmlWriter = XmlWriter.Create(FilePath))
                {
                    xmlSerializer.Serialize(xmlWriter, xmlObject);

                    xmlWriter.Close();
                }
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                return false;
            }

            return true;
        }

        public virtual Boolean ProcessExecution(T_XmlObject XmlObject, String FilePath)
        {
            this.FilePath = FilePath;

            this.XmlObject = XmlObject;

            return ProcessExecution();
        }


        //FUNCTIONS
        protected override void ResetProcess()
        {
            base.ResetProcess();

            xmlObject = default(T_XmlObject);
        }
    }
}