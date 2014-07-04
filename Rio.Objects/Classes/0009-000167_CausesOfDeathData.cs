//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000167
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000167";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class CDCollection : System.Collections.Generic.List<CD>
	{
	}



	[XmlRoot(ElementName="CausesOfDeathData",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="CausesOfDeathData",Namespace=Declarations.SchemaVersion)]
	public partial class CausesOfDeathData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="versionDate",DataType="date")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? versionDate { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(CauseOfDeath),ElementName="CauseOfDeath",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public CauseOfDeath CauseOfDeath { get; set; }

		public CausesOfDeathData()
		{
		}
	}


	[XmlType(TypeName="CauseOfDeath",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class CauseOfDeath
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(CD),ElementName="CD",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public CDCollection CDCollection { get; set; }

		public CauseOfDeath()
		{
		}
	}


	[XmlType(TypeName="CD",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class CD
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="Code",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string Code { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string Name { get; set; }

		public CD()
		{
		}
	}
}
