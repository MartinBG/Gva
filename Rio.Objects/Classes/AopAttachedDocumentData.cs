//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace AopAttachedDocument
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/AopAttachedDocument";
	}




	[XmlType(TypeName="AopAttachedDocumentData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AopAttachedDocumentData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4034.AttachedDocumentKind),ElementName="AttachedDocumentKind",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4034.AttachedDocumentKind AttachedDocumentKind { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AttachedDocumentUniqueIdentifier",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string AttachedDocumentUniqueIdentifier { get; set; }

		public AopAttachedDocumentData()
		{
		}
	}
}
