using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aop.Api.Utils
{
    public class FedHelper
    {
        public static List<NOMv5.nom> GetFedDocumentNomenclatures(string path = "~/FedNoms/nom.xml")
        {
            var xmlPath = System.Web.Hosting.HostingEnvironment.MapPath(path);
            var nomXml = System.IO.File.ReadAllText(xmlPath);
            var nomObj = XmlSerializerUtils.XmlDeserializeFromString<NOMv5.nomenclature>(nomXml);

            return nomObj.nom;
        }

        public static List<NUTv5.item> GetFedDocumentNuts(string path = "~/FedNoms/nuts.xml")
        {
            var xmlPath = System.Web.Hosting.HostingEnvironment.MapPath(path);
            var nutXml = System.IO.File.ReadAllText(xmlPath);
            var nutObj = XmlSerializerUtils.XmlDeserializeFromString<NUTv5.nuts>(nutXml);

            return nutObj.item;
        }
    }
}
