//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_1198
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-1198";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ProcedureAssessmentConformityCollection : System.Collections.Generic.List<ProcedureAssessmentConformity>
	{
	}



	[XmlRoot(ElementName="ProcedureAssessmentConformityData",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="ProcedureAssessmentConformityData",Namespace=Declarations.SchemaVersion)]
	public partial class ProcedureAssessmentConformityData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAttribute(AttributeName="versionDate",DataType="date")]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? versionDate { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(ProcedureAssessmentConformities),ElementName="ProcedureAssessmentConformities",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ProcedureAssessmentConformities ProcedureAssessmentConformities { get; set; }

		public ProcedureAssessmentConformityData()
		{
		}
	}


	[XmlType(TypeName="ProcedureAssessmentConformities",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ProcedureAssessmentConformities
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(ProcedureAssessmentConformity),ElementName="ProcedureAssessmentConformity",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ProcedureAssessmentConformityCollection ProcedureAssessmentConformityCollection { get; set; }

		public ProcedureAssessmentConformities()
		{
		}
	}


	[XmlType(TypeName="ProcedureAssessmentConformity",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ProcedureAssessmentConformity
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="Code",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string Code { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string Name { get; set; }

		public ProcedureAssessmentConformity()
		{
		}
	}
}