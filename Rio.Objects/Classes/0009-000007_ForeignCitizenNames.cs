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
		public string FirstCyrillic { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="LastCyrillic",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string LastCyrillic { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="OtherCyrillic",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string OtherCyrillic { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PseudonimCyrillic",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string PseudonimCyrillic { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="FirstLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string FirstLatin { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="LastLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string LastLatin { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="OtherLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string OtherLatin { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PseudonimLatin",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string PseudonimLatin { get; set; }

		public ForeignCitizenNames()
		{
		}
	}
}
