//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_6090
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-6090";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class DecisionGrantAccessPublicInformationOfficialCollection : System.Collections.Generic.List<DecisionGrantAccessPublicInformationOfficial>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class RejectedInformationCollection : System.Collections.Generic.List<RejectedInformation>
	{
	}



	[XmlRoot(ElementName="DecisionGrantAccessPublicInformation",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="DecisionGrantAccessPublicInformation",Namespace=Declarations.SchemaVersion)]
	public partial class DecisionGrantAccessPublicInformation
	{

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
		[XmlElement(ElementName="LegalBasisIssuanceAdministrativeAct",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __LegalBasisIssuanceAdministrativeAct;
		
		[XmlIgnore]
		public string LegalBasisIssuanceAdministrativeAct
		{ 
			get { return __LegalBasisIssuanceAdministrativeAct; }
			set { __LegalBasisIssuanceAdministrativeAct = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000073.AISCaseURI),ElementName="AISCaseURI",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000073.AISCaseURI __AISCaseURI;
		
		[XmlIgnore]
		public R_0009_000073.AISCaseURI AISCaseURI
		{
			get {return __AISCaseURI;}
			set {__AISCaseURI = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DegreeAccessRequestedPublicInformation",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DegreeAccessRequestedPublicInformation;
		
		[XmlIgnore]
		public string DegreeAccessRequestedPublicInformation
		{ 
			get { return __DegreeAccessRequestedPublicInformation; }
			set { __DegreeAccessRequestedPublicInformation = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DescriptionProvidedPublicInformation",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DescriptionProvidedPublicInformation;
		
		[XmlIgnore]
		public string DescriptionProvidedPublicInformation
		{ 
			get { return __DescriptionProvidedPublicInformation; }
			set { __DescriptionProvidedPublicInformation = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="FormProvidingAccessPublicInformation",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __FormProvidingAccessPublicInformation;
		
		[XmlIgnore]
		public string FormProvidingAccessPublicInformation
		{ 
			get { return __FormProvidingAccessPublicInformation; }
			set { __FormProvidingAccessPublicInformation = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PlacePublicInformationAccessGiven",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PlacePublicInformationAccessGiven;
		
		[XmlIgnore]
		public string PlacePublicInformationAccessGiven
		{ 
			get { return __PlacePublicInformationAccessGiven; }
			set { __PlacePublicInformationAccessGiven = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PeriodAccessPublicInformation",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PeriodAccessPublicInformation;
		
		[XmlIgnore]
		public string PeriodAccessPublicInformation
		{ 
			get { return __PeriodAccessPublicInformation; }
			set { __PeriodAccessPublicInformation = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(RejectedInformation),ElementName="RejectedInformation",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public RejectedInformationCollection __RejectedInformationCollection;
		
		[XmlIgnore]
		public RejectedInformationCollection RejectedInformationCollection
		{
			get
			{
				if (__RejectedInformationCollection == null) __RejectedInformationCollection = new RejectedInformationCollection();
				return __RejectedInformationCollection;
			}
			set {__RejectedInformationCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="InformationPaymentCostsProvidingAccess",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __InformationPaymentCostsProvidingAccess;
		
		[XmlIgnore]
		public string InformationPaymentCostsProvidingAccess
		{ 
			get { return __InformationPaymentCostsProvidingAccess; }
			set { __InformationPaymentCostsProvidingAccess = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="OtherBodiesOrganizations",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __OtherBodiesOrganizations;
		
		[XmlIgnore]
		public string OtherBodiesOrganizations
		{ 
			get { return __OtherBodiesOrganizations; }
			set { __OtherBodiesOrganizations = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(DecisionGrantAccessPublicInformationOfficial),ElementName="Official",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DecisionGrantAccessPublicInformationOfficialCollection __OfficialCollection;
		
		[XmlIgnore]
		public DecisionGrantAccessPublicInformationOfficialCollection OfficialCollection
		{
			get
			{
				if (__OfficialCollection == null) __OfficialCollection = new DecisionGrantAccessPublicInformationOfficialCollection();
				return __OfficialCollection;
			}
			set {__OfficialCollection = value;}
		}

		public DecisionGrantAccessPublicInformation()
		{
		}
	}


	[XmlType(TypeName="RejectedInformation",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class RejectedInformation
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="RequestedPublicInformationDescription",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __RequestedPublicInformationDescription;
		
		[XmlIgnore]
		public string RequestedPublicInformationDescription
		{ 
			get { return __RequestedPublicInformationDescription; }
			set { __RequestedPublicInformationDescription = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ReasonsNotGrandAccessPublicInformation",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ReasonsNotGrandAccessPublicInformation;
		
		[XmlIgnore]
		public string ReasonsNotGrandAccessPublicInformation
		{ 
			get { return __ReasonsNotGrandAccessPublicInformation; }
			set { __ReasonsNotGrandAccessPublicInformation = value; }
		}

		public RejectedInformation()
		{
		}
	}


	[XmlType(TypeName="DecisionGrantAccessPublicInformationOfficial",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class DecisionGrantAccessPublicInformationOfficial
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="SignatureUniqueID",DataType="string")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __SignatureUniqueID;
		
		[XmlIgnore]
		public string SignatureUniqueID
		{ 
			get { return __SignatureUniqueID; }
			set { __SignatureUniqueID = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ElectronicDocumentAuthorQuality",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ElectronicDocumentAuthorQuality;
		
		[XmlIgnore]
		public string ElectronicDocumentAuthorQuality
		{ 
			get { return __ElectronicDocumentAuthorQuality; }
			set { __ElectronicDocumentAuthorQuality = value; }
		}

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

		public DecisionGrantAccessPublicInformationOfficial()
		{
		}
	}
}