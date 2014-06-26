//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4998
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4998";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PermissibleActivityCollection : System.Collections.Generic.List<R_4996.PermissibleActivity>
	{
	}



	[XmlType(TypeName="AerialWorkAircraftData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AerialWorkAircraftData
	{

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
		[XmlElement(ElementName="AircraftSerialNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AircraftSerialNumber;
		
		[XmlIgnore]
		public string AircraftSerialNumber
		{ 
			get { return __AircraftSerialNumber; }
			set { __AircraftSerialNumber = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4996.PermissibleActivity),ElementName="PermissibleActivity",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PermissibleActivityCollection __PermissibleActivityCollection;
		
		[XmlIgnore]
		public PermissibleActivityCollection PermissibleActivityCollection
		{
			get
			{
				if (__PermissibleActivityCollection == null) __PermissibleActivityCollection = new PermissibleActivityCollection();
				return __PermissibleActivityCollection;
			}
			set {__PermissibleActivityCollection = value;}
		}

		public AerialWorkAircraftData()
		{
		}
	}
}