//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4422
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4422";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class EngineDataCollection : System.Collections.Generic.List<R_4420.EngineData>
	{
	}



	[XmlType(TypeName="AircraftEnginesData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AircraftEnginesData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_3988.ManufacturerContactData),ElementName="ManufacturerContactData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_3988.ManufacturerContactData __ManufacturerContactData;
		
		[XmlIgnore]
		public R_3988.ManufacturerContactData ManufacturerContactData
		{
			get {return __ManufacturerContactData;}
			set {__ManufacturerContactData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="EnginesCount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="integer",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __EnginesCount;
		
		[XmlIgnore]
		public string EnginesCount
		{ 
			get { return __EnginesCount; }
			set { __EnginesCount = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4420.EngineData),ElementName="EngineData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public EngineDataCollection __EngineDataCollection;
		
		[XmlIgnore]
		public EngineDataCollection EngineDataCollection
		{
			get
			{
				if (__EngineDataCollection == null) __EngineDataCollection = new EngineDataCollection();
				return __EngineDataCollection;
			}
			set {__EngineDataCollection = value;}
		}

		public AircraftEnginesData()
		{
		}
	}
}