//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_5096
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-5096";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AttachedDocumentCollection : System.Collections.Generic.List<R_0009_000139.AttachedDocument>
	{
	}



	[XmlRoot(ElementName="ApprovalTechnicalLogApplication",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="ApprovalTechnicalLogApplication",Namespace=Declarations.SchemaVersion)]
	public partial class ApprovalTechnicalLogApplication
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000152.ElectronicAdministrativeServiceHeader),ElementName="ElectronicAdministrativeServiceHeader",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000152.ElectronicAdministrativeServiceHeader ElectronicAdministrativeServiceHeader { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ApplicantType",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string ApplicantType { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000013.EntityBasicData),ElementName="EntityBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000013.EntityBasicData EntityBasicData { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000014.ForeignEntityBasicData),ElementName="ForeignEntityBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000014.ForeignEntityBasicData ForeignEntityBasicData { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="EntityTradeName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string EntityTradeName { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4584.ContactInformation),ElementName="ContactInformation",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4584.ContactInformation ContactInformation { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4696.EAURecipientsAttachedDocumentDatasCollection),ElementName="EAURecipientsAttachedDocumentDatasCollection",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4696.EAURecipientsAttachedDocumentDatasCollection EAURecipientsAttachedDocumentDatasCollection { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4702.RepresentativeAuthorizedPerson),ElementName="RepresentativeAuthorizedPerson",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4702.RepresentativeAuthorizedPerson RepresentativeAuthorizedPerson { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ConsentReceivingElectronicStatements",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="boolean",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool ConsentReceivingElectronicStatements { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(ApprovalTechnicalLogApplicationAttachedDocuments),ElementName="AttachedDocuments",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ApprovalTechnicalLogApplicationAttachedDocuments AttachedDocuments { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000153.ElectronicAdministrativeServiceFooter),ElementName="ElectronicAdministrativeServiceFooter",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000153.ElectronicAdministrativeServiceFooter ElectronicAdministrativeServiceFooter { get; set; }

		public ApprovalTechnicalLogApplication()
		{
		}
	}


	[XmlType(TypeName="ApprovalTechnicalLogApplicationAttachedDocuments",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ApprovalTechnicalLogApplicationAttachedDocuments
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000139.AttachedDocument),ElementName="AttachedDocument",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AttachedDocumentCollection AttachedDocumentCollection { get; set; }

		public ApprovalTechnicalLogApplicationAttachedDocuments()
		{
		}
	}
}
