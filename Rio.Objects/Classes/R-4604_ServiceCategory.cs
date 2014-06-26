//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4604
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4604";
	}




	[XmlType(TypeName="ServiceCategory",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ServiceCategory
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ServiceCategoryCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ServiceCategoryCode;
		
		[XmlIgnore]
		public string ServiceCategoryCode
		{ 
			get { return __ServiceCategoryCode; }
			set { __ServiceCategoryCode = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ServiceCategoryName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ServiceCategoryName;
		
		[XmlIgnore]
		public string ServiceCategoryName
		{ 
			get { return __ServiceCategoryName; }
			set { __ServiceCategoryName = value; }
		}

		public ServiceCategory()
		{
		}
	}
}
