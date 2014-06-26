//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4470
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4470";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AttachedDocumentCollection : System.Collections.Generic.List<R_0009_000139.AttachedDocument>
	{
	}



	[XmlRoot(ElementName="AircraftAirworthinessCertificateApplication",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="AircraftAirworthinessCertificateApplication",Namespace=Declarations.SchemaVersion)]
	public partial class AircraftAirworthinessCertificateApplication
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
		[XmlElement(Type=typeof(R_0009_000015.ElectronicServiceRecipient),ElementName="ElectronicServiceRecipient",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000015.ElectronicServiceRecipient __ElectronicServiceRecipient;
		
		[XmlIgnore]
		public R_0009_000015.ElectronicServiceRecipient ElectronicServiceRecipient
		{
			get {return __ElectronicServiceRecipient;}
			set {__ElectronicServiceRecipient = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ApplicantAviationalOperatorIndication",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="boolean",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ApplicantAviationalOperatorIndication;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ApplicantAviationalOperatorIndicationSpecified;
		
		[XmlIgnore]
		public bool ApplicantAviationalOperatorIndication
		{ 
			get { return __ApplicantAviationalOperatorIndication; }
			set { __ApplicantAviationalOperatorIndication = value; __ApplicantAviationalOperatorIndicationSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4294.AviationalOperatorBasicData),ElementName="AviationalOperatorBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4294.AviationalOperatorBasicData __AviationalOperatorBasicData;
		
		[XmlIgnore]
		public R_4294.AviationalOperatorBasicData AviationalOperatorBasicData
		{
			get {return __AviationalOperatorBasicData;}
			set {__AviationalOperatorBasicData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4014.EvidencePersonRepresentingRecipientElectronicService),ElementName="EvidencePersonRepresentingRecipientElectronicService",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4014.EvidencePersonRepresentingRecipientElectronicService __EvidencePersonRepresentingRecipientElectronicService;
		
		[XmlIgnore]
		public R_4014.EvidencePersonRepresentingRecipientElectronicService EvidencePersonRepresentingRecipientElectronicService
		{
			get {return __EvidencePersonRepresentingRecipientElectronicService;}
			set {__EvidencePersonRepresentingRecipientElectronicService = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AuthorQuality",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AuthorQuality;
		
		[XmlIgnore]
		public string AuthorQuality
		{ 
			get { return __AuthorQuality; }
			set { __AuthorQuality = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ApplicantNotAircraftOwner",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="boolean",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ApplicantNotAircraftOwner;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ApplicantNotAircraftOwnerSpecified;
		
		[XmlIgnore]
		public bool ApplicantNotAircraftOwner
		{ 
			get { return __ApplicantNotAircraftOwner; }
			set { __ApplicantNotAircraftOwner = value; __ApplicantNotAircraftOwnerSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4310.AircraftHiringData),ElementName="AircraftHiringData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4310.AircraftHiringData __AircraftHiringData;
		
		[XmlIgnore]
		public R_4310.AircraftHiringData AircraftHiringData
		{
			get {return __AircraftHiringData;}
			set {__AircraftHiringData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4400.AircraftData),ElementName="AircraftData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4400.AircraftData __AircraftData;
		
		[XmlIgnore]
		public R_4400.AircraftData AircraftData
		{
			get {return __AircraftData;}
			set {__AircraftData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4422.AircraftEnginesData),ElementName="AircraftEnginesData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4422.AircraftEnginesData __AircraftEnginesData;
		
		[XmlIgnore]
		public R_4422.AircraftEnginesData AircraftEnginesData
		{
			get {return __AircraftEnginesData;}
			set {__AircraftEnginesData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4442.AircraftPropellersData),ElementName="AircraftPropellersData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4442.AircraftPropellersData __AircraftPropellersData;
		
		[XmlIgnore]
		public R_4442.AircraftPropellersData AircraftPropellersData
		{
			get {return __AircraftPropellersData;}
			set {__AircraftPropellersData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4454.ProvidedOperationTypes),ElementName="ProvidedOperationTypes",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4454.ProvidedOperationTypes __ProvidedOperationTypes;
		
		[XmlIgnore]
		public R_4454.ProvidedOperationTypes ProvidedOperationTypes
		{
			get {return __ProvidedOperationTypes;}
			set {__ProvidedOperationTypes = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4466.AttachedDocumentDatasWithWhenAppliedCollection),ElementName="AttachedDocumentDatasWithWhenAppliedCollection",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4466.AttachedDocumentDatasWithWhenAppliedCollection __AttachedDocumentDatasWithWhenAppliedCollection;
		
		[XmlIgnore]
		public R_4466.AttachedDocumentDatasWithWhenAppliedCollection AttachedDocumentDatasWithWhenAppliedCollection
		{
			get {return __AttachedDocumentDatasWithWhenAppliedCollection;}
			set {__AttachedDocumentDatasWithWhenAppliedCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4468.AttachedDocumentDatasGVAWithWhenAppliedCollection),ElementName="AttachedDocumentDatasGVAWithWhenAppliedCollection",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4468.AttachedDocumentDatasGVAWithWhenAppliedCollection __AttachedDocumentDatasGVAWithWhenAppliedCollection;
		
		[XmlIgnore]
		public R_4468.AttachedDocumentDatasGVAWithWhenAppliedCollection AttachedDocumentDatasGVAWithWhenAppliedCollection
		{
			get {return __AttachedDocumentDatasGVAWithWhenAppliedCollection;}
			set {__AttachedDocumentDatasGVAWithWhenAppliedCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_3990.SubmissionsTermsCollection),ElementName="SupportingDocumentationSubmissionsTermsCollection",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_3990.SubmissionsTermsCollection __SupportingDocumentationSubmissionsTermsCollection;
		
		[XmlIgnore]
		public R_3990.SubmissionsTermsCollection SupportingDocumentationSubmissionsTermsCollection
		{
			get {return __SupportingDocumentationSubmissionsTermsCollection;}
			set {__SupportingDocumentationSubmissionsTermsCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_3994.AttachedDocumentDatasCollection),ElementName="SupportingDocumentationAttachedDocumentDatasCollection",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_3994.AttachedDocumentDatasCollection __SupportingDocumentationAttachedDocumentDatasCollection;
		
		[XmlIgnore]
		public R_3994.AttachedDocumentDatasCollection SupportingDocumentationAttachedDocumentDatasCollection
		{
			get {return __SupportingDocumentationAttachedDocumentDatasCollection;}
			set {__SupportingDocumentationAttachedDocumentDatasCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_3990.SubmissionsTermsCollection),ElementName="InspectionReportSubmissionsTermsCollection",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_3990.SubmissionsTermsCollection __InspectionReportSubmissionsTermsCollection;
		
		[XmlIgnore]
		public R_3990.SubmissionsTermsCollection InspectionReportSubmissionsTermsCollection
		{
			get {return __InspectionReportSubmissionsTermsCollection;}
			set {__InspectionReportSubmissionsTermsCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_3994.AttachedDocumentDatasCollection),ElementName="InspectionReportAttachedDocumentDatasCollection",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_3994.AttachedDocumentDatasCollection __InspectionReportAttachedDocumentDatasCollection;
		
		[XmlIgnore]
		public R_3994.AttachedDocumentDatasCollection InspectionReportAttachedDocumentDatasCollection
		{
			get {return __InspectionReportAttachedDocumentDatasCollection;}
			set {__InspectionReportAttachedDocumentDatasCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftInspectionRepresentatives",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AircraftInspectionRepresentatives;
		
		[XmlIgnore]
		public string AircraftInspectionRepresentatives
		{ 
			get { return __AircraftInspectionRepresentatives; }
			set { __AircraftInspectionRepresentatives = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4354.AircraftInspectionPreferences),ElementName="AircraftInspectionPreferences",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4354.AircraftInspectionPreferences __AircraftInspectionPreferences;
		
		[XmlIgnore]
		public R_4354.AircraftInspectionPreferences AircraftInspectionPreferences
		{
			get {return __AircraftInspectionPreferences;}
			set {__AircraftInspectionPreferences = value;}
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
		[XmlElement(Type=typeof(AircraftAirworthinessCertificateApplicationAttachedDocuments),ElementName="AttachedDocuments",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AircraftAirworthinessCertificateApplicationAttachedDocuments __AttachedDocuments;
		
		[XmlIgnore]
		public AircraftAirworthinessCertificateApplicationAttachedDocuments AttachedDocuments
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

		public AircraftAirworthinessCertificateApplication()
		{
		}
	}


	[XmlType(TypeName="AircraftAirworthinessCertificateApplicationAttachedDocuments",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class AircraftAirworthinessCertificateApplicationAttachedDocuments
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

		public AircraftAirworthinessCertificateApplicationAttachedDocuments()
		{
		}
	}
}
