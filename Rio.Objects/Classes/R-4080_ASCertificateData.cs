//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4080
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4080";
	}




	[XmlType(TypeName="ASCertificateData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ASCertificateData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ASCertificateNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ASCertificateNumber;
		
		[XmlIgnore]
		public string ASCertificateNumber
		{ 
			get { return __ASCertificateNumber; }
			set { __ASCertificateNumber = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ASCertificateIssuedOn",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __ASCertificateIssuedOn;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ASCertificateIssuedOnSpecified { get { return __ASCertificateIssuedOn.HasValue; } }
		
		[XmlIgnore]
		public DateTime? ASCertificateIssuedOn
		{ 
			get { return __ASCertificateIssuedOn; }
			set { __ASCertificateIssuedOn = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ASCertificateValidity",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __ASCertificateValidity;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ASCertificateValiditySpecified { get { return __ASCertificateValidity.HasValue; } }
		
		[XmlIgnore]
		public DateTime? ASCertificateValidity
		{ 
			get { return __ASCertificateValidity; }
			set { __ASCertificateValidity = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4072.AviationAdministration),ElementName="AviationAdministration",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4072.AviationAdministration __AviationAdministration;
		
		[XmlIgnore]
		public R_4072.AviationAdministration AviationAdministration
		{
			get {return __AviationAdministration;}
			set {__AviationAdministration = value;}
		}

		public ASCertificateData()
		{
		}
	}
}
