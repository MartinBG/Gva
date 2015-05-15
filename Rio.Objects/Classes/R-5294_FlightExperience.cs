//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_5294
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-5294";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AircraftClassTypeCollection : System.Collections.Generic.List<R_5288.AircraftClassType>
	{
	}

	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ExperienceHoursFlightsCountCollection : System.Collections.Generic.List<R_5292.ExperienceHoursFlightsCount>
	{
	}



	[XmlType(TypeName="FlightExperience",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class FlightExperience
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="LicenseNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string LicenseNumber { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_5288.AircraftClassType),ElementName="AircraftClassType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public AircraftClassTypeCollection AircraftClassTypeCollection { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_5292.ExperienceHoursFlightsCount),ElementName="ExperienceHoursFlightsCount",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public ExperienceHoursFlightsCountCollection ExperienceHoursFlightsCountCollection { get; set; }

		public FlightExperience()
		{
		}
	}
}
