//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_6064
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-6064";
	}




	[XmlRoot(ElementName="ContainerTransferFileCompetence",Namespace=Declarations.SchemaVersion,IsNullable=false),Serializable]
	[XmlType(TypeName="ContainerTransferFileCompetence",Namespace=Declarations.SchemaVersion)]
	public partial class ContainerTransferFileCompetence
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DocumentTypeName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string DocumentTypeName { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000003.DocumentTypeURI),ElementName="DocumentTypeURI",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000003.DocumentTypeURI DocumentTypeURI { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000002.ElectronicServiceProviderBasicData),ElementName="SenderProvider",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000002.ElectronicServiceProviderBasicData SenderProvider { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000002.ElectronicServiceProviderBasicData),ElementName="ReceiverProvider",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000002.ElectronicServiceProviderBasicData ReceiverProvider { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_6062.FileTransferredJurisdiction),ElementName="FileTransferredJurisdiction",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_6062.FileTransferredJurisdiction FileTransferredJurisdiction { get; set; }

		public ContainerTransferFileCompetence()
		{
		}
	}
}
