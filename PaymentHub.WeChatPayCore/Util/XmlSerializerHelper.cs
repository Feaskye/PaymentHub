using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PaymentHub.WeChatPayCore.Util
{
    internal class XmlSerializerHelper
    {
        // Methods
        internal static T FromXmlString<T>(string xmlString) where T : class
        {
            T local;
            XmlReaderSettings settings = new XmlReaderSettings();
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringReader reader = new StringReader(xmlString))
            {
                using (XmlReader reader2 = XmlReader.Create(reader, settings))
                {
                    local = serializer.Deserialize(reader2) as T;
                }
            }
            return local;
        }

        internal static string GetXmlString<T>(T model) where T : class
        {
            string str;
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");
            XmlWriterSettings settings1 = new XmlWriterSettings();
            settings1.OmitXmlDeclaration = true;
            settings1.Indent = true;
            XmlWriterSettings settings = settings1;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StringWriter writer = new StringWriter())
            {
                using (XmlWriter writer2 = XmlWriter.Create(writer, settings))
                {
                    serializer.Serialize(writer2, model, namespaces);
                    str = writer.ToString();
                }
            }
            return str;
        }
    }

}
