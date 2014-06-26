//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000037
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000037";
	}




	[XmlType(TypeName="AISObjectClassificationData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AISObjectClassificationData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000028.AISUserOrAISURI),ElementName="ClassifiedBy",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000028.AISUserOrAISURI __ClassifiedBy;
		
		[XmlIgnore]
		public R_0009_000028.AISUserOrAISURI ClassifiedBy
		{
			get {return __ClassifiedBy;}
			set {__ClassifiedBy = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ClassificationTime",Form=XmlSchemaForm.Qualified,DataType="dateTime",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __ClassificationTime;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __ClassificationTimeSpecified { get { return __ClassificationTime.HasValue; } }
		
		[XmlIgnore]
		public DateTime? ClassificationTime
		{ 
			get { return __ClassificationTime; }
			set { __ClassificationTime = value; }
		}
		


		public AISObjectClassificationData()
		{
		}
	}
}