using Common.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rio.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rio.Data.Utils
{
    public static class RioObjectUtils
    {
        public static Encoding DefaultEncoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }

        public static byte[] GetBytesFromJObject(string docTypeUri, JObject jObject)
        {
            string contentStr = JsonConvert.SerializeObject(jObject);

            var rioObj = Newtonsoft.Json.JsonConvert.DeserializeObject(contentStr, GetTypeByDocTypeUri(docTypeUri));

            return XmlSerializerUtils.XmlSerializeObjectToBytes(rioObj);
        }

        public static JObject GetJObjectFromBytes(string docTypeUri, byte[] content)
        {
            var rioObj = XmlSerializerUtils.XmlDeserializeFromBytes(GetTypeByDocTypeUri(docTypeUri), content);

            string contentToString = Newtonsoft.Json.JsonConvert.SerializeObject(rioObj);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(contentToString);
        }

        public static byte[] GetEmptyRioObjectBytes(string docTypeUri)
        {
            var rioObj = Activator.CreateInstance(GetTypeByDocTypeUri(docTypeUri));

            return XmlSerializerUtils.XmlSerializeObjectToBytes(rioObj);
        }

        private static Type GetTypeByDocTypeUri(string docTypeUri)
        {
            RioDocumentMetadata rioDocumentMetadata = RioDocumentMetadata.GetMetadataByDocumentTypeURI(docTypeUri);

            return rioDocumentMetadata.RioObjectType;
        }
    }
}
