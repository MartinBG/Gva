//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000010
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000010";
	}




	[XmlType(TypeName="ForeignCitizenIdentityDocument",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ForeignCitizenIdentityDocument
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DocumentNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DocumentNumber;
		
		[XmlIgnore]
		public string DocumentNumber
		{ 
			get { return __DocumentNumber; }
			set { __DocumentNumber = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DocumentType",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DocumentType;
		
		[XmlIgnore]
		public string DocumentType
		{ 
			get { return __DocumentType; }
			set { __DocumentType = value; }
		}

		public ForeignCitizenIdentityDocument()
		{
		}
	}
}