//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000016
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000016";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ElectronicStatementAuthorCollection : System.Collections.Generic.List<R_0009_000012.ElectronicStatementAuthor>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class RecipientGroupCollection : System.Collections.Generic.List<RecipientGroup>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ElectronicServiceRecipientCollection : System.Collections.Generic.List<R_0009_000015.ElectronicServiceRecipient>
	{
	}



	[XmlType(TypeName="ElectronicServiceApplicant",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ElectronicServiceApplicant
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(RecipientGroup),ElementName="RecipientGroup",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public RecipientGroupCollection RecipientGroupCollection { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="EmailAddress",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string EmailAddress { get; set; }

		public ElectronicServiceApplicant()
		{
		}
	}


	[XmlType(TypeName="RecipientGroup",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class RecipientGroup
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000012.ElectronicStatementAuthor),ElementName="Author",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ElectronicStatementAuthorCollection AuthorCollection { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AuthorQuality",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string AuthorQuality { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000015.ElectronicServiceRecipient),ElementName="Recipient",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ElectronicServiceRecipientCollection RecipientCollection { get; set; }

		public RecipientGroup()
		{
		}
	}
}
