//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000123
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000123";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class MinorCollection : System.Collections.Generic.List<Minor>
	{
	}



	[XmlRoot(ElementName="MaritalStatusCerfiticate",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="MaritalStatusCerfiticate",Namespace=Declarations.SchemaVersion)]
	public partial class MaritalStatusCerfiticate
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
		[XmlElement(Type=typeof(R_0009_000135.CitizenshipRegistrationBasicData),ElementName="CitizenshipRegistrationBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000135.CitizenshipRegistrationBasicData __CitizenshipRegistrationBasicData;
		
		[XmlIgnore]
		public R_0009_000135.CitizenshipRegistrationBasicData CitizenshipRegistrationBasicData
		{
			get {return __CitizenshipRegistrationBasicData;}
			set {__CitizenshipRegistrationBasicData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000094.PersonAddress),ElementName="PermanentAddress",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000094.PersonAddress __PermanentAddress;
		
		[XmlIgnore]
		public R_0009_000094.PersonAddress PermanentAddress
		{
			get {return __PermanentAddress;}
			set {__PermanentAddress = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="MaritalStatusCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MaritalStatusCode;
		
		[XmlIgnore]
		public string MaritalStatusCode
		{ 
			get { return __MaritalStatusCode; }
			set { __MaritalStatusCode = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="MaritalStatus",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MaritalStatus;
		
		[XmlIgnore]
		public string MaritalStatus
		{ 
			get { return __MaritalStatus; }
			set { __MaritalStatus = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(Spouse),ElementName="Spouse",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Spouse __Spouse;
		
		[XmlIgnore]
		public Spouse Spouse
		{
			get {return __Spouse;}
			set {__Spouse = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(MaritalStatusCerfiticateMinorChildren),ElementName="MinorChildren",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public MaritalStatusCerfiticateMinorChildren __MinorChildren;
		
		[XmlIgnore]
		public MaritalStatusCerfiticateMinorChildren MinorChildren
		{
			get {return __MinorChildren;}
			set {__MinorChildren = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(MaritalStatusCerfiticateOfficial),ElementName="Official",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public MaritalStatusCerfiticateOfficial __Official;
		
		[XmlIgnore]
		public MaritalStatusCerfiticateOfficial Official
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

		public MaritalStatusCerfiticate()
		{
		}
	}


	[XmlType(TypeName="Spouse",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class Spouse
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="MaritalStatusCerfiticatePersonSerialNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="integer",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MaritalStatusCerfiticatePersonSerialNumber;
		
		[XmlIgnore]
		public string MaritalStatusCerfiticatePersonSerialNumber
		{ 
			get { return __MaritalStatusCerfiticatePersonSerialNumber; }
			set { __MaritalStatusCerfiticatePersonSerialNumber = value; }
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
		[XmlElement(Type=typeof(R_0009_000006.PersonIdentifier),ElementName="PersonIdentifier",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000006.PersonIdentifier __PersonIdentifier;
		
		[XmlIgnore]
		public R_0009_000006.PersonIdentifier PersonIdentifier
		{
			get {return __PersonIdentifier;}
			set {__PersonIdentifier = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="BirthDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __BirthDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __BirthDateSpecified { get { return __BirthDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? BirthDate
		{ 
			get { return __BirthDate; }
			set { __BirthDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000094.PersonAddress),ElementName="SpousePermanentAddress",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000094.PersonAddress __SpousePermanentAddress;
		
		[XmlIgnore]
		public R_0009_000094.PersonAddress SpousePermanentAddress
		{
			get {return __SpousePermanentAddress;}
			set {__SpousePermanentAddress = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000133.Citizenship),ElementName="Citizenship",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000133.Citizenship __Citizenship;
		
		[XmlIgnore]
		public R_0009_000133.Citizenship Citizenship
		{
			get {return __Citizenship;}
			set {__Citizenship = value;}
		}

		public Spouse()
		{
		}
	}


	[XmlType(TypeName="MaritalStatusCerfiticateMinorChildren",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class MaritalStatusCerfiticateMinorChildren
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(Minor),ElementName="Minor",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public MinorCollection __MinorCollection;
		
		[XmlIgnore]
		public MinorCollection MinorCollection
		{
			get
			{
				if (__MinorCollection == null) __MinorCollection = new MinorCollection();
				return __MinorCollection;
			}
			set {__MinorCollection = value;}
		}

		public MaritalStatusCerfiticateMinorChildren()
		{
		}
	}


	[XmlType(TypeName="Minor",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class Minor
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="MaritalStatusCerfiticatePersonSerialNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="integer",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MaritalStatusCerfiticatePersonSerialNumber;
		
		[XmlIgnore]
		public string MaritalStatusCerfiticatePersonSerialNumber
		{ 
			get { return __MaritalStatusCerfiticatePersonSerialNumber; }
			set { __MaritalStatusCerfiticatePersonSerialNumber = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000121.MinorData),ElementName="MinorData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000121.MinorData __MinorData;
		
		[XmlIgnore]
		public R_0009_000121.MinorData MinorData
		{
			get {return __MinorData;}
			set {__MinorData = value;}
		}

		public Minor()
		{
		}
	}


	[XmlType(TypeName="MaritalStatusCerfiticateOfficial",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class MaritalStatusCerfiticateOfficial
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

		public MaritalStatusCerfiticateOfficial()
		{
		}
	}
}
