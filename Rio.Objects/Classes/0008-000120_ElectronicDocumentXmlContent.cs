//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0008_000120
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/value/0008-000120";
	}




	[XmlType(TypeName="ElectronicDocumentXmlContent",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ElectronicDocumentXmlContent
	{

		[System.Web.Script.Serialization.ScriptIgnore]
        //[XmlAnyElement()]
		//public System.Xml.XmlElement Any;

        [XmlElement(Type = typeof(Abbcdn.Abbcdnconfig), ElementName = "Abbcdnconfig", IsNullable = true, Form = XmlSchemaForm.Qualified, Namespace = Declarations.SchemaVersion)]
        public Abbcdn.Abbcdnconfig Any;

		public ElectronicDocumentXmlContent()
		{
		}
	}
}
