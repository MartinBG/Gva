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
		public R_0009_000152.ElectronicAdministrativeServiceHeader __ElectronicAdministrativeServiceHeader;
		
		[XmlIgnore]
		public R_0009_000152.ElectronicAdministrativeServiceHeader ElectronicAdministrativeServiceHeader
		{
			get {return __ElectronicAdministrativeServiceHeader;}
			set {__ElectronicAdministrativeServiceHeader = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000141.ServiceApplicantReceiptData),ElementName="ServiceApplicantReceiptData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000141.ServiceApplicantReceiptData __ServiceApplicantReceiptData;
		
		[XmlIgnore]
		public R_0009_000141.ServiceApplicantReceiptData ServiceApplicantReceiptData
		{
			get {return __ServiceApplicantReceiptData;}
			set {__ServiceApplicantReceiptData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000145.MunicipalityInformationAccessRightSubject),ElementName="MunicipalityInformationAccessRightSubject",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000145.MunicipalityInformationAccessRightSubject __MunicipalityInformationAccessRightSubject;
		
		[XmlIgnore]
		public R_0009_000145.MunicipalityInformationAccessRightSubject MunicipalityInformationAccessRightSubject
		{
			get {return __MunicipalityInformationAccessRightSubject;}
			set {__MunicipalityInformationAccessRightSubject = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="NeededMunicipalityInformationDescription",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __NeededMunicipalityInformationDescription;
		
		[XmlIgnore]
		public string NeededMunicipalityInformationDescription
		{ 
			get { return __NeededMunicipalityInformationDescription; }
			set { __NeededMunicipalityInformationDescription = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(MunicipalityInformationAccessTypes),ElementName="MunicipalityInformationAccessTypes",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public MunicipalityInformationAccessTypes __MunicipalityInformationAccessTypes;
		
		[XmlIgnore]
		public MunicipalityInformationAccessTypes MunicipalityInformationAccessTypes
		{
			get {return __MunicipalityInformationAccessTypes;}
			set {__MunicipalityInformationAccessTypes = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="InformationRecordingTechnicalParameters",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __InformationRecordingTechnicalParameters;
		
		[XmlIgnore]
		public string InformationRecordingTechnicalParameters
		{ 
			get { return __InformationRecordingTechnicalParameters; }
			set { __InformationRecordingTechnicalParameters = value; }
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
		public MunicipalityInformationAccessTypeCollection __MunicipalityInformationAccessTypeCollection;
		
		[XmlIgnore]
		public MunicipalityInformationAccessTypeCollection MunicipalityInformationAccessTypeCollection
		{
			get
			{
				if (__MunicipalityInformationAccessTypeCollection == null) __MunicipalityInformationAccessTypeCollection = new MunicipalityInformationAccessTypeCollection();
				return __MunicipalityInformationAccessTypeCollection;
			}
			set {__MunicipalityInformationAccessTypeCollection = value;}
		}

		public MunicipalityInformationAccessTypes()
		{
		}
	}
}
