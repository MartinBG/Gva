//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000178
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000178";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class TouristSiteCategoryCollection : System.Collections.Generic.List<TouristSiteCategory>
	{
	}



	[XmlRoot(ElementName="TouristSiteCategoryData",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="TouristSiteCategoryData",Namespace=Declarations.SchemaVersion)]
	public partial class TouristSiteCategoryData
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
		[XmlElement(Type=typeof(TouristSiteCategories),ElementName="TouristSiteCategories",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public TouristSiteCategories __TouristSiteCategories;
		
		[XmlIgnore]
		public TouristSiteCategories TouristSiteCategories
		{
			get {return __TouristSiteCategories;}
			set {__TouristSiteCategories = value;}
		}

		public TouristSiteCategoryData()
		{
		}
	}


	[XmlType(TypeName="TouristSiteCategories",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class TouristSiteCategories
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(TouristSiteCategory),ElementName="TouristSiteCategory",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public TouristSiteCategoryCollection __TouristSiteCategoryCollection;
		
		[XmlIgnore]
		public TouristSiteCategoryCollection TouristSiteCategoryCollection
		{
			get
			{
				if (__TouristSiteCategoryCollection == null) __TouristSiteCategoryCollection = new TouristSiteCategoryCollection();
				return __TouristSiteCategoryCollection;
			}
			set {__TouristSiteCategoryCollection = value;}
		}

		public TouristSiteCategories()
		{
		}
	}


	[XmlType(TypeName="TouristSiteCategory",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class TouristSiteCategory
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

		public TouristSiteCategory()
		{
		}
	}
}
