//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4692
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4692";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class UnnumberedAttachedDocumentDataCollection : System.Collections.Generic.List<R_4690.UnnumberedAttachedDocumentData>
	{
	}



	[XmlType(TypeName="UnnumberedAttachedDocumentDatasCollection",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class UnnumberedAttachedDocumentDatasCollection
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4690.UnnumberedAttachedDocumentData),ElementName="UnnumberedAttachedDocumentData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public UnnumberedAttachedDocumentDataCollection UnnumberedAttachedDocumentDataCollection { get; set; }

		public UnnumberedAttachedDocumentDatasCollection()
		{
		}
	}
}
