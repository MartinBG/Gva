//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000045
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000045";
	}




	[XmlType(TypeName="CreatedDocumentFromServiceStage",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class CreatedDocumentFromServiceStage : R_0009_000043.AdministrativeNomenclatureDocumentTypeBasicData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(CreatedDocumentFromServiceStageAdditionalData),ElementName="AdditionalData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public CreatedDocumentFromServiceStageAdditionalData AdditionalData { get; set; }

		public CreatedDocumentFromServiceStage() : base()
		{
		}
	}


	[XmlType(TypeName="CreatedDocumentFromServiceStageAdditionalData",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class CreatedDocumentFromServiceStageAdditionalData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAnyElement()]
		public System.Xml.XmlElement[] Any;

		public CreatedDocumentFromServiceStageAdditionalData()
		{
		}
	}
}
