//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_5286
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-5286";
	}




	[XmlType(TypeName="EntryQualificationClassPermission",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class EntryQualificationClassPermission
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4158.AircraftType),ElementName="AircraftType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4158.AircraftType AircraftType { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_5274.TrainingBasicData),ElementName="AircraftTrainingBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_5274.TrainingBasicData AircraftTrainingBasicData { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4174.FlightSimulatorType),ElementName="FlightSimulatorType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4174.FlightSimulatorType FlightSimulatorType { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_5274.TrainingBasicData),ElementName="FlightSimulatorTrainingBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_5274.TrainingBasicData FlightSimulatorTrainingBasicData { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="OtherMeansDescriptionTraining",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string OtherMeansDescriptionTraining { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_5274.TrainingBasicData),ElementName="OtherTrainingBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_5274.TrainingBasicData OtherTrainingBasicData { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="OrderNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string OrderNumber { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="OrderDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? OrderDate { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_5270.IndividualRecoveryProgram),ElementName="TrainingIndividualRecoveryProgram",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_5270.IndividualRecoveryProgram TrainingIndividualRecoveryProgram { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_5274.TrainingBasicData),ElementName="FinishedTrainingBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_5274.TrainingBasicData FinishedTrainingBasicData { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_5270.IndividualRecoveryProgram),ElementName="FinishedIndividualRecoveryProgram",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_5270.IndividualRecoveryProgram FinishedIndividualRecoveryProgram { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="OtherTrainingDescription",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string OtherTrainingDescription { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_5274.TrainingBasicData),ElementName="OtherMeansTrainingBasicData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_5274.TrainingBasicData OtherMeansTrainingBasicData { get; set; }

		public EntryQualificationClassPermission()
		{
		}
	}
}