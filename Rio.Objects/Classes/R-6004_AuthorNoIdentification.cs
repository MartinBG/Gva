//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_6004
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-6004";
	}




	[XmlType(TypeName="AuthorNoIdentification",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AuthorNoIdentification
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000005.PersonNames),ElementName="PersonNames",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000005.PersonNames PersonNames { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000007.ForeignCitizenNames),ElementName="ForeignCitizenNames",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000007.ForeignCitizenNames ForeignCitizenNames { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(AuthorNoIdentificationEntity),ElementName="Entity",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AuthorNoIdentificationEntity Entity { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(AuthorNoIdentificationForeignEntity),ElementName="ForeignEntity",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AuthorNoIdentificationForeignEntity ForeignEntity { get; set; }

		public AuthorNoIdentification()
		{
		}
	}


	[XmlType(TypeName="AuthorNoIdentificationEntity",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class AuthorNoIdentificationEntity
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="EntityName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string EntityName { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000142.Residence),ElementName="Residence",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000142.Residence Residence { get; set; }

		public AuthorNoIdentificationEntity()
		{
		}
	}


	[XmlType(TypeName="AuthorNoIdentificationForeignEntity",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class AuthorNoIdentificationForeignEntity
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ForeignEntityName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string ForeignEntityName { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000144.ForeignEntityResidence),ElementName="ForeignEntityResidence",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000144.ForeignEntityResidence ForeignEntityResidence { get; set; }

		public AuthorNoIdentificationForeignEntity()
		{
		}
	}
}
