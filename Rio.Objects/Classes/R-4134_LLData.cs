//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4134
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4134";
	}




	[XmlType(TypeName="LLData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class LLData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4112.ASCoursePeriod),ElementName="ASCoursePeriod",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4112.ASCoursePeriod __ASCoursePeriod;
		
		[XmlIgnore]
		public R_4112.ASCoursePeriod ASCoursePeriod
		{
			get {return __ASCoursePeriod;}
			set {__ASCoursePeriod = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4126.ASLLData),ElementName="ASLLData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4126.ASLLData __ASLLData;
		
		[XmlIgnore]
		public R_4126.ASLLData ASLLData
		{
			get {return __ASLLData;}
			set {__ASLLData = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4132.LEC),ElementName="LEC",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4132.LEC __LEC;
		
		[XmlIgnore]
		public R_4132.LEC LEC
		{
			get {return __LEC;}
			set {__LEC = value;}
		}

		public LLData()
		{
		}
	}
}