//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000103
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000103";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PersonResidenceStatusCollection : System.Collections.Generic.List<PersonResidenceStatus>
	{
	}



	[XmlRoot(ElementName="PersonResidenceStatusData",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="PersonResidenceStatusData",Namespace=Declarations.SchemaVersion)]
	public partial class PersonResidenceStatusData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="versionDate",DataType="date")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? versionDate { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(PersonResidenceStatuses),ElementName="PersonResidenceStatuses",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PersonResidenceStatuses PersonResidenceStatuses { get; set; }

		public PersonResidenceStatusData()
		{
		}
	}


	[XmlType(TypeName="PersonResidenceStatuses",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class PersonResidenceStatuses
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(PersonResidenceStatus),ElementName="PersonResidenceStatus",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PersonResidenceStatusCollection PersonResidenceStatusCollection { get; set; }

		public PersonResidenceStatuses()
		{
		}
	}


	[XmlType(TypeName="PersonResidenceStatus",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class PersonResidenceStatus
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="Code",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string Code { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string Name { get; set; }

		public PersonResidenceStatus()
		{
		}
	}
}
