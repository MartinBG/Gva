//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4564
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4564";
	}




	[XmlType(TypeName="ExportCertificateExceptions",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ExportCertificateExceptions
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ExportCertificateAirworthinessExceptions",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ExportCertificateAirworthinessExceptions;
		
		[XmlIgnore]
		public string ExportCertificateAirworthinessExceptions
		{ 
			get { return __ExportCertificateAirworthinessExceptions; }
			set { __ExportCertificateAirworthinessExceptions = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4040.AttachedDocumentData),ElementName="AttachedDocumentData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4040.AttachedDocumentData __AttachedDocumentData;
		
		[XmlIgnore]
		public R_4040.AttachedDocumentData AttachedDocumentData
		{
			get {return __AttachedDocumentData;}
			set {__AttachedDocumentData = value;}
		}

		public ExportCertificateExceptions()
		{
		}
	}
}
