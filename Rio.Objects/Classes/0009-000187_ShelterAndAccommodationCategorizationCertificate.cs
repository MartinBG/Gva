//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000187
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000187";
	}




	[XmlRoot(ElementName="ShelterAndAccommodationCategorizationCertificate",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="ShelterAndAccommodationCategorizationCertificate",Namespace=Declarations.SchemaVersion)]
	public partial class ShelterAndAccommodationCategorizationCertificate
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000003.DocumentTypeURI),ElementName="DocumentTypeURI",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000003.DocumentTypeURI __DocumentTypeURI;
		
		[XmlIgnore]
		public R_0009_000003.DocumentTypeURI DocumentTypeURI
		{
			get {return __DocumentTypeURI;}
			set {__DocumentTypeURI = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DocumentTypeName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DocumentTypeName;
		
		[XmlIgnore]
		public string DocumentTypeName
		{ 
			get { return __DocumentTypeName; }
			set { __DocumentTypeName = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000001.DocumentURI),ElementName="DocumentURI",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000001.DocumentURI __DocumentURI;
		
		[XmlIgnore]
		public R_0009_000001.DocumentURI DocumentURI
		{
			get {return __DocumentURI;}
			set {__DocumentURI = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DocumentReceiptOrSigningDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __DocumentReceiptOrSigningDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __DocumentReceiptOrSigningDateSpecified { get { return __DocumentReceiptOrSigningDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? DocumentReceiptOrSigningDate
		{ 
			get { return __DocumentReceiptOrSigningDate; }
			set { __DocumentReceiptOrSigningDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000143.MunicipalityAdministrationLocation),ElementName="MunicipalityAdministrationLocation",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000143.MunicipalityAdministrationLocation __MunicipalityAdministrationLocation;
		
		[XmlIgnore]
		public R_0009_000143.MunicipalityAdministrationLocation MunicipalityAdministrationLocation
		{
			get {return __MunicipalityAdministrationLocation;}
			set {__MunicipalityAdministrationLocation = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000002.ElectronicServiceProviderBasicData),ElementName="ElectronicServiceProviderBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000002.ElectronicServiceProviderBasicData __ElectronicServiceProviderBasicData;
		
		[XmlIgnore]
		public R_0009_000002.ElectronicServiceProviderBasicData ElectronicServiceProviderBasicData
		{
			get {return __ElectronicServiceProviderBasicData;}
			set {__ElectronicServiceProviderBasicData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="TouristSiteCategoryCertificateNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __TouristSiteCategoryCertificateNumber;
		
		[XmlIgnore]
		public string TouristSiteCategoryCertificateNumber
		{ 
			get { return __TouristSiteCategoryCertificateNumber; }
			set { __TouristSiteCategoryCertificateNumber = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="TouristSiteCategoryCertificateIssueDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __TouristSiteCategoryCertificateIssueDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __TouristSiteCategoryCertificateIssueDateSpecified { get { return __TouristSiteCategoryCertificateIssueDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? TouristSiteCategoryCertificateIssueDate
		{ 
			get { return __TouristSiteCategoryCertificateIssueDate; }
			set { __TouristSiteCategoryCertificateIssueDate = value; }
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
		[XmlElement(ElementName="TouristSiteRoomCount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="integer",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __TouristSiteRoomCount;
		
		[XmlIgnore]
		public string TouristSiteRoomCount
		{ 
			get { return __TouristSiteRoomCount; }
			set { __TouristSiteRoomCount = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(ShelterAndAccommodationCategorizationCertificateOwner),ElementName="Owner",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ShelterAndAccommodationCategorizationCertificateOwner __Owner;
		
		[XmlIgnore]
		public ShelterAndAccommodationCategorizationCertificateOwner Owner
		{
			get {return __Owner;}
			set {__Owner = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(ShelterAndAccommodationCategorizationCertificateTenant),ElementName="Tenant",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ShelterAndAccommodationCategorizationCertificateTenant __Tenant;
		
		[XmlIgnore]
		public ShelterAndAccommodationCategorizationCertificateTenant Tenant
		{
			get {return __Tenant;}
			set {__Tenant = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000184.TouristSiteCategoryAllocationOrderNumber),ElementName="TouristSiteCategoryAllocationOrderNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000184.TouristSiteCategoryAllocationOrderNumber __TouristSiteCategoryAllocationOrderNumber;
		
		[XmlIgnore]
		public R_0009_000184.TouristSiteCategoryAllocationOrderNumber TouristSiteCategoryAllocationOrderNumber
		{
			get {return __TouristSiteCategoryAllocationOrderNumber;}
			set {__TouristSiteCategoryAllocationOrderNumber = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="TouristSiteCategoryAllocationOrderDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __TouristSiteCategoryAllocationOrderDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __TouristSiteCategoryAllocationOrderDateSpecified { get { return __TouristSiteCategoryAllocationOrderDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? TouristSiteCategoryAllocationOrderDate
		{ 
			get { return __TouristSiteCategoryAllocationOrderDate; }
			set { __TouristSiteCategoryAllocationOrderDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(ShelterAndAccommodationCategorizationCertificateOfficial),ElementName="Official",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ShelterAndAccommodationCategorizationCertificateOfficial __Official;
		
		[XmlIgnore]
		public ShelterAndAccommodationCategorizationCertificateOfficial Official
		{
			get {return __Official;}
			set {__Official = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000004.XMLDigitalSignature),ElementName="XMLDigitalSignature",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000004.XMLDigitalSignature __XMLDigitalSignature;
		
		[XmlIgnore]
		public R_0009_000004.XMLDigitalSignature XMLDigitalSignature
		{
			get {return __XMLDigitalSignature;}
			set {__XMLDigitalSignature = value;}
		}

		public ShelterAndAccommodationCategorizationCertificate()
		{
		}
	}


	[XmlType(TypeName="ShelterAndAccommodationCategorizationCertificateOwner",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ShelterAndAccommodationCategorizationCertificateOwner
	{

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
		[XmlElement(Type=typeof(ShelterAndAccommodationCategorizationCertificateOwnerBulgarianEntity),ElementName="BulgarianEntity",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ShelterAndAccommodationCategorizationCertificateOwnerBulgarianEntity __BulgarianEntity;
		
		[XmlIgnore]
		public ShelterAndAccommodationCategorizationCertificateOwnerBulgarianEntity BulgarianEntity
		{
			get {return __BulgarianEntity;}
			set {__BulgarianEntity = value;}
		}

		public ShelterAndAccommodationCategorizationCertificateOwner()
		{
		}
	}


	[XmlType(TypeName="ShelterAndAccommodationCategorizationCertificateOwnerBulgarianEntity",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ShelterAndAccommodationCategorizationCertificateOwnerBulgarianEntity
	{

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

		public ShelterAndAccommodationCategorizationCertificateOwnerBulgarianEntity()
		{
		}
	}


	[XmlType(TypeName="ShelterAndAccommodationCategorizationCertificateTenant",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ShelterAndAccommodationCategorizationCertificateTenant
	{

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
		[XmlElement(Type=typeof(ShelterAndAccommodationCategorizationCertificateTenantBulgarianEntity),ElementName="BulgarianEntity",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ShelterAndAccommodationCategorizationCertificateTenantBulgarianEntity __BulgarianEntity;
		
		[XmlIgnore]
		public ShelterAndAccommodationCategorizationCertificateTenantBulgarianEntity BulgarianEntity
		{
			get {return __BulgarianEntity;}
			set {__BulgarianEntity = value;}
		}

		public ShelterAndAccommodationCategorizationCertificateTenant()
		{
		}
	}


	[XmlType(TypeName="ShelterAndAccommodationCategorizationCertificateTenantBulgarianEntity",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ShelterAndAccommodationCategorizationCertificateTenantBulgarianEntity
	{

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

		public ShelterAndAccommodationCategorizationCertificateTenantBulgarianEntity()
		{
		}
	}


	[XmlType(TypeName="ShelterAndAccommodationCategorizationCertificateOfficial",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ShelterAndAccommodationCategorizationCertificateOfficial
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000005.PersonNames),ElementName="PersonNames",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000005.PersonNames __PersonNames;
		
		[XmlIgnore]
		public R_0009_000005.PersonNames PersonNames
		{
			get {return __PersonNames;}
			set {__PersonNames = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000007.ForeignCitizenNames),ElementName="ForeignCitizenNames",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000007.ForeignCitizenNames __ForeignCitizenNames;
		
		[XmlIgnore]
		public R_0009_000007.ForeignCitizenNames ForeignCitizenNames
		{
			get {return __ForeignCitizenNames;}
			set {__ForeignCitizenNames = value;}
		}

		public ShelterAndAccommodationCategorizationCertificateOfficial()
		{
		}
	}
}