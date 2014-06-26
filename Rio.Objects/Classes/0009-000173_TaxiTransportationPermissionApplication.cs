//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000173
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000173";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class CarRegistrationNumberCollection : System.Collections.Generic.List<string>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AttachedDocumentCollection : System.Collections.Generic.List<R_0009_000139.AttachedDocument>
	{
	}



	[XmlRoot(ElementName="TaxiTransportationPermissionApplication",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="TaxiTransportationPermissionApplication",Namespace=Declarations.SchemaVersion)]
	public partial class TaxiTransportationPermissionApplication
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
		[XmlElement(ElementName="TaxisInTaxiTransportersApplicationCount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="integer",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __TaxisInTaxiTransportersApplicationCount;
		
		[XmlIgnore]
		public string TaxisInTaxiTransportersApplicationCount
		{ 
			get { return __TaxisInTaxiTransportersApplicationCount; }
			set { __TaxisInTaxiTransportersApplicationCount = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(CarRegistrationNumbers),ElementName="CarRegistrationNumbers",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public CarRegistrationNumbers __CarRegistrationNumbers;
		
		[XmlIgnore]
		public CarRegistrationNumbers CarRegistrationNumbers
		{
			get {return __CarRegistrationNumbers;}
			set {__CarRegistrationNumbers = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(TaxiTransportationPermissionApplicationAttachedDocuments),ElementName="AttachedDocuments",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public TaxiTransportationPermissionApplicationAttachedDocuments __AttachedDocuments;
		
		[XmlIgnore]
		public TaxiTransportationPermissionApplicationAttachedDocuments AttachedDocuments
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

		public TaxiTransportationPermissionApplication()
		{
		}
	}


	[XmlType(TypeName="CarRegistrationNumbers",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class CarRegistrationNumbers
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(string),ElementName="CarRegistrationNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public CarRegistrationNumberCollection __CarRegistrationNumberCollection;
		
		[XmlIgnore]
		public CarRegistrationNumberCollection CarRegistrationNumberCollection
		{
			get
			{
				if (__CarRegistrationNumberCollection == null) __CarRegistrationNumberCollection = new CarRegistrationNumberCollection();
				return __CarRegistrationNumberCollection;
			}
			set {__CarRegistrationNumberCollection = value;}
		}

		public CarRegistrationNumbers()
		{
		}
	}


	[XmlType(TypeName="TaxiTransportationPermissionApplicationAttachedDocuments",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class TaxiTransportationPermissionApplicationAttachedDocuments
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

		public TaxiTransportationPermissionApplicationAttachedDocuments()
		{
		}
	}
}
