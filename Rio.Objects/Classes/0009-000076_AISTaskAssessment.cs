//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000076
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000076";
	}




	[XmlType(TypeName="AISTaskAssessment",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AISTaskAssessment
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="TaskID",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __TaskID;
		
		[XmlIgnore]
		public string TaskID
		{ 
			get { return __TaskID; }
			set { __TaskID = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="CheckDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __CheckDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __CheckDateSpecified { get { return __CheckDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? CheckDate
		{ 
			get { return __CheckDate; }
			set { __CheckDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AssessmentType",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AssessmentType;
		
		[XmlIgnore]
		public string AssessmentType
		{ 
			get { return __AssessmentType; }
			set { __AssessmentType = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="Description",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Description;
		
		[XmlIgnore]
		public string Description
		{ 
			get { return __Description; }
			set { __Description = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000032.AISObjectCreationData),ElementName="ObjectCreationData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000032.AISObjectCreationData __ObjectCreationData;
		
		[XmlIgnore]
		public R_0009_000032.AISObjectCreationData ObjectCreationData
		{
			get {return __ObjectCreationData;}
			set {__ObjectCreationData = value;}
		}

		public AISTaskAssessment()
		{
		}
	}
}
