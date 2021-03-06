//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_5114
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-5114";
	}




	[XmlType(TypeName="PersonApprovalData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PersonApprovalData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="NamesPersonApproval",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string NamesPersonApproval { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PositionPersonApproval",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string PositionPersonApproval { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PhoneNumbersDesc",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string PhoneNumbersDesc { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="EmailAddress",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string EmailAddress { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="QualificationPersonApproval",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string QualificationPersonApproval { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ExperiencePersonApproval",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string ExperiencePersonApproval { get; set; }

		public PersonApprovalData()
		{
		}
	}
}
