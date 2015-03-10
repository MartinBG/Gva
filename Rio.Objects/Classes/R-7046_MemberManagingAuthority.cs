//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_7046
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-7046";
	}

	[Serializable]
	public enum ModificationTypeNomenclature
	{
		[XmlEnum(Name="Empty")] Empty,
		[XmlEnum(Name="Create")] Create,
		[XmlEnum(Name="Update")] Update,
		[XmlEnum(Name="Delete")] Delete
	}




	[XmlType(TypeName="MemberManagingAuthority",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class MemberManagingAuthority
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="id",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string id { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="modificationType")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_7066.ModificationTypeNomenclature modificationType { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000008.PersonBasicData),ElementName="PersonBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000008.PersonBasicData PersonBasicData { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000013.EntityBasicData),ElementName="EntityBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000013.EntityBasicData EntityBasicData { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="StartMandateMemberManagingAuthority",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? StartMandateMemberManagingAuthority { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="EndMandateMemberManagingAuthority",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? EndMandateMemberManagingAuthority { get; set; }

		public MemberManagingAuthority()
		{
		}
	}
}
