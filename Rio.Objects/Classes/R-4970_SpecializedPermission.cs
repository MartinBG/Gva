//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4970
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4970";
	}




	[XmlType(TypeName="SpecializedPermission",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SpecializedPermission
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="SpecializedPermissionCode",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __SpecializedPermissionCode;
		
		[XmlIgnore]
		public string SpecializedPermissionCode
		{ 
			get { return __SpecializedPermissionCode; }
			set { __SpecializedPermissionCode = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="SpecializedPermissionName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __SpecializedPermissionName;
		
		[XmlIgnore]
		public string SpecializedPermissionName
		{ 
			get { return __SpecializedPermissionName; }
			set { __SpecializedPermissionName = value; }
		}

		public SpecializedPermission()
		{
		}
	}
}
