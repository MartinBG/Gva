//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4158
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4158";
	}




	[XmlType(TypeName="AircraftType",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AircraftType
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftTypeCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AircraftTypeCode;
		
		[XmlIgnore]
		public string AircraftTypeCode
		{ 
			get { return __AircraftTypeCode; }
			set { __AircraftTypeCode = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftTypeName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AircraftTypeName;
		
		[XmlIgnore]
		public string AircraftTypeName
		{ 
			get { return __AircraftTypeName; }
			set { __AircraftTypeName = value; }
		}

		public AircraftType()
		{
		}
	}
}
