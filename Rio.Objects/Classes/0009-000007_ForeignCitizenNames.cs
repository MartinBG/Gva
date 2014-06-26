//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000007
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000007";
	}




	[XmlType(TypeName="ForeignCitizenNames",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ForeignCitizenNames
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="FirstCyrillic",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __FirstCyrillic;
		
		[XmlIgnore]
		public string FirstCyrillic
		{ 
			get { return __FirstCyrillic; }
			set { __FirstCyrillic = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="LastCyrillic",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __LastCyrillic;
		
		[XmlIgnore]
		public string LastCyrillic
		{ 
			get { return __LastCyrillic; }
			set { __LastCyrillic = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="OtherCyrillic",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __OtherCyrillic;
		
		[XmlIgnore]
		public string OtherCyrillic
		{ 
			get { return __OtherCyrillic; }
			set { __OtherCyrillic = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PseudonimCyrillic",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PseudonimCyrillic;
		
		[XmlIgnore]
		public string PseudonimCyrillic
		{ 
			get { return __PseudonimCyrillic; }
			set { __PseudonimCyrillic = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="FirstLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __FirstLatin;
		
		[XmlIgnore]
		public string FirstLatin
		{ 
			get { return __FirstLatin; }
			set { __FirstLatin = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="LastLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __LastLatin;
		
		[XmlIgnore]
		public string LastLatin
		{ 
			get { return __LastLatin; }
			set { __LastLatin = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="OtherLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __OtherLatin;
		
		[XmlIgnore]
		public string OtherLatin
		{ 
			get { return __OtherLatin; }
			set { __OtherLatin = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PseudonimLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PseudonimLatin;
		
		[XmlIgnore]
		public string PseudonimLatin
		{ 
			get { return __PseudonimLatin; }
			set { __PseudonimLatin = value; }
		}

		public ForeignCitizenNames()
		{
		}
	}
}
