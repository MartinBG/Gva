//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4964
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4964";
	}




	[XmlType(TypeName="CertificationSubject",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class CertificationSubject
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="CertificationSubjectCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CertificationSubjectCode;
		
		[XmlIgnore]
		public string CertificationSubjectCode
		{ 
			get { return __CertificationSubjectCode; }
			set { __CertificationSubjectCode = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="CertificationSubjectName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CertificationSubjectName;
		
		[XmlIgnore]
		public string CertificationSubjectName
		{ 
			get { return __CertificationSubjectName; }
			set { __CertificationSubjectName = value; }
		}

		public CertificationSubject()
		{
		}
	}
}