//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4512
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4512";
	}




	[XmlType(TypeName="NoiseData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class NoiseData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="NoiseCertificationAdditionalModifications",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __NoiseCertificationAdditionalModifications;
		
		[XmlIgnore]
		public string NoiseCertificationAdditionalModifications
		{ 
			get { return __NoiseCertificationAdditionalModifications; }
			set { __NoiseCertificationAdditionalModifications = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="StandardAircraftNoise",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __StandardAircraftNoise;
		
		[XmlIgnore]
		public string StandardAircraftNoise
		{ 
			get { return __StandardAircraftNoise; }
			set { __StandardAircraftNoise = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="NoiseLevelSide",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __NoiseLevelSide;
		
		[XmlIgnore]
		public string NoiseLevelSide
		{ 
			get { return __NoiseLevelSide; }
			set { __NoiseLevelSide = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="NoiseLevelApproach",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __NoiseLevelApproach;
		
		[XmlIgnore]
		public string NoiseLevelApproach
		{ 
			get { return __NoiseLevelApproach; }
			set { __NoiseLevelApproach = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="NoiseLevelFlight",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __NoiseLevelFlight;
		
		[XmlIgnore]
		public string NoiseLevelFlight
		{ 
			get { return __NoiseLevelFlight; }
			set { __NoiseLevelFlight = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="NoiseLevelFlightOver",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __NoiseLevelFlightOver;
		
		[XmlIgnore]
		public string NoiseLevelFlightOver
		{ 
			get { return __NoiseLevelFlightOver; }
			set { __NoiseLevelFlightOver = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="NoiseLevelTakeOff",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __NoiseLevelTakeOff;
		
		[XmlIgnore]
		public string NoiseLevelTakeOff
		{ 
			get { return __NoiseLevelTakeOff; }
			set { __NoiseLevelTakeOff = value; }
		}

		public NoiseData()
		{
		}
	}
}