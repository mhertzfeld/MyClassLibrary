using MyClassLibrary;
using MyClassLibrary.Logging;
using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;


namespace MyClassLibrary.XML
{
    public class XmlValidationProcess<T_LogWriter>
        : MyClassLibrary.Process.ProcessWorkerBase
        where T_LogWriter : Logging.ILogWriter, new()
    {
        //FIELDS
        protected FileInfo schemaFileInfo;

        protected FileInfo xmlFileInfo;

        protected XmlReaderSettings xmlReaderSettings;

        protected XmlSchema xmlSchema;

        protected XmlSeverityType? xmlSeverityType;


        //PROPERTIES
        public virtual FileInfo SchemaFileInfo
        {
            get { return schemaFileInfo; }

            set
            {
                if (value == default(FileInfo))
                {
                    throw new PropertySetToDefaultException("SchemaFileInfo");
                }

                schemaFileInfo = value;
            }
        }

        public virtual FileInfo XmlFileInfo
        {
            get { return xmlFileInfo; }

            set
            {
                if (value == default(FileInfo))
                {
                    throw new PropertySetToDefaultException("XmlFileInfo");
                }

                xmlFileInfo = value;
            }
        }

        public virtual XmlReaderSettings XmlReaderSettings
        {
            get { return xmlReaderSettings; }

            protected set { xmlReaderSettings = value; }
        }

        public virtual XmlSchema XmlSchema
        {
            get { return XmlSchema; }

            protected set { xmlSchema = value; }
        }

        public virtual XmlSeverityType? XmlSeverityType
        {
            get { return xmlSeverityType; }

            protected set { xmlSeverityType = value; }
        }


        //INITIALIZE
        public XmlValidationProcess()
        {
            schemaFileInfo = null;
            
            xmlFileInfo = null;

            xmlReaderSettings = null;

            xmlSchema = null;

            xmlSeverityType = null;
        }


        //EVENTS HANDLERS
        protected void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            LoggingUtilities.WriteLogEntry<T_LogWriter>(e.Exception);

            xmlSeverityType = e.Severity;
        }


        //METHODS
        public override bool ProcessExecution()
        {
            if (SchemaFileInfo == default(FileInfo)) { throw new NullReferenceException("SchemaFileInfo"); }

            if (XmlFileInfo == default(FileInfo)) { throw new NullReferenceException("XmlFileInfo"); }

            if (!BuildXmlSchema()) { return false; }

            BuildXmlReaderSettings();

            XmlReader xmlReader;

            if (!CreateXmlReader(out xmlReader)) { return false; }

            if (!ValidateXml(xmlReader)) { return false; }

            return (XmlSeverityType == null);
        }

        public virtual Boolean ProcessExecution(FileInfo XmlFileInfo, FileInfo SchemaFileInfo)
        {
            this.SchemaFileInfo = SchemaFileInfo;

            this.XmlFileInfo = XmlFileInfo;

            return ProcessExecution();
        }
        
        public virtual void RunWorker(FileInfo XmlFileInfo, FileInfo SchemaFileInfo)
        {
            this.SchemaFileInfo = SchemaFileInfo;

            this.XmlFileInfo = XmlFileInfo;

            RunWorker();
        }
        

        //FUNCTIONS
        protected virtual void BuildXmlReaderSettings()
        {
            xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.Schemas.Add(xmlSchema);
            xmlReaderSettings.ValidationType = ValidationType.Schema;
        }

        protected virtual Boolean BuildXmlSchema()
        {
            try
            {
                xmlSchema = XmlSchema.Read(SchemaFileInfo.OpenRead(), new ValidationEventHandler(ValidationEventHandler));
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                xmlSchema = null;

                return false;
            }

            return true;
        }

        protected virtual Boolean CreateXmlReader(out XmlReader xmlReader)
        {
            try
            {
                xmlReader = XmlReader.Create(XmlFileInfo.OpenRead(), xmlReaderSettings);
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                xmlReader = null;

                return false;
            }

            return true;
        }

        protected override void ResetProcess()
        {
            base.ResetProcess();

            xmlReaderSettings = null;

            xmlSchema = null;

            xmlSeverityType = null;
        }

        protected virtual Boolean ValidateXml(XmlReader xmlReader)
        {
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlReader);

                xmlDocument.Validate(new ValidationEventHandler(ValidationEventHandler));    
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                return false;
            }

            return true;
        }
    }
}