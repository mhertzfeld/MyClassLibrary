using MyClassLibrary.Logging;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace MyClassLibrary.XML
{
    public class XmlReaderProcessBase<T_XmlObject, T_LogWriter>
        : MyClassLibrary.Process.ProcessWorkerBase
        where T_LogWriter : Logging.ILogWriter, new()
    {
        //FIELDS
        protected FileInfo fileInfo;

        protected T_XmlObject xmlObject;


        //PROPERTIES
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

        public virtual T_XmlObject XmlObject
        {
            get { return xmlObject; }

            protected set { xmlObject = value; }
        }


        //INITIALIZE
        public XmlReaderProcessBase()
        {
            fileInfo = null;

            xmlObject = default(T_XmlObject);
        }


        //METHODS
        public override bool ProcessExecution()
        {
            if (FileInfo == default(FileInfo)) { throw new NullReferenceException("FileInfo"); }

            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T_XmlObject));

                using (XmlReader xmlReader = XmlReader.Create(fileInfo.OpenRead()))
                {
                    if (xmlSerializer.CanDeserialize(xmlReader))
                    {
                        T_XmlObject customerOrderDownloadXml = (T_XmlObject)xmlSerializer.Deserialize(xmlReader);
                    }
                }
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                return false;
            }

            return true;
        }

        public virtual Boolean ProcessExecution(FileInfo FileInfo)
        {
            this.FileInfo = FileInfo;

            return ProcessExecution();
        }

        public virtual void RunWorker(FileInfo FileInfo)
        {
            RunWorker();
        }


        //FUNCTIONS
        protected override void ResetProcess()
        {
            base.ResetProcess();

            xmlObject = default(T_XmlObject);
        }
    }
}