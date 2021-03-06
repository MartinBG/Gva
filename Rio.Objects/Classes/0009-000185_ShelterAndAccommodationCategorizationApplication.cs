//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000185
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000185";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AttachedDocumentCollection : System.Collections.Generic.List<R_0009_000139.AttachedDocument>
	{
	}



	[XmlRoot(ElementName="ShelterAndAccommodationCategorizationApplication",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="ShelterAndAccommodationCategorizationApplication",Namespace=Declarations.SchemaVersion)]
	public partial class ShelterAndAccommodationCategorizationApplication
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
		[XmlElement(Type=typeof(R_0009_000181.ShelterAndAccommodation),ElementName="ShelterAndAccommodation",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000181.ShelterAndAccommodation ShelterAndAccommodation { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_3312.OwnerOfTouristSite),ElementName="OwnerOfTouristSite",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_3312.OwnerOfTouristSite OwnerOfTouristSite { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(ShelterAndAccommodationCategorizationApplicationAttachedDocuments),ElementName="AttachedDocuments",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ShelterAndAccommodationCategorizationApplicationAttachedDocuments AttachedDocuments { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000153.ElectronicAdministrativeServiceFooter),ElementName="ElectronicAdministrativeServiceFooter",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000153.ElectronicAdministrativeServiceFooter ElectronicAdministrativeServiceFooter { get; set; }

		public ShelterAndAccommodationCategorizationApplication()
		{
		}
	}


	[XmlType(TypeName="ShelterAndAccommodationCategorizationApplicationAttachedDocuments",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ShelterAndAccommodationCategorizationApplicationAttachedDocuments
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000139.AttachedDocument),ElementName="AttachedDocument",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AttachedDocumentCollection AttachedDocumentCollection { get; set; }

		public ShelterAndAccommodationCategorizationApplicationAttachedDocuments()
		{
		}
	}
}
