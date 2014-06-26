//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_5000
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-5000";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AerialWorkAircraftDataCollection : System.Collections.Generic.List<R_4998.AerialWorkAircraftData>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AttachedDocumentCollection : System.Collections.Generic.List<R_0009_000139.AttachedDocument>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class GeneralAuthorizationsCollection : System.Collections.Generic.List<R_4982.GeneralAuthorizations>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class OperationAreaCollection : System.Collections.Generic.List<R_4988.OperationArea>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class CertificationScopeObjectCollection : System.Collections.Generic.List<R_4980.CertificationScopeObject>
	{
	}



	[XmlRoot(ElementName="AOCConductingAerialWorkApplication",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="AOCConductingAerialWorkApplication",Namespace=Declarations.SchemaVersion)]
	public partial class AOCConductingAerialWorkApplication
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
		[XmlElement(ElementName="AviationSecurityCertificateNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AviationSecurityCertificateNumber;
		
		[XmlIgnore]
		public string AviationSecurityCertificateNumber
		{ 
			get { return __AviationSecurityCertificateNumber; }
			set { __AviationSecurityCertificateNumber = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AviationSecurityCertificateDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __AviationSecurityCertificateDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __AviationSecurityCertificateDateSpecified { get { return __AviationSecurityCertificateDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? AviationSecurityCertificateDate
		{ 
			get { return __AviationSecurityCertificateDate; }
			set { __AviationSecurityCertificateDate = value; }
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
		[XmlElement(Type=typeof(R_4980.CertificationScopeObject),ElementName="CertificationScopeObject",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public CertificationScopeObjectCollection __CertificationScopeObjectCollection;
		
		[XmlIgnore]
		public CertificationScopeObjectCollection CertificationScopeObjectCollection
		{
			get
			{
				if (__CertificationScopeObjectCollection == null) __CertificationScopeObjectCollection = new CertificationScopeObjectCollection();
				return __CertificationScopeObjectCollection;
			}
			set {__CertificationScopeObjectCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4982.GeneralAuthorizations),ElementName="GeneralAuthorizations",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public GeneralAuthorizationsCollection __GeneralAuthorizationsCollection;
		
		[XmlIgnore]
		public GeneralAuthorizationsCollection GeneralAuthorizationsCollection
		{
			get
			{
				if (__GeneralAuthorizationsCollection == null) __GeneralAuthorizationsCollection = new GeneralAuthorizationsCollection();
				return __GeneralAuthorizationsCollection;
			}
			set {__GeneralAuthorizationsCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4988.OperationArea),ElementName="OperationArea",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public OperationAreaCollection __OperationAreaCollection;
		
		[XmlIgnore]
		public OperationAreaCollection OperationAreaCollection
		{
			get
			{
				if (__OperationAreaCollection == null) __OperationAreaCollection = new OperationAreaCollection();
				return __OperationAreaCollection;
			}
			set {__OperationAreaCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="OperationAreaDescription",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __OperationAreaDescription;
		
		[XmlIgnore]
		public string OperationAreaDescription
		{ 
			get { return __OperationAreaDescription; }
			set { __OperationAreaDescription = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4998.AerialWorkAircraftData),ElementName="AerialWorkAircraftData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AerialWorkAircraftDataCollection __AerialWorkAircraftDataCollection;
		
		[XmlIgnore]
		public AerialWorkAircraftDataCollection AerialWorkAircraftDataCollection
		{
			get
			{
				if (__AerialWorkAircraftDataCollection == null) __AerialWorkAircraftDataCollection = new AerialWorkAircraftDataCollection();
				return __AerialWorkAircraftDataCollection;
			}
			set {__AerialWorkAircraftDataCollection = value;}
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
		[XmlElement(Type=typeof(R_4692.UnnumberedAttachedDocumentDatasCollection),ElementName="UnnumberedAttachedDocumentDatasCollection",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4692.UnnumberedAttachedDocumentDatasCollection __UnnumberedAttachedDocumentDatasCollection;
		
		[XmlIgnore]
		public R_4692.UnnumberedAttachedDocumentDatasCollection UnnumberedAttachedDocumentDatasCollection
		{
			get {return __UnnumberedAttachedDocumentDatasCollection;}
			set {__UnnumberedAttachedDocumentDatasCollection = value;}
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
		[XmlElement(Type=typeof(AOCConductingAerialWorkApplicationAttachedDocuments),ElementName="AttachedDocuments",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AOCConductingAerialWorkApplicationAttachedDocuments __AttachedDocuments;
		
		[XmlIgnore]
		public AOCConductingAerialWorkApplicationAttachedDocuments AttachedDocuments
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

		public AOCConductingAerialWorkApplication()
		{
		}
	}


	[XmlType(TypeName="AOCConductingAerialWorkApplicationAttachedDocuments",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class AOCConductingAerialWorkApplicationAttachedDocuments
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

		public AOCConductingAerialWorkApplicationAttachedDocuments()
		{
		}
	}
}