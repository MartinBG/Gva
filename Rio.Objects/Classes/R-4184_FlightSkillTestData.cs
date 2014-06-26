//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4184
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4184";
	}




	[XmlType(TypeName="FlightSkillTestData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class FlightSkillTestData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="FlightSkillTestDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __FlightSkillTestDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __FlightSkillTestDateSpecified { get { return __FlightSkillTestDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? FlightSkillTestDate
		{ 
			get { return __FlightSkillTestDate; }
			set { __FlightSkillTestDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4168.FlightSkillTest),ElementName="FlightSkillTest",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4168.FlightSkillTest __FlightSkillTest;
		
		[XmlIgnore]
		public R_4168.FlightSkillTest FlightSkillTest
		{
			get {return __FlightSkillTest;}
			set {__FlightSkillTest = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4152.AircraftClass),ElementName="AircraftClass",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4152.AircraftClass __AircraftClass;
		
		[XmlIgnore]
		public R_4152.AircraftClass AircraftClass
		{
			get {return __AircraftClass;}
			set {__AircraftClass = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4158.AircraftType),ElementName="AircraftType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4158.AircraftType __AircraftType;
		
		[XmlIgnore]
		public R_4158.AircraftType AircraftType
		{
			get {return __AircraftType;}
			set {__AircraftType = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4174.FlightSimulatorType),ElementName="FlightSimulatorType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4174.FlightSimulatorType __FlightSimulatorType;
		
		[XmlIgnore]
		public R_4174.FlightSimulatorType FlightSimulatorType
		{
			get {return __FlightSimulatorType;}
			set {__FlightSimulatorType = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4180.FlightSkillTestExaminer),ElementName="FlightSkillTestExaminer",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4180.FlightSkillTestExaminer __FlightSkillTestExaminer;
		
		[XmlIgnore]
		public R_4180.FlightSkillTestExaminer FlightSkillTestExaminer
		{
			get {return __FlightSkillTestExaminer;}
			set {__FlightSkillTestExaminer = value;}
		}

		public FlightSkillTestData()
		{
		}
	}
}