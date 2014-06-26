//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4834
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4834";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class CourseCollection : System.Collections.Generic.List<R_4816.Course>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AttachedDocumentCollection : System.Collections.Generic.List<R_0009_000139.AttachedDocument>
	{
	}



	[XmlRoot(ElementName="VocationalAviationTrainingCenterApplication",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="VocationalAviationTrainingCenterApplication",Namespace=Declarations.SchemaVersion)]
	public partial class VocationalAviationTrainingCenterApplication
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
		[XmlElement(Type=typeof(R_4816.Course),ElementName="Course",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public CourseCollection __CourseCollection;
		
		[XmlIgnore]
		public CourseCollection CourseCollection
		{
			get
			{
				if (__CourseCollection == null) __CourseCollection = new CourseCollection();
				return __CourseCollection;
			}
			set {__CourseCollection = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4832.TeachersInstructors),ElementName="Teachers",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4832.TeachersInstructors __Teachers;
		
		[XmlIgnore]
		public R_4832.TeachersInstructors Teachers
		{
			get {return __Teachers;}
			set {__Teachers = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4832.TeachersInstructors),ElementName="Instructors",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4832.TeachersInstructors __Instructors;
		
		[XmlIgnore]
		public R_4832.TeachersInstructors Instructors
		{
			get {return __Instructors;}
			set {__Instructors = value;}
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
		[XmlElement(Type=typeof(VocationalAviationTrainingCenterApplicationAttachedDocuments),ElementName="AttachedDocuments",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public VocationalAviationTrainingCenterApplicationAttachedDocuments __AttachedDocuments;
		
		[XmlIgnore]
		public VocationalAviationTrainingCenterApplicationAttachedDocuments AttachedDocuments
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

		public VocationalAviationTrainingCenterApplication()
		{
		}
	}


	[XmlType(TypeName="VocationalAviationTrainingCenterApplicationAttachedDocuments",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class VocationalAviationTrainingCenterApplicationAttachedDocuments
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

		public VocationalAviationTrainingCenterApplicationAttachedDocuments()
		{
		}
	}
}