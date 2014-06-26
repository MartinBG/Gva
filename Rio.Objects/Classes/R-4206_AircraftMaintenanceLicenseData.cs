//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4206
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4206";
	}




	[XmlType(TypeName="AircraftMaintenanceLicenseData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AircraftMaintenanceLicenseData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftMaintenanceLicenseNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AircraftMaintenanceLicenseNumber;
		
		[XmlIgnore]
		public string AircraftMaintenanceLicenseNumber
		{ 
			get { return __AircraftMaintenanceLicenseNumber; }
			set { __AircraftMaintenanceLicenseNumber = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftMaintenanceLicenseDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __AircraftMaintenanceLicenseDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __AircraftMaintenanceLicenseDateSpecified { get { return __AircraftMaintenanceLicenseDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? AircraftMaintenanceLicenseDate
		{ 
			get { return __AircraftMaintenanceLicenseDate; }
			set { __AircraftMaintenanceLicenseDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="CAAPersonalIdentificationNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="integer",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CAAPersonalIdentificationNumber;
		
		[XmlIgnore]
		public string CAAPersonalIdentificationNumber
		{ 
			get { return __CAAPersonalIdentificationNumber; }
			set { __CAAPersonalIdentificationNumber = value; }
		}

		public AircraftMaintenanceLicenseData()
		{
		}
	}
}
