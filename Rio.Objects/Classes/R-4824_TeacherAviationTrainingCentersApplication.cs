//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4824
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4824";
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



	[XmlRoot(ElementName="TeacherAviationTrainingCentersApplication",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="TeacherAviationTrainingCentersApplication",Namespace=Declarations.SchemaVersion)]
	public partial class TeacherAviationTrainingCentersApplication
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
		[XmlElement(Type=typeof(R_0009_000008.PersonBasicData),ElementName="PersonBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000008.PersonBasicData __PersonBasicData;
		
		[XmlIgnore]
		public R_0009_000008.PersonBasicData PersonBasicData
		{
			get {return __PersonBasicData;}
			set {__PersonBasicData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000095.PlaceOfBirth),ElementName="PlaceOfBirth",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000095.PlaceOfBirth __PlaceOfBirth;
		
		[XmlIgnore]
		public R_0009_000095.PlaceOfBirth PlaceOfBirth
		{
			get {return __PlaceOfBirth;}
			set {__PlaceOfBirth = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000011.ForeignCitizenBasicData),ElementName="ForeignCitizenBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000011.ForeignCitizenBasicData __ForeignCitizenBasicData;
		
		[XmlIgnore]
		public R_0009_000011.ForeignCitizenBasicData ForeignCitizenBasicData
		{
			get {return __ForeignCitizenBasicData;}
			set {__ForeignCitizenBasicData = value;}
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
		[XmlElement(Type=typeof(R_4822.AviationTrainingCenter),ElementName="AviationTrainingCenter",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4822.AviationTrainingCenter __AviationTrainingCenter;
		
		[XmlIgnore]
		public R_4822.AviationTrainingCenter AviationTrainingCenter
		{
			get {return __AviationTrainingCenter;}
			set {__AviationTrainingCenter = value;}
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
		[XmlElement(Type=typeof(TeacherAviationTrainingCentersApplicationAttachedDocuments),ElementName="AttachedDocuments",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public TeacherAviationTrainingCentersApplicationAttachedDocuments __AttachedDocuments;
		
		[XmlIgnore]
		public TeacherAviationTrainingCentersApplicationAttachedDocuments AttachedDocuments
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

		public TeacherAviationTrainingCentersApplication()
		{
		}
	}


	[XmlType(TypeName="TeacherAviationTrainingCentersApplicationAttachedDocuments",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class TeacherAviationTrainingCentersApplicationAttachedDocuments
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

		public TeacherAviationTrainingCentersApplicationAttachedDocuments()
		{
		}
	}
}
