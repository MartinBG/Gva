//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4734
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4734";
	}




	[XmlType(TypeName="Derogation",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class Derogation
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DerogationName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DerogationName;
		
		[XmlIgnore]
		public string DerogationName
		{ 
			get { return __DerogationName; }
			set { __DerogationName = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DerogationChosenFlag",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="boolean",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __DerogationChosenFlag;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __DerogationChosenFlagSpecified;
		
		[XmlIgnore]
		public bool DerogationChosenFlag
		{ 
			get { return __DerogationChosenFlag; }
			set { __DerogationChosenFlag = value; __DerogationChosenFlagSpecified = true; }
		}

		public Derogation()
		{
		}
	}
}
