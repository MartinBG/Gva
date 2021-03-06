//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000158
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000158";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class CountryCollection : System.Collections.Generic.List<Country>
	{
	}



	[XmlRoot(ElementName="CountryData",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="CountryData",Namespace=Declarations.SchemaVersion)]
	public partial class CountryData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="versionDate",DataType="date")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? versionDate { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(Countries),ElementName="Countries",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Countries Countries { get; set; }

		public CountryData()
		{
		}
	}


	[XmlType(TypeName="Countries",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class Countries
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(Country),ElementName="Country",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public CountryCollection CountryCollection { get; set; }

		public Countries()
		{
		}
	}


	[XmlType(TypeName="Country",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class Country
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="Code",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string Code { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string Name { get; set; }

		public Country()
		{
		}
	}
}
