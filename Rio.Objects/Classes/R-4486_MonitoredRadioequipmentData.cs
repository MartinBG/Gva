//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4486
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4486";
	}




	[XmlType(TypeName="MonitoredRadioequipmentData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class MonitoredRadioequipmentData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="MonitoredRadioequipmentType",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MonitoredRadioequipmentType;
		
		[XmlIgnore]
		public string MonitoredRadioequipmentType
		{ 
			get { return __MonitoredRadioequipmentType; }
			set { __MonitoredRadioequipmentType = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="MonitoredRadioequipmentCount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="integer",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MonitoredRadioequipmentCount;
		
		[XmlIgnore]
		public string MonitoredRadioequipmentCount
		{ 
			get { return __MonitoredRadioequipmentCount; }
			set { __MonitoredRadioequipmentCount = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="MonitoredRadioequipmentManufacturer",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MonitoredRadioequipmentManufacturer;
		
		[XmlIgnore]
		public string MonitoredRadioequipmentManufacturer
		{ 
			get { return __MonitoredRadioequipmentManufacturer; }
			set { __MonitoredRadioequipmentManufacturer = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="MonitoredRadioequipmentModel",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MonitoredRadioequipmentModel;
		
		[XmlIgnore]
		public string MonitoredRadioequipmentModel
		{ 
			get { return __MonitoredRadioequipmentModel; }
			set { __MonitoredRadioequipmentModel = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="MonitoredRadioequipmentPower",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MonitoredRadioequipmentPower;
		
		[XmlIgnore]
		public string MonitoredRadioequipmentPower
		{ 
			get { return __MonitoredRadioequipmentPower; }
			set { __MonitoredRadioequipmentPower = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="MonitoredRadioequipmentEmissionClass",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MonitoredRadioequipmentEmissionClass;
		
		[XmlIgnore]
		public string MonitoredRadioequipmentEmissionClass
		{ 
			get { return __MonitoredRadioequipmentEmissionClass; }
			set { __MonitoredRadioequipmentEmissionClass = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="MonitoredRadioequipmentFrequency",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __MonitoredRadioequipmentFrequency;
		
		[XmlIgnore]
		public string MonitoredRadioequipmentFrequency
		{ 
			get { return __MonitoredRadioequipmentFrequency; }
			set { __MonitoredRadioequipmentFrequency = value; }
		}

		public MonitoredRadioequipmentData()
		{
		}
	}
}
