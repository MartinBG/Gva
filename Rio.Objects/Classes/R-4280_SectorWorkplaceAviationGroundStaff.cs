//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4280
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4280";
	}




	[XmlType(TypeName="SectorWorkplaceAviationGroundStaff",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SectorWorkplaceAviationGroundStaff
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="SectorWorkplaceAviationGroundStaffCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string SectorWorkplaceAviationGroundStaffCode { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="SectorWorkplaceAviationGroundStaffName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string SectorWorkplaceAviationGroundStaffName { get; set; }

		public SectorWorkplaceAviationGroundStaff()
		{
		}
	}
}
