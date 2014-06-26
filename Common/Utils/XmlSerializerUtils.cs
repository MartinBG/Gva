using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public static class XmlSerializerUtils
    {
        #region Public

        public static Encoding DefaultEncoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }

        public static string XmlSerializeToString<T>(T document)
        {
            return XmlSerializeToStringInternal(typeof(T), document);
        }

        public static string XmlSerializeObjectToString(object document)
        {
            return XmlSerializeToStringInternal(document.GetType(), document);
        }

        public static byte[] XmlSerializeToBytes<T>(T document)
        {
            return XmlSerializeToBytesInternal(typeof(T), document);
        }

        public static byte[] XmlSerializeObjectToBytes(object document)
        {
            return XmlSerializeToBytesInternal(document.GetType(), document);
        }

        public static T XmlDeserializeFromString<T>(string documentXml)
        {
            if (string.IsNullOrWhiteSpace(documentXml))
                throw new ArgumentNullException("documentXml should not be empty.");

            return (T)XmlDeserializeFromStringInternal(typeof(T), documentXml);
        }

        public static object XmlDeserializeFromString(Type objectType, string documentXml)
        {
            return XmlDeserializeFromStringInternal(objectType, documentXml);
        }

        public static T XmlDeserializeFromBytes<T>(byte[] bytes)
        {
            return (T)XmlDeserializeFromBytesInternal(typeof(T), bytes);
        }

        public static object XmlDeserializeFromBytes(Type objectType, byte[] bytes)
        {
            return XmlDeserializeFromBytesInternal(objectType, bytes);
        }

        #endregion

        #region Private

        private static byte[] XmlSerializeToBytesInternal(Type type, object document)
        {
            if (document == null)
                throw new ArgumentNullException("document should not be null.");

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (System.Xml.XmlTextWriter xmlWriter = new System.Xml.XmlTextWriter(ms, DefaultEncoding))
                {
                    System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);
                    serializer.Serialize(xmlWriter, document);
                }

                return ms.ToArray();
            }
        }

        private static string XmlSerializeToStringInternal(Type type, object document)
        {
            byte[] bytes = XmlSerializeToBytesInternal(type, document);

            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes))
            using (System.IO.StreamReader sr = new System.IO.StreamReader(ms, DefaultEncoding))
            {
                return sr.ReadToEnd();
            }
        }

        private static object XmlDeserializeFromStringInternal(Type type, string xml)
        {
            using (System.IO.StringReader sr = new System.IO.StringReader(xml))
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);
                return serializer.Deserialize(sr);
            }
        }

        private static object XmlDeserializeFromBytesInternal(Type type, byte[] bytes)
        {
            using (System.IO.Stream s = new System.IO.MemoryStream(bytes))
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);
                return serializer.Deserialize(s);
            }
        }

        #endregion
    }
}
