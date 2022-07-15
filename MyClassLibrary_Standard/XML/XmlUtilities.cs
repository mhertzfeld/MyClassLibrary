using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace MyClassLibrary.XML
{
    public static class XmlUtilities
    {
        public static void SerializeObjectToXmlString<T>(T _ObjectToSerialize, out String _XmlString)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(memoryStream))
                {
                    xmlSerializer.Serialize(streamWriter, _ObjectToSerialize);

                    memoryStream.Position = 0;

                    using (StreamReader streamReader = new StreamReader(memoryStream))
                    {
                        XmlDocument xmlDocument = new XmlDocument();

                        xmlDocument.Load(streamReader);

                        _XmlString = xmlDocument.OuterXml;
                    }
                }
            }
        }
    }
}