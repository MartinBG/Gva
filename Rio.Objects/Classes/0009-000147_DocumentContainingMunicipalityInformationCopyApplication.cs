//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000147
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000147";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class MunicipalityInformationAccessTypeCollection : System.Collections.Generic.List<string>
	{
	}



	[XmlRoot(ElementName="DocumentContainingMunicipalityInformationCopyApplication",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="DocumentContainingMunicipalityInformationCopyApplication",Namespace=Declarations.SchemaVersion)]
	public partial class DocumentContainingMunicipalityInformationCopyApplication
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000152.ElectronicAdministrativeServiceHeader),ElementName="ElectronicAdministrativeServiceHeader",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000152.ElectronicAdministrativeServiceHeader ElectronicAdministrativeServiceHeader { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000141.ServiceApplicantReceiptData),ElementName="ServiceApplicantReceiptData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000141.ServiceApplicantReceiptData ServiceApplicantReceiptData { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000145.MunicipalityInformationAccessRightSubject),ElementName="MunicipalityInformationAccessRightSubject",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000145.MunicipalityInformationAccessRightSubject MunicipalityInformationAccessRightSubject { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="NeededMunicipalityInformationDescription",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string NeededMunicipalityInformationDescription { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(MunicipalityInformationAccessTypes),ElementName="MunicipalityInformationAccessTypes",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public MunicipalityInformationAccessTypes MunicipalityInformationAccessTypes { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="InformationRecordingTechnicalParameters",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string InformationRecordingTechnicalParameters { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000153.ElectronicAdministrativeServiceFooter),ElementName="ElectronicAdministrativeServiceFooter",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000153.ElectronicAdministrativeServiceFooter ElectronicAdministrativeServiceFooter { get; set; }

		public DocumentContainingMunicipalityInformationCopyApplication()
		{
		}
	}


	[XmlType(TypeName="MunicipalityInformationAccessTypes",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class MunicipalityInformationAccessTypes
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(string),ElementName="MunicipalityInformationAccessType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public MunicipalityInformationAccessTypeCollection MunicipalityInformationAccessTypeCollection { get; set; }

		public MunicipalityInformationAccessTypes()
		{
		}
	}
}
