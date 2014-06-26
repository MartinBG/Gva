//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4116
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4116";
	}




	[XmlType(TypeName="CrewInteractTrainingData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class CrewInteractTrainingData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4104.ASCourseExam),ElementName="ASCourseExam",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4104.ASCourseExam __ASCourseExam;
		
		[XmlIgnore]
		public R_4104.ASCourseExam ASCourseExam
		{
			get {return __ASCourseExam;}
			set {__ASCourseExam = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4112.ASCoursePeriod),ElementName="ASCoursePeriod",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4112.ASCoursePeriod __ASCoursePeriod;
		
		[XmlIgnore]
		public R_4112.ASCoursePeriod ASCoursePeriod
		{
			get {return __ASCoursePeriod;}
			set {__ASCoursePeriod = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="CITeoreticalCourseHours",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CITeoreticalCourseHours;
		
		[XmlIgnore]
		public string CITeoreticalCourseHours
		{ 
			get { return __CITeoreticalCourseHours; }
			set { __CITeoreticalCourseHours = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="CIFlightCourseHours",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CIFlightCourseHours;
		
		[XmlIgnore]
		public string CIFlightCourseHours
		{ 
			get { return __CIFlightCourseHours; }
			set { __CIFlightCourseHours = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4110.ATO),ElementName="ATO",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4110.ATO __ATO;
		
		[XmlIgnore]
		public R_4110.ATO ATO
		{
			get {return __ATO;}
			set {__ATO = value;}
		}

		public CrewInteractTrainingData()
		{
		}
	}
}