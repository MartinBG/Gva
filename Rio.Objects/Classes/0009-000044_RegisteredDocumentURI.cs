//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000044
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000044";
	}




	[XmlType(TypeName="RegisteredDocumentURI",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class RegisteredDocumentURI
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000001.DocumentURI),ElementName="DocumentURI",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000001.DocumentURI __DocumentURI;
		
		[XmlIgnore]
		public R_0009_000001.DocumentURI DocumentURI
		{
			get {return __DocumentURI;}
			set {__DocumentURI = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DocumentInInternalRegisterURI",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __DocumentInInternalRegisterURI;
		
		[XmlIgnore]
		public string DocumentInInternalRegisterURI
		{ 
			get { return __DocumentInInternalRegisterURI; }
			set { __DocumentInInternalRegisterURI = value; }
		}

		public RegisteredDocumentURI()
		{
		}
	}
}