//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4536
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4536";
	}




	[XmlType(TypeName="AirworthinessAdditionalInformation",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AirworthinessAdditionalInformation
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftMaximumTakeOffWeight",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="integer",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AircraftMaximumTakeOffWeight;
		
		[XmlIgnore]
		public string AircraftMaximumTakeOffWeight
		{ 
			get { return __AircraftMaximumTakeOffWeight; }
			set { __AircraftMaximumTakeOffWeight = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftMaintenanceProgram",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AircraftMaintenanceProgram;
		
		[XmlIgnore]
		public string AircraftMaintenanceProgram
		{ 
			get { return __AircraftMaintenanceProgram; }
			set { __AircraftMaintenanceProgram = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftInspectionRepresentatives",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AircraftInspectionRepresentatives;
		
		[XmlIgnore]
		public string AircraftInspectionRepresentatives
		{ 
			get { return __AircraftInspectionRepresentatives; }
			set { __AircraftInspectionRepresentatives = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftModificationsEquipment",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AircraftModificationsEquipment;
		
		[XmlIgnore]
		public string AircraftModificationsEquipment
		{ 
			get { return __AircraftModificationsEquipment; }
			set { __AircraftModificationsEquipment = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ChangesModificationsResult",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ChangesModificationsResult;
		
		[XmlIgnore]
		public string ChangesModificationsResult
		{ 
			get { return __ChangesModificationsResult; }
			set { __ChangesModificationsResult = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="FlagSpaceFlightsBrnav",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="boolean",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __FlagSpaceFlightsBrnav;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __FlagSpaceFlightsBrnavSpecified;
		
		[XmlIgnore]
		public bool FlagSpaceFlightsBrnav
		{ 
			get { return __FlagSpaceFlightsBrnav; }
			set { __FlagSpaceFlightsBrnav = value; __FlagSpaceFlightsBrnavSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="FlagSpaceFlightsRvsm",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="boolean",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __FlagSpaceFlightsRvsm;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __FlagSpaceFlightsRvsmSpecified;
		
		[XmlIgnore]
		public bool FlagSpaceFlightsRvsm
		{ 
			get { return __FlagSpaceFlightsRvsm; }
			set { __FlagSpaceFlightsRvsm = value; __FlagSpaceFlightsRvsmSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="FlagSpaceFlightsOther",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="boolean",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __FlagSpaceFlightsOther;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __FlagSpaceFlightsOtherSpecified;
		
		[XmlIgnore]
		public bool FlagSpaceFlightsOther
		{ 
			get { return __FlagSpaceFlightsOther; }
			set { __FlagSpaceFlightsOther = value; __FlagSpaceFlightsOtherSpecified = true; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="SpaceName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __SpaceName;
		
		[XmlIgnore]
		public string SpaceName
		{ 
			get { return __SpaceName; }
			set { __SpaceName = value; }
		}

		public AirworthinessAdditionalInformation()
		{
		}
	}
}
