//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4862
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4862";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AttachedDocumentCollection : System.Collections.Generic.List<R_0009_000139.AttachedDocument>
	{
	}



	[XmlRoot(ElementName="AviationTrainingCenterAnotherCountryApplication",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="AviationTrainingCenterAnotherCountryApplication",Namespace=Declarations.SchemaVersion)]
	public partial class AviationTrainingCenterAnotherCountryApplication
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
		[XmlElement(ElementName="ApplicantType",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ApplicantType;
		
		[XmlIgnore]
		public string ApplicantType
		{ 
			get { return __ApplicantType; }
			set { __ApplicantType = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000013.EntityBasicData),ElementName="EntityBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000013.EntityBasicData __EntityBasicData;
		
		[XmlIgnore]
		public R_0009_000013.EntityBasicData EntityBasicData
		{
			get {return __EntityBasicData;}
			set {__EntityBasicData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000142.Residence),ElementName="Residence",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000142.Residence __Residence;
		
		[XmlIgnore]
		public R_0009_000142.Residence Residence
		{
			get {return __Residence;}
			set {__Residence = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000172.EntityManagementAddress),ElementName="EntityManagementAddress",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000172.EntityManagementAddress __EntityManagementAddress;
		
		[XmlIgnore]
		public R_0009_000172.EntityManagementAddress EntityManagementAddress
		{
			get {return __EntityManagementAddress;}
			set {__EntityManagementAddress = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000014.ForeignEntityBasicData),ElementName="ForeignEntityBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000014.ForeignEntityBasicData __ForeignEntityBasicData;
		
		[XmlIgnore]
		public R_0009_000014.ForeignEntityBasicData ForeignEntityBasicData
		{
			get {return __ForeignEntityBasicData;}
			set {__ForeignEntityBasicData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000144.ForeignEntityResidence),ElementName="ForeignEntityResidence",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000144.ForeignEntityResidence __ForeignEntityResidence;
		
		[XmlIgnore]
		public R_0009_000144.ForeignEntityResidence ForeignEntityResidence
		{
			get {return __ForeignEntityResidence;}
			set {__ForeignEntityResidence = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4584.ContactInformation),ElementName="ContactInformation",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4584.ContactInformation __ContactInformation;
		
		[XmlIgnore]
		public R_4584.ContactInformation ContactInformation
		{
			get {return __ContactInformation;}
			set {__ContactInformation = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4702.RepresentativeAuthorizedPerson),ElementName="RepresentativeAuthorizedPerson",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4702.RepresentativeAuthorizedPerson __RepresentativeAuthorizedPerson;
		
		[XmlIgnore]
		public R_4702.RepresentativeAuthorizedPerson RepresentativeAuthorizedPerson
		{
			get {return __RepresentativeAuthorizedPerson;}
			set {__RepresentativeAuthorizedPerson = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="OrganizationCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __OrganizationCode;
		
		[XmlIgnore]
		public string OrganizationCode
		{ 
			get { return __OrganizationCode; }
			set { __OrganizationCode = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="CountryGRAOCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CountryGRAOCode;
		
		[XmlIgnore]
		public string CountryGRAOCode
		{ 
			get { return __CountryGRAOCode; }
			set { __CountryGRAOCode = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="CountryName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CountryName;
		
		[XmlIgnore]
		public string CountryName
		{ 
			get { return __CountryName; }
			set { __CountryName = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4848.Leadership),ElementName="Leadership",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4848.Leadership __Leadership;
		
		[XmlIgnore]
		public R_4848.Leadership Leadership
		{
			get {return __Leadership;}
			set {__Leadership = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ActivityStartDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __ActivityStartDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ActivityStartDateSpecified { get { return __ActivityStartDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? ActivityStartDate
		{ 
			get { return __ActivityStartDate; }
			set { __ActivityStartDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4858.Premises),ElementName="Accommodation",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4858.Premises __Accommodation;
		
		[XmlIgnore]
		public R_4858.Premises Accommodation
		{
			get {return __Accommodation;}
			set {__Accommodation = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4858.Premises),ElementName="Premises",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4858.Premises __Premises;
		
		[XmlIgnore]
		public R_4858.Premises Premises
		{
			get {return __Premises;}
			set {__Premises = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_3994.AttachedDocumentDatasCollection),ElementName="AttachedDocumentDatasCollection",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_3994.AttachedDocumentDatasCollection __AttachedDocumentDatasCollection;
		
		[XmlIgnore]
		public R_3994.AttachedDocumentDatasCollection AttachedDocumentDatasCollection
		{
			get {return __AttachedDocumentDatasCollection;}
			set {__AttachedDocumentDatasCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4696.EAURecipientsAttachedDocumentDatasCollection),ElementName="EAURecipientsAttachedDocumentDatasCollection",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4696.EAURecipientsAttachedDocumentDatasCollection __EAURecipientsAttachedDocumentDatasCollection;
		
		[XmlIgnore]
		public R_4696.EAURecipientsAttachedDocumentDatasCollection EAURecipientsAttachedDocumentDatasCollection
		{
			get {return __EAURecipientsAttachedDocumentDatasCollection;}
			set {__EAURecipientsAttachedDocumentDatasCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ConsentReceivingElectronicStatements",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="boolean",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ConsentReceivingElectronicStatements;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ConsentReceivingElectronicStatementsSpecified;
		
		[XmlIgnore]
		public bool ConsentReceivingElectronicStatements
		{ 
			get { return __ConsentReceivingElectronicStatements; }
			set { __ConsentReceivingElectronicStatements = value; __ConsentReceivingElectronicStatementsSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(AviationTrainingCenterAnotherCountryApplicationAttachedDocuments),ElementName="AttachedDocuments",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AviationTrainingCenterAnotherCountryApplicationAttachedDocuments __AttachedDocuments;
		
		[XmlIgnore]
		public AviationTrainingCenterAnotherCountryApplicationAttachedDocuments AttachedDocuments
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

		public AviationTrainingCenterAnotherCountryApplication()
		{
		}
	}


	[XmlType(TypeName="AviationTrainingCenterAnotherCountryApplicationAttachedDocuments",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class AviationTrainingCenterAnotherCountryApplicationAttachedDocuments
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

		public AviationTrainingCenterAnotherCountryApplicationAttachedDocuments()
		{
		}
	}
}