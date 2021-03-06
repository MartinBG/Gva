//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000124
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000124";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AttachedDocumentCollection : System.Collections.Generic.List<R_0009_000139.AttachedDocument>
	{
	}



	[XmlRoot(ElementName="SecondDeathCertificateTranscriptApplication",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="SecondDeathCertificateTranscriptApplication",Namespace=Declarations.SchemaVersion)]
	public partial class SecondDeathCertificateTranscriptApplication
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000152.ElectronicAdministrativeServiceHeader),ElementName="ElectronicAdministrativeServiceHeader",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000152.ElectronicAdministrativeServiceHeader ElectronicAdministrativeServiceHeader { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ServiceTermType",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string ServiceTermType { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000141.ServiceApplicantReceiptData),ElementName="ServiceApplicantReceiptData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000141.ServiceApplicantReceiptData ServiceApplicantReceiptData { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000126.DeceasedPersonData),ElementName="DeceasedPersonData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000126.DeceasedPersonData DeceasedPersonData { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000125.DeathLocation),ElementName="DeathLocation",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000125.DeathLocation DeathLocation { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DateOfDeath",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? DateOfDeath { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(SecondDeathCertificateTranscriptApplicationAttachedDocuments),ElementName="AttachedDocuments",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SecondDeathCertificateTranscriptApplicationAttachedDocuments AttachedDocuments { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000153.ElectronicAdministrativeServiceFooter),ElementName="ElectronicAdministrativeServiceFooter",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000153.ElectronicAdministrativeServiceFooter ElectronicAdministrativeServiceFooter { get; set; }

		public SecondDeathCertificateTranscriptApplication()
		{
		}
	}


	[XmlType(TypeName="SecondDeathCertificateTranscriptApplicationAttachedDocuments",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class SecondDeathCertificateTranscriptApplicationAttachedDocuments
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000139.AttachedDocument),ElementName="AttachedDocument",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AttachedDocumentCollection AttachedDocumentCollection { get; set; }

		public SecondDeathCertificateTranscriptApplicationAttachedDocuments()
		{
		}
	}
}
