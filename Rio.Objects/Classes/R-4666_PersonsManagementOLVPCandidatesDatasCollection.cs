//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4666
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4666";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PersonManagementOLVPCandidateDataCollection : System.Collections.Generic.List<R_4664.PersonManagementOLVPCandidateData>
	{
	}



	[XmlType(TypeName="PersonsManagementOLVPCandidatesDatasCollection",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PersonsManagementOLVPCandidatesDatasCollection
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4664.PersonManagementOLVPCandidateData),ElementName="PersonManagementOLVPCandidateData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PersonManagementOLVPCandidateDataCollection PersonManagementOLVPCandidateDataCollection { get; set; }

		public PersonsManagementOLVPCandidatesDatasCollection()
		{
		}
	}
}
