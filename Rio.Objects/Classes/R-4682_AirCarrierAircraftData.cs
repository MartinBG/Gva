//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4682
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4682";
	}




	[XmlType(TypeName="AirCarrierAircraftData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AirCarrierAircraftData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftRegistrationMark",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string AircraftRegistrationMark { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4158.AircraftType),ElementName="AircraftType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4158.AircraftType AircraftType { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftMaximumTakeOffWeight",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="integer",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string AircraftMaximumTakeOffWeight { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftPassengerSeatsCount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string AircraftPassengerSeatsCount { get; set; }

		public AirCarrierAircraftData()
		{
		}
	}
}
