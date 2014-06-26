//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000156
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000156";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class GenderCollection : System.Collections.Generic.List<Gender>
	{
	}



	[XmlRoot(ElementName="GenderData",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="GenderData",Namespace=Declarations.SchemaVersion)]
	public partial class GenderData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="versionDate",DataType="date")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __versionDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __versionDateSpecified { get { return __versionDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? versionDate
		{ 
		get { return __versionDate; }
		set { __versionDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(Genders),ElementName="Genders",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Genders __Genders;
		
		[XmlIgnore]
		public Genders Genders
		{
			get {return __Genders;}
			set {__Genders = value;}
		}

		public GenderData()
		{
		}
	}


	[XmlType(TypeName="Genders",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class Genders
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(Gender),ElementName="Gender",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public GenderCollection __GenderCollection;
		
		[XmlIgnore]
		public GenderCollection GenderCollection
		{
			get
			{
				if (__GenderCollection == null) __GenderCollection = new GenderCollection();
				return __GenderCollection;
			}
			set {__GenderCollection = value;}
		}

		public Genders()
		{
		}
	}


	[XmlType(TypeName="Gender",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class Gender
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="Code",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Code;
		
		[XmlIgnore]
		public string Code
		{ 
			get { return __Code; }
			set { __Code = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Name;
		
		[XmlIgnore]
		public string Name
		{ 
			get { return __Name; }
			set { __Name = value; }
		}

		public Gender()
		{
		}
	}
}