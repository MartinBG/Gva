//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4980
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4980";
	}




	[XmlType(TypeName="CertificationScopeObject",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class CertificationScopeObject
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4964.CertificationSubject),ElementName="CertificationSubject",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4964.CertificationSubject __CertificationSubject;
		
		[XmlIgnore]
		public R_4964.CertificationSubject CertificationSubject
		{
			get {return __CertificationSubject;}
			set {__CertificationSubject = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4978.CertificationScopes),ElementName="CertificationScopes",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4978.CertificationScopes __CertificationScopes;
		
		[XmlIgnore]
		public R_4978.CertificationScopes CertificationScopes
		{
			get {return __CertificationScopes;}
			set {__CertificationScopes = value;}
		}

		public CertificationScopeObject()
		{
		}
	}
}