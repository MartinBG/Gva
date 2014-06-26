//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000134
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000134";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class InheritorsCerfiticateInheritorsInheritorCollection : System.Collections.Generic.List<InheritorsCerfiticateInheritorsInheritor>
	{
	}



	[XmlRoot(ElementName="InheritorsCerfiticate",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="InheritorsCerfiticate",Namespace=Declarations.SchemaVersion)]
	public partial class InheritorsCerfiticate
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
		[XmlElement(Type=typeof(R_0009_000135.CitizenshipRegistrationBasicData),ElementName="DeceasedPerson",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000135.CitizenshipRegistrationBasicData __DeceasedPerson;
		
		[XmlIgnore]
		public R_0009_000135.CitizenshipRegistrationBasicData DeceasedPerson
		{
			get {return __DeceasedPerson;}
			set {__DeceasedPerson = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000164.CitizenshipStatusActBasicData),ElementName="DeathAct",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000164.CitizenshipStatusActBasicData __DeathAct;
		
		[XmlIgnore]
		public R_0009_000164.CitizenshipStatusActBasicData DeathAct
		{
			get {return __DeathAct;}
			set {__DeathAct = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DateOfDeath",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __DateOfDeath;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __DateOfDeathSpecified { get { return __DateOfDeath.HasValue; } }
		
		[XmlIgnore]
		public DateTime? DateOfDeath
		{ 
			get { return __DateOfDeath; }
			set { __DateOfDeath = value; }
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
		[XmlElement(Type=typeof(InheritorsCerfiticateInheritors),ElementName="Inheritors",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public InheritorsCerfiticateInheritors __Inheritors;
		
		[XmlIgnore]
		public InheritorsCerfiticateInheritors Inheritors
		{
			get {return __Inheritors;}
			set {__Inheritors = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(InheritorsCerfiticateOfficial),ElementName="Official",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public InheritorsCerfiticateOfficial __Official;
		
		[XmlIgnore]
		public InheritorsCerfiticateOfficial Official
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

		public InheritorsCerfiticate()
		{
		}
	}


	[XmlType(TypeName="InheritorsCerfiticateInheritors",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class InheritorsCerfiticateInheritors
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(InheritorsCerfiticateInheritorsInheritor),ElementName="Inheritor",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public InheritorsCerfiticateInheritorsInheritorCollection __InheritorCollection;
		
		[XmlIgnore]
		public InheritorsCerfiticateInheritorsInheritorCollection InheritorCollection
		{
			get
			{
				if (__InheritorCollection == null) __InheritorCollection = new InheritorsCerfiticateInheritorsInheritorCollection();
				return __InheritorCollection;
			}
			set {__InheritorCollection = value;}
		}

		public InheritorsCerfiticateInheritors()
		{
		}
	}


	[XmlType(TypeName="InheritorsCerfiticateInheritorsInheritor",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class InheritorsCerfiticateInheritorsInheritor
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000130.InheritorData),ElementName="InheritorData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000130.InheritorData __InheritorData;
		
		[XmlIgnore]
		public R_0009_000130.InheritorData InheritorData
		{
			get {return __InheritorData;}
			set {__InheritorData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000131.ForeignInheritorData),ElementName="ForeignInheritorData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000131.ForeignInheritorData __ForeignInheritorData;
		
		[XmlIgnore]
		public R_0009_000131.ForeignInheritorData ForeignInheritorData
		{
			get {return __ForeignInheritorData;}
			set {__ForeignInheritorData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ParentUID",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ParentUID;
		
		[XmlIgnore]
		public string ParentUID
		{ 
			get { return __ParentUID; }
			set { __ParentUID = value; }
		}

		public InheritorsCerfiticateInheritorsInheritor()
		{
		}
	}


	[XmlType(TypeName="InheritorsCerfiticateOfficial",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class InheritorsCerfiticateOfficial
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

		public InheritorsCerfiticateOfficial()
		{
		}
	}
}