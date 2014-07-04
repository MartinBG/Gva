//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4294
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4294";
	}




	[XmlType(TypeName="AviationalOperatorBasicData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AviationalOperatorBasicData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000013.EntityBasicData),ElementName="EntityBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000013.EntityBasicData EntityBasicData { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4286.CorrespondenceAddress),ElementName="CorrespondenceAddress",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4286.CorrespondenceAddress CorrespondenceAddress { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000014.ForeignEntityBasicData),ElementName="ForeignEntityBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000014.ForeignEntityBasicData ForeignEntityBasicData { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4288.CorrespondenceAddressAbroad),ElementName="CorrespondenceAddressAbroad",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4288.CorrespondenceAddressAbroad CorrespondenceAddressAbroad { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PhoneNumbersDesc",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string PhoneNumbersDesc { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="FaxNumbersDesc",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string FaxNumbersDesc { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="EmailAddress",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string EmailAddress { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AviationalOperatorCertificateNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string AviationalOperatorCertificateNumber { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AviationalOperatorCertificateIssueDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? AviationalOperatorCertificateIssueDate { get; set; }

		public AviationalOperatorBasicData()
		{
		}
	}
}
