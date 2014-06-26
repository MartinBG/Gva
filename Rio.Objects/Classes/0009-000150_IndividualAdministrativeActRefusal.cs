//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000150
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000150";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class IndividualAdministrativeActRefusalOfficialCollection : System.Collections.Generic.List<IndividualAdministrativeActRefusalOfficial>
	{
	}



	[XmlRoot(ElementName="IndividualAdministrativeActRefusal",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="IndividualAdministrativeActRefusal",Namespace=Declarations.SchemaVersion)]
	public partial class IndividualAdministrativeActRefusal
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
		[XmlElement(Type=typeof(R_0009_000016.ElectronicServiceApplicant),ElementName="ElectronicServiceApplicant",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000016.ElectronicServiceApplicant __ElectronicServiceApplicant;
		
		[XmlIgnore]
		public R_0009_000016.ElectronicServiceApplicant ElectronicServiceApplicant
		{
			get {return __ElectronicServiceApplicant;}
			set {__ElectronicServiceApplicant = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="IndividualAdministrativeActRefusalHeader",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __IndividualAdministrativeActRefusalHeader;
		
		[XmlIgnore]
		public string IndividualAdministrativeActRefusalHeader
		{ 
			get { return __IndividualAdministrativeActRefusalHeader; }
			set { __IndividualAdministrativeActRefusalHeader = value; }
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
		[XmlElement(ElementName="IndividualAdministrativeActRefusalLegalGround",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __IndividualAdministrativeActRefusalLegalGround;
		
		[XmlIgnore]
		public string IndividualAdministrativeActRefusalLegalGround
		{ 
			get { return __IndividualAdministrativeActRefusalLegalGround; }
			set { __IndividualAdministrativeActRefusalLegalGround = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="IndividualAdministrativeActRefusalFactualGround",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __IndividualAdministrativeActRefusalFactualGround;
		
		[XmlIgnore]
		public string IndividualAdministrativeActRefusalFactualGround
		{ 
			get { return __IndividualAdministrativeActRefusalFactualGround; }
			set { __IndividualAdministrativeActRefusalFactualGround = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="IndividualAdministrativeActRefusalAppealTerm",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="duration",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __IndividualAdministrativeActRefusalAppealTerm;
		
		[XmlIgnore]
		public string IndividualAdministrativeActRefusalAppealTerm
		{ 
			get { return __IndividualAdministrativeActRefusalAppealTerm; }
			set { __IndividualAdministrativeActRefusalAppealTerm = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="IndividualAdministrativeActRefusalAppealAuthority",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __IndividualAdministrativeActRefusalAppealAuthority;
		
		[XmlIgnore]
		public string IndividualAdministrativeActRefusalAppealAuthority
		{ 
			get { return __IndividualAdministrativeActRefusalAppealAuthority; }
			set { __IndividualAdministrativeActRefusalAppealAuthority = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AdministrativeBodyName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AdministrativeBodyName;
		
		[XmlIgnore]
		public string AdministrativeBodyName
		{ 
			get { return __AdministrativeBodyName; }
			set { __AdministrativeBodyName = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(IndividualAdministrativeActRefusalOfficial),ElementName="Official",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IndividualAdministrativeActRefusalOfficialCollection __OfficialCollection;
		
		[XmlIgnore]
		public IndividualAdministrativeActRefusalOfficialCollection OfficialCollection
		{
			get
			{
				if (__OfficialCollection == null) __OfficialCollection = new IndividualAdministrativeActRefusalOfficialCollection();
				return __OfficialCollection;
			}
			set {__OfficialCollection = value;}
		}

		public IndividualAdministrativeActRefusal()
		{
		}
	}


	[XmlType(TypeName="IndividualAdministrativeActRefusalOfficial",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class IndividualAdministrativeActRefusalOfficial
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

		public IndividualAdministrativeActRefusalOfficial()
		{
		}
	}
}