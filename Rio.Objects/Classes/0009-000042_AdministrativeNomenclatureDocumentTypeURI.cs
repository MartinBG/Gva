//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000042
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000042";
	}




	[XmlType(TypeName="AdministrativeNomenclatureDocumentTypeURI",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AdministrativeNomenclatureDocumentTypeURI
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000031.UnifiedDataURI),ElementName="SegmentUnifiedDataURI",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000031.UnifiedDataURI __SegmentUnifiedDataURI;
		
		[XmlIgnore]
		public R_0009_000031.UnifiedDataURI SegmentUnifiedDataURI
		{
			get {return __SegmentUnifiedDataURI;}
			set {__SegmentUnifiedDataURI = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000001.DocumentURI),ElementName="OfficialDocumentRegisterURI",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000001.DocumentURI __OfficialDocumentRegisterURI;
		
		[XmlIgnore]
		public R_0009_000001.DocumentURI OfficialDocumentRegisterURI
		{
			get {return __OfficialDocumentRegisterURI;}
			set {__OfficialDocumentRegisterURI = value;}
		}

		public AdministrativeNomenclatureDocumentTypeURI()
		{
		}
	}
}
