//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000041
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000041";
	}




	[XmlType(TypeName="ServiceStageExecutor",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ServiceStageExecutor
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000030.PositionInAdministrationOrAISUser),ElementName="PositionInAdministrationOrAISUser",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000030.PositionInAdministrationOrAISUser PositionInAdministrationOrAISUser { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(ServiceStageExecutorAdditionalData),ElementName="AdditionalData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ServiceStageExecutorAdditionalData AdditionalData { get; set; }

		public ServiceStageExecutor()
		{
		}
	}


	[XmlType(TypeName="ServiceStageExecutorAdditionalData",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class ServiceStageExecutorAdditionalData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAnyElement()]
		public System.Xml.XmlElement[] Any;

		public ServiceStageExecutorAdditionalData()
		{
		}
	}
}
