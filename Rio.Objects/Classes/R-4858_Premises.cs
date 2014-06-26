//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4858
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4858";
	}




	[XmlType(TypeName="Premises",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class Premises
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PremisesLocation",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PremisesLocation;
		
		[XmlIgnore]
		public string PremisesLocation
		{ 
			get { return __PremisesLocation; }
			set { __PremisesLocation = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PremisesRoomCount",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PremisesRoomCount;
		
		[XmlIgnore]
		public string PremisesRoomCount
		{ 
			get { return __PremisesRoomCount; }
			set { __PremisesRoomCount = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PremisesRoomSize",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PremisesRoomSize;
		
		[XmlIgnore]
		public string PremisesRoomSize
		{ 
			get { return __PremisesRoomSize; }
			set { __PremisesRoomSize = value; }
		}

		public Premises()
		{
		}
	}
}