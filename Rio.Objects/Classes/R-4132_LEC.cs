//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4132
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4132";
	}




	[XmlType(TypeName="LEC",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class LEC
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="LECCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __LECCode;
		
		[XmlIgnore]
		public string LECCode
		{ 
			get { return __LECCode; }
			set { __LECCode = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="LECName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __LECName;
		
		[XmlIgnore]
		public string LECName
		{ 
			get { return __LECName; }
			set { __LECName = value; }
		}

		public LEC()
		{
		}
	}
}
