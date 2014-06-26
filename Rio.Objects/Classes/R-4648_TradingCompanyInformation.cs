//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4648
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4648";
	}




	[XmlType(TypeName="TradingCompanyInformation",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class TradingCompanyInformation
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="TradingCompanyName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __TradingCompanyName;
		
		[XmlIgnore]
		public string TradingCompanyName
		{ 
			get { return __TradingCompanyName; }
			set { __TradingCompanyName = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="TradingCompanyRegistrationDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __TradingCompanyRegistrationDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __TradingCompanyRegistrationDateSpecified { get { return __TradingCompanyRegistrationDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? TradingCompanyRegistrationDate
		{ 
			get { return __TradingCompanyRegistrationDate; }
			set { __TradingCompanyRegistrationDate = value; }
		}
		


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

		public TradingCompanyInformation()
		{
		}
	}
}
