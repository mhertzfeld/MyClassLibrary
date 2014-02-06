using MyClassLibrary.Logging;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace MyClassLibrary.XML
{
    public static class XmlUtilities
    {
        public static Boolean SerializeObjectToXmlString<T, T_LogWriter>(T objectToSerialize, out String xmlString)
            where T_LogWriter : Logging.ILogWriter, new()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (StreamWriter streamWriter = new StreamWriter(memoryStream))
                    {
                        xmlSerializer.Serialize(streamWriter, objectToSerialize);

                        memoryStream.Position = 0;

                        using (StreamReader streamReader = new StreamReader(memoryStream))
                        {
                            XmlDocument xmlDocument = new XmlDocument();

                            xmlDocument.Load(streamReader);

                            xmlString = xmlDocument.OuterXml;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LoggingUtilities.WriteLogEntry<T_LogWriter>(exception);

                xmlString = null;

                return false;
            }

            return true;
        }
    }
}