//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_5216
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-5216";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class RemovedLicenseRestrictionCollection : System.Collections.Generic.List<R_5214.RemovedLicenseRestriction>
	{
	}



	[XmlType(TypeName="RemovedLicensesRestrictions",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class RemovedLicensesRestrictions
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_5214.RemovedLicenseRestriction),ElementName="RemovedLicenseRestriction",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public RemovedLicenseRestrictionCollection RemovedLicenseRestrictionCollection { get; set; }

		public RemovedLicensesRestrictions()
		{
		}
	}
}
