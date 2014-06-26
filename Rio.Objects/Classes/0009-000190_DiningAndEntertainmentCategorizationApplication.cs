//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000190
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000190";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AttachedDocumentCollection : System.Collections.Generic.List<R_0009_000139.AttachedDocument>
	{
	}



	[XmlRoot(ElementName="DiningAndEntertainmentCategorizationApplication",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="DiningAndEntertainmentCategorizationApplication",Namespace=Declarations.SchemaVersion)]
	public partial class DiningAndEntertainmentCategorizationApplication
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000152.ElectronicAdministrativeServiceHeader),ElementName="ElectronicAdministrativeServiceHeader",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000152.ElectronicAdministrativeServiceHeader __ElectronicAdministrativeServiceHeader;
		
		[XmlIgnore]
		public R_0009_000152.ElectronicAdministrativeServiceHeader ElectronicAdministrativeServiceHeader
		{
			get {return __ElectronicAdministrativeServiceHeader;}
			set {__ElectronicAdministrativeServiceHeader = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ServiceTermType",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ServiceTermType;
		
		[XmlIgnore]
		public string ServiceTermType
		{ 
			get { return __ServiceTermType; }
			set { __ServiceTermType = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000141.ServiceApplicantReceiptData),ElementName="ServiceApplicantReceiptData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000141.ServiceApplicantReceiptData __ServiceApplicantReceiptData;
		
		[XmlIgnore]
		public R_0009_000141.ServiceApplicantReceiptData ServiceApplicantReceiptData
		{
			get {return __ServiceApplicantReceiptData;}
			set {__ServiceApplicantReceiptData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000189.DiningAndEntertainmentSite),ElementName="DiningAndEntertainmentSite",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000189.DiningAndEntertainmentSite __DiningAndEntertainmentSite;
		
		[XmlIgnore]
		public R_0009_000189.DiningAndEntertainmentSite DiningAndEntertainmentSite
		{
			get {return __DiningAndEntertainmentSite;}
			set {__DiningAndEntertainmentSite = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_3312.OwnerOfTouristSite),ElementName="OwnerOfTouristSite",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_3312.OwnerOfTouristSite __OwnerOfTouristSite;
		
		[XmlIgnore]
		public R_3312.OwnerOfTouristSite OwnerOfTouristSite
		{
			get {return __OwnerOfTouristSite;}
			set {__OwnerOfTouristSite = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(DiningAndEntertainmentCategorizationApplicationAttachedDocuments),ElementName="AttachedDocuments",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DiningAndEntertainmentCategorizationApplicationAttachedDocuments __AttachedDocuments;
		
		[XmlIgnore]
		public DiningAndEntertainmentCategorizationApplicationAttachedDocuments AttachedDocuments
		{
			get {return __AttachedDocuments;}
			set {__AttachedDocuments = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000153.ElectronicAdministrativeServiceFooter),ElementName="ElectronicAdministrativeServiceFooter",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000153.ElectronicAdministrativeServiceFooter __ElectronicAdministrativeServiceFooter;
		
		[XmlIgnore]
		public R_0009_000153.ElectronicAdministrativeServiceFooter ElectronicAdministrativeServiceFooter
		{
			get {return __ElectronicAdministrativeServiceFooter;}
			set {__ElectronicAdministrativeServiceFooter = value;}
		}

		public DiningAndEntertainmentCategorizationApplication()
		{
		}
	}


	[XmlType(TypeName="DiningAndEntertainmentCategorizationApplicationAttachedDocuments",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class DiningAndEntertainmentCategorizationApplicationAttachedDocuments
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000139.AttachedDocument),ElementName="AttachedDocument",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AttachedDocumentCollection __AttachedDocumentCollection;
		
		[XmlIgnore]
		public AttachedDocumentCollection AttachedDocumentCollection
		{
			get
			{
				if (__AttachedDocumentCollection == null) __AttachedDocumentCollection = new AttachedDocumentCollection();
				return __AttachedDocumentCollection;
			}
			set {__AttachedDocumentCollection = value;}
		}

		public DiningAndEntertainmentCategorizationApplicationAttachedDocuments()
		{
		}
	}
}