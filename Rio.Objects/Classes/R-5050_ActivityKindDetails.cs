//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_5050
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-5050";
	}




	[XmlType(TypeName="ActivityKindDetails",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ActivityKindDetails
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4596.ActivityKind),ElementName="ActivityKind",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4596.ActivityKind __ActivityKind;
		
		[XmlIgnore]
		public R_4596.ActivityKind ActivityKind
		{
			get {return __ActivityKind;}
			set {__ActivityKind = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ActivitySchedule",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ActivitySchedule;
		
		[XmlIgnore]
		public string ActivitySchedule
		{ 
			get { return __ActivitySchedule; }
			set { __ActivitySchedule = value; }
		}

		public ActivityKindDetails()
		{
		}
	}
}