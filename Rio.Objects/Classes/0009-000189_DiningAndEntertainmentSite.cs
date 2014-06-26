//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000189
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000189";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class TouristSitePhoneNumberCollection : System.Collections.Generic.List<string>
	{
	}



	[XmlType(TypeName="DiningAndEntertainmentSite",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class DiningAndEntertainmentSite
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="TouristSiteCategoryCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __TouristSiteCategoryCode;
		
		[XmlIgnore]
		public string TouristSiteCategoryCode
		{ 
			get { return __TouristSiteCategoryCode; }
			set { __TouristSiteCategoryCode = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="TouristSiteCategory",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __TouristSiteCategory;
		
		[XmlIgnore]
		public string TouristSiteCategory
		{ 
			get { return __TouristSiteCategory; }
			set { __TouristSiteCategory = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DiningAndEntertainmentSiteTypeCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DiningAndEntertainmentSiteTypeCode;
		
		[XmlIgnore]
		public string DiningAndEntertainmentSiteTypeCode
		{ 
			get { return __DiningAndEntertainmentSiteTypeCode; }
			set { __DiningAndEntertainmentSiteTypeCode = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DiningAndEntertainmentSiteType",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DiningAndEntertainmentSiteType;
		
		[XmlIgnore]
		public string DiningAndEntertainmentSiteType
		{ 
			get { return __DiningAndEntertainmentSiteType; }
			set { __DiningAndEntertainmentSiteType = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="TouristSiteName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __TouristSiteName;
		
		[XmlIgnore]
		public string TouristSiteName
		{ 
			get { return __TouristSiteName; }
			set { __TouristSiteName = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000180.TouristSiteAddress),ElementName="TouristSiteAddress",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000180.TouristSiteAddress __TouristSiteAddress;
		
		[XmlIgnore]
		public R_0009_000180.TouristSiteAddress TouristSiteAddress
		{
			get {return __TouristSiteAddress;}
			set {__TouristSiteAddress = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(DiningAndEntertainmentSiteTouristSitePhoneNumbers),ElementName="TouristSitePhoneNumbers",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DiningAndEntertainmentSiteTouristSitePhoneNumbers __TouristSitePhoneNumbers;
		
		[XmlIgnore]
		public DiningAndEntertainmentSiteTouristSitePhoneNumbers TouristSitePhoneNumbers
		{
			get {return __TouristSitePhoneNumbers;}
			set {__TouristSitePhoneNumbers = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="TotalSeatsCount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="integer",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __TotalSeatsCount;
		
		[XmlIgnore]
		public string TotalSeatsCount
		{ 
			get { return __TotalSeatsCount; }
			set { __TotalSeatsCount = value; }
		}

		public DiningAndEntertainmentSite()
		{
		}
	}


	[XmlType(TypeName="DiningAndEntertainmentSiteTouristSitePhoneNumbers",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class DiningAndEntertainmentSiteTouristSitePhoneNumbers
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(string),ElementName="TouristSitePhoneNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public TouristSitePhoneNumberCollection __TouristSitePhoneNumberCollection;
		
		[XmlIgnore]
		public TouristSitePhoneNumberCollection TouristSitePhoneNumberCollection
		{
			get
			{
				if (__TouristSitePhoneNumberCollection == null) __TouristSitePhoneNumberCollection = new TouristSitePhoneNumberCollection();
				return __TouristSitePhoneNumberCollection;
			}
			set {__TouristSitePhoneNumberCollection = value;}
		}

		public DiningAndEntertainmentSiteTouristSitePhoneNumbers()
		{
		}
	}
}
