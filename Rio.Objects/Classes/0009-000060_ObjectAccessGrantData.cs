//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000060
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000060";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AccessGrantCollection : System.Collections.Generic.List<AccessGrant>
	{
	}



	[XmlType(TypeName="ObjectAccessGrantData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ObjectAccessGrantData
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(AccessGrant),ElementName="AccessGrant",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AccessGrantCollection AccessGrantCollection { get; set; }

		public ObjectAccessGrantData()
		{
		}
	}


	[XmlType(TypeName="AccessGrant",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class AccessGrant
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ObjectID",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string ObjectID { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ObjectType",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string ObjectType { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000027.AISUserBasicData),ElementName="Grantee",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000027.AISUserBasicData Grantee { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(SpecificParameters),ElementName="SpecificParameters",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SpecificParameters SpecificParameters { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000032.AISObjectCreationData),ElementName="ObjectCreationData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000032.AISObjectCreationData ObjectCreationData { get; set; }

		public AccessGrant()
		{
		}
	}


	[XmlType(TypeName="SpecificParameters",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class SpecificParameters
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAnyElement()]
		public System.Xml.XmlElement[] Any;

		public SpecificParameters()
		{
		}
	}
}
