//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_5264
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-5264";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class QualificationClassRecordedOwnershipLicenseCollection : System.Collections.Generic.List<R_5262.QualificationClassRecordedOwnershipLicense>
	{
	}



	[XmlType(TypeName="QualificationClassesRecordedOwnershipLicenses",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class QualificationClassesRecordedOwnershipLicenses
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_5262.QualificationClassRecordedOwnershipLicense),ElementName="QualificationClassRecordedOwnershipLicense",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public QualificationClassRecordedOwnershipLicenseCollection QualificationClassRecordedOwnershipLicenseCollection { get; set; }

		public QualificationClassesRecordedOwnershipLicenses()
		{
		}
	}
}
