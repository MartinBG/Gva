//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4816
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4816";
	}




	[XmlType(TypeName="Course",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class Course
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="CourseCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CourseCode;
		
		[XmlIgnore]
		public string CourseCode
		{ 
			get { return __CourseCode; }
			set { __CourseCode = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="CourseName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CourseName;
		
		[XmlIgnore]
		public string CourseName
		{ 
			get { return __CourseName; }
			set { __CourseName = value; }
		}

		public Course()
		{
		}
	}
}
