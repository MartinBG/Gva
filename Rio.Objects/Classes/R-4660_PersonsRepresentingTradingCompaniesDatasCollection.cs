//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4660
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4660";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PersonRepresentingTradingCompanyDataCollection : System.Collections.Generic.List<R_4658.PersonRepresentingTradingCompanyData>
	{
	}



	[XmlType(TypeName="PersonsRepresentingTradingCompaniesDatasCollection",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class PersonsRepresentingTradingCompaniesDatasCollection
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4658.PersonRepresentingTradingCompanyData),ElementName="PersonRepresentingTradingCompanyData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public PersonRepresentingTradingCompanyDataCollection PersonRepresentingTradingCompanyDataCollection { get; set; }

		public PersonsRepresentingTradingCompaniesDatasCollection()
		{
		}
	}
}
