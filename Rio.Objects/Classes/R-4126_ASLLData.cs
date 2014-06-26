//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4126
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4126";
	}




	[XmlType(TypeName="ASLLData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ASLLData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ASLLCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ASLLCode;
		
		[XmlIgnore]
		public string ASLLCode
		{ 
			get { return __ASLLCode; }
			set { __ASLLCode = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ASLLName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ASLLName;
		
		[XmlIgnore]
		public string ASLLName
		{ 
			get { return __ASLLName; }
			set { __ASLLName = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ASLLNameCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ASLLNameCode;
		
		[XmlIgnore]
		public string ASLLNameCode
		{ 
			get { return __ASLLNameCode; }
			set { __ASLLNameCode = value; }
		}

		public ASLLData()
		{
		}
	}
}