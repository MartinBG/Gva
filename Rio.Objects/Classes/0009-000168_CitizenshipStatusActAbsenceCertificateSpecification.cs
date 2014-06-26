//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000168
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000168";
	}




	[XmlType(TypeName="CitizenshipStatusActAbsenceCertificateSpecification",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class CitizenshipStatusActAbsenceCertificateSpecification
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="CitizenshipStatusActType",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CitizenshipStatusActType;
		
		[XmlIgnore]
		public string CitizenshipStatusActType
		{ 
			get { return __CitizenshipStatusActType; }
			set { __CitizenshipStatusActType = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(DeathCertificate),ElementName="DeathCertificate",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DeathCertificate __DeathCertificate;
		
		[XmlIgnore]
		public DeathCertificate DeathCertificate
		{
			get {return __DeathCertificate;}
			set {__DeathCertificate = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(MarriageCertificate),ElementName="MarriageCertificate",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public MarriageCertificate __MarriageCertificate;
		
		[XmlIgnore]
		public MarriageCertificate MarriageCertificate
		{
			get {return __MarriageCertificate;}
			set {__MarriageCertificate = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(BirthCertificate),ElementName="BirthCertificate",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public BirthCertificate __BirthCertificate;
		
		[XmlIgnore]
		public BirthCertificate BirthCertificate
		{
			get {return __BirthCertificate;}
			set {__BirthCertificate = value;}
		}

		public CitizenshipStatusActAbsenceCertificateSpecification()
		{
		}
	}


	[XmlType(TypeName="DeathCertificate",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class DeathCertificate
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000126.DeceasedPersonData),ElementName="DeceasedPersonData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000126.DeceasedPersonData __DeceasedPersonData;
		
		[XmlIgnore]
		public R_0009_000126.DeceasedPersonData DeceasedPersonData
		{
			get {return __DeceasedPersonData;}
			set {__DeceasedPersonData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000125.DeathLocation),ElementName="DeathLocation",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000125.DeathLocation __DeathLocation;
		
		[XmlIgnore]
		public R_0009_000125.DeathLocation DeathLocation
		{
			get {return __DeathLocation;}
			set {__DeathLocation = value;}
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
		


		public DeathCertificate()
		{
		}
	}


	[XmlType(TypeName="MarriageCertificate",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class MarriageCertificate
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000113.SpouseData),ElementName="SpouseData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000113.SpouseData __SpouseData;
		
		[XmlIgnore]
		public R_0009_000113.SpouseData SpouseData
		{
			get {return __SpouseData;}
			set {__SpouseData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PersonLastName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PersonLastName;
		
		[XmlIgnore]
		public string PersonLastName
		{ 
			get { return __PersonLastName; }
			set { __PersonLastName = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ForeignCitizenLastNameCyrillic",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ForeignCitizenLastNameCyrillic;
		
		[XmlIgnore]
		public string ForeignCitizenLastNameCyrillic
		{ 
			get { return __ForeignCitizenLastNameCyrillic; }
			set { __ForeignCitizenLastNameCyrillic = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000114.MarriageLocation),ElementName="MarriageLocation",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000114.MarriageLocation __MarriageLocation;
		
		[XmlIgnore]
		public R_0009_000114.MarriageLocation MarriageLocation
		{
			get {return __MarriageLocation;}
			set {__MarriageLocation = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="MarriageDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __MarriageDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __MarriageDateSpecified { get { return __MarriageDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? MarriageDate
		{ 
			get { return __MarriageDate; }
			set { __MarriageDate = value; }
		}
		


		public MarriageCertificate()
		{
		}
	}


	[XmlType(TypeName="BirthCertificate",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class BirthCertificate
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000110.ParentData),ElementName="MotherData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000110.ParentData __MotherData;
		
		[XmlIgnore]
		public R_0009_000110.ParentData MotherData
		{
			get {return __MotherData;}
			set {__MotherData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000110.ParentData),ElementName="FatherData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000110.ParentData __FatherData;
		
		[XmlIgnore]
		public R_0009_000110.ParentData FatherData
		{
			get {return __FatherData;}
			set {__FatherData = value;}
		}

		public BirthCertificate()
		{
		}
	}
}