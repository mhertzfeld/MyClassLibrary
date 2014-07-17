using MyClassLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Schema;


namespace MyClassLibrary.XML
{
    public class XmlValidationProcess
    {
        //STATIC METHODS
        public static Boolean ValidateXml(FileInfo xmlFileInfo, FileInfo schemaFileInfo)
        {
            XmlValidationProcess xmlValidationProcess = new XmlValidationProcess();

            return xmlValidationProcess.ExecuteProcess(xmlFileInfo, schemaFileInfo);
        }


        //FIELDS
        protected FileInfo schemaFileInfo;

        protected FileInfo xmlFileInfo;

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

            xmlSchema = null;

            xmlSeverityType = null;
        }


        //EVENTS HANDLERS
        protected void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            Trace.WriteLine(e);

            xmlSeverityType = e.Severity;
        }


        //METHODS
        public virtual Boolean ExecuteProcess()
        {
            if (SchemaFileInfo == default(FileInfo)) 
            { throw new NullReferenceException("SchemaFileInfo"); }

            if (XmlFileInfo == default(FileInfo)) 
            { throw new NullReferenceException("XmlFileInfo"); }

            if (!LoadXmlSchema()) 
            { return false; }

            xmlSchema = null;

            xmlSeverityType = null;

            XmlReader xmlReader;

            if (!BuildXmlReader(out xmlReader)) 
            { return false; }

            if (!ValidateXml(xmlReader)) 
            { return false; }
                        
            if (xmlReader != null)
            { xmlReader.Dispose(); }

            return (XmlSeverityType == null);
        }

        public virtual Boolean ExecuteProcess(FileInfo XmlFileInfo, FileInfo SchemaFileInfo)
        {
            this.SchemaFileInfo = SchemaFileInfo;

            this.XmlFileInfo = XmlFileInfo;

            return ExecuteProcess();
        }
                

        //FUNCTIONS
        protected virtual Boolean BuildXmlReader(out XmlReader xmlReader)
        {
            try
            {
                xmlReader = XmlReader.Create(XmlFileInfo.OpenRead(), CreateXmlReaderSettings());
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                xmlReader = null;

                return false;
            }

            return true;
        }

        protected virtual XmlReaderSettings CreateXmlReaderSettings()
        {
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.CloseInput = true;
            xmlReaderSettings.Schemas.Add(xmlSchema);
            xmlReaderSettings.ValidationType = ValidationType.Schema;

            return xmlReaderSettings;
        }

        protected virtual Boolean LoadXmlSchema()
        {
            try
            {
                xmlSchema = XmlSchema.Read(SchemaFileInfo.OpenRead(), new ValidationEventHandler(ValidationEventHandler));
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                xmlSchema = null;

                return false;
            }

            return true;
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
                Trace.WriteLine(exception);

                return false;
            }

            return true;
        }
    }
}