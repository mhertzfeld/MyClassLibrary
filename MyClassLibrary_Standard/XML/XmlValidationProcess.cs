using System;
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
            xmlSeverityType = e.Severity;
        }


        //METHODS
        public virtual Boolean ExecuteProcess()
        {
            if (SchemaFileInfo == default(FileInfo)) 
            { throw new NullReferenceException("SchemaFileInfo"); }

            if (XmlFileInfo == default(FileInfo)) 
            { throw new NullReferenceException("XmlFileInfo"); }

            xmlSchema = null;

            xmlSeverityType = null;

            LoadXmlSchema();

            XmlReader xmlReader;

            BuildXmlReader(out xmlReader);

            ValidateXml(xmlReader);

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
        protected virtual void BuildXmlReader(out XmlReader xmlReader)
        {
            xmlReader = XmlReader.Create(XmlFileInfo.OpenRead(), CreateXmlReaderSettings());
        }

        protected virtual XmlReaderSettings CreateXmlReaderSettings()
        {
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.CloseInput = true;
            xmlReaderSettings.Schemas.Add(xmlSchema);
            xmlReaderSettings.ValidationType = ValidationType.Schema;

            return xmlReaderSettings;
        }

        protected virtual void LoadXmlSchema()
        {
            xmlSchema = XmlSchema.Read(SchemaFileInfo.OpenRead(), new ValidationEventHandler(ValidationEventHandler));
        }
        
        protected virtual void ValidateXml(XmlReader xmlReader)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlReader);

            xmlDocument.Validate(new ValidationEventHandler(ValidationEventHandler));
        }
    }
}