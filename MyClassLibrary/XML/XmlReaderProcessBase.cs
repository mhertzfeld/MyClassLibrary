using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace MyClassLibrary.XML
{
    public class XmlReaderProcess<T_XmlObject>
    {
        //STATIC METHODS
        public static Boolean GetXmlObject(FileInfo fileInfo, out T_XmlObject xmlObject)
        {
            XmlReaderProcess<T_XmlObject> xmlReaderProcess = new XmlReaderProcess<T_XmlObject>();

            if (xmlReaderProcess.ExecuteProcess(fileInfo))
            {
                xmlObject = xmlReaderProcess.XmlObject;

                return true;
            }
            else
            {
                xmlObject = default(T_XmlObject);

                return false;
            }
        }


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
        public XmlReaderProcess()
        {
            fileInfo = null;

            xmlObject = default(T_XmlObject);
        }


        //METHODS
        public virtual Boolean ExecuteProcess()
        {
            if (FileInfo == null) 
            { throw new InvalidOperationException("FileInfo can not be null"); }

            xmlObject = default(T_XmlObject);

            try
            {
                using (XmlReader xmlReader = XmlReader.Create(fileInfo.OpenRead(), CreateXmlReaderSettings()))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T_XmlObject));
                    //Throws and catches File Not Found exception.  This is expected.  http://stackoverflow.com/questions/1127431/xmlserializer-giving-filenotfoundexception-at-constructor

                    XmlObject = (T_XmlObject)xmlSerializer.Deserialize(xmlReader);
                   
                    xmlReader.Close();
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);

                return false;
            }

            return true;
        }

        public virtual Boolean ExecuteProcess(FileInfo FileInfo)
        {
            this.FileInfo = FileInfo;

            return ExecuteProcess();
        }


        //FUNCTIONS
        protected virtual XmlReaderSettings CreateXmlReaderSettings()
        {
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
            xmlReaderSettings.CloseInput = true;

            return xmlReaderSettings;
        }
    }
}