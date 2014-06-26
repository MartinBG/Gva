//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4440
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4440";
	}




	[XmlType(TypeName="PropellerData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PropellerData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="CountryGRAOCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CountryGRAOCode;
		
		[XmlIgnore]
		public string CountryGRAOCode
		{ 
			get { return __CountryGRAOCode; }
			set { __CountryGRAOCode = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="CountryName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __CountryName;
		
		[XmlIgnore]
		public string CountryName
		{ 
			get { return __CountryName; }
			set { __CountryName = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PropellerManufacturePlace",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PropellerManufacturePlace;
		
		[XmlIgnore]
		public string PropellerManufacturePlace
		{ 
			get { return __PropellerManufacturePlace; }
			set { __PropellerManufacturePlace = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PropellerManufactureDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __PropellerManufactureDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __PropellerManufactureDateSpecified { get { return __PropellerManufactureDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? PropellerManufactureDate
		{ 
			get { return __PropellerManufactureDate; }
			set { __PropellerManufactureDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4434.PropellerType),ElementName="PropellerType",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4434.PropellerType __PropellerType;
		
		[XmlIgnore]
		public R_4434.PropellerType PropellerType
		{
			get {return __PropellerType;}
			set {__PropellerType = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PropellerSerialNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PropellerSerialNumber;
		
		[XmlIgnore]
		public string PropellerSerialNumber
		{ 
			get { return __PropellerSerialNumber; }
			set { __PropellerSerialNumber = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PropellerUsage",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PropellerUsage;
		
		[XmlIgnore]
		public string PropellerUsage
		{ 
			get { return __PropellerUsage; }
			set { __PropellerUsage = value; }
		}

		public PropellerData()
		{
		}
	}
}
