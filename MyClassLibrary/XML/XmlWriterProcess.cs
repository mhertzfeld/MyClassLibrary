using System;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;


namespace MyClassLibrary.XML
{
    public class XmlWriterProcess<T_XmlObject>
    {
        //STATIC METHODS
        public static Boolean WriteXml(T_XmlObject xmlObject, String filePath)
        {
            XmlWriterProcess<T_XmlObject> xmlWriterProcess = new XmlWriterProcess<T_XmlObject>();

            return xmlWriterProcess.ExecuteProcess(xmlObject, filePath);
        }


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
        public XmlWriterProcess()
        {
            xmlObject = default(T_XmlObject);

            filePath = null;
        }


        //METHODS
        public virtual Boolean ExecuteProcess()
        {
            if (XmlObject.Equals(default(T_XmlObject))) 
            { throw new InvalidOperationException("XmlObject can not be null"); }

            if (FilePath == null) 
            { throw new NullReferenceException("FilePath can not be null"); }

            xmlObject = default(T_XmlObject);

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
                Trace.WriteLine(exception);

                return false;
            }

            return true;
        }

        public virtual Boolean ExecuteProcess(T_XmlObject XmlObject, String FilePath)
        {
            this.FilePath = FilePath;

            this.XmlObject = XmlObject;

            return ExecuteProcess();
        }
    }
}