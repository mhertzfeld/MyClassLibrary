using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace MyClassLibrary.XML
{
    public class XmlReaderProcess<T_XmlObject>
    {
        //STATIC METHODS
        public static void GetXmlObject(FileInfo _FileInfo, out T_XmlObject _XmlObject)
        {
            XmlReaderProcess<T_XmlObject> _XmlReaderProcess = new XmlReaderProcess<T_XmlObject>();
            _XmlReaderProcess.ExecuteProcess();
            _XmlObject = _XmlReaderProcess.xmlObject;
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
        public virtual void ExecuteProcess()
        {
            if (FileInfo == null) 
            { throw new InvalidOperationException("FileInfo can not be null"); }

            xmlObject = default(T_XmlObject);

            using (XmlReader xmlReader = XmlReader.Create(fileInfo.OpenRead(), CreateXmlReaderSettings()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T_XmlObject));
                //Throws and catches File Not Found exception.  This is expected.  http://stackoverflow.com/questions/1127431/xmlserializer-giving-filenotfoundexception-at-constructor

                XmlObject = (T_XmlObject)xmlSerializer.Deserialize(xmlReader);

                xmlReader.Close();
            }
        }

        public virtual void ExecuteProcess(FileInfo FileInfo)
        {
            this.FileInfo = FileInfo;

            ExecuteProcess();
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