﻿using MyClassLibrary;
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
            LoggingUtilities.WriteLogEntry<T_LogWriter>(e.Exception);

            xmlSeverityType = e.Severity;
        }


        //METHODS
        public override bool ProcessExecution()
        {
            if (SchemaFileInfo == default(FileInfo)) 
            { throw new NullReferenceException("SchemaFileInfo"); }

            if (XmlFileInfo == default(FileInfo)) 
            { throw new NullReferenceException("XmlFileInfo"); }

            if (!LoadXmlSchema()) 
            { return false; }

            XmlReader xmlReader;

            if (!BuildXmlReader(out xmlReader)) 
            { return false; }

            if (!ValidateXml(xmlReader)) 
            { return false; }

            MyUtilities.DisposeObject(xmlReader);

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
        protected virtual Boolean BuildXmlReader(out XmlReader xmlReader)
        {
            try
            {
                xmlReader = XmlReader.Create(XmlFileInfo.OpenRead(), CreateXmlReaderSettings());
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

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
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                xmlSchema = null;

                return false;
            }

            return true;
        }

        protected override void ResetProcess()
        {
            base.ResetProcess();

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