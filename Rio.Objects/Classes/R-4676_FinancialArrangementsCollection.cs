//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4676
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4676";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class FinancialArrangementCollection : System.Collections.Generic.List<R_4674.FinancialArrangement>
	{
	}



	[XmlType(TypeName="FinancialArrangementsCollection",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class FinancialArrangementsCollection
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4674.FinancialArrangement),ElementName="FinancialArrangement",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public FinancialArrangementCollection __FinancialArrangementCollection;
		
		[XmlIgnore]
		public FinancialArrangementCollection FinancialArrangementCollection
		{
			get
			{
				if (__FinancialArrangementCollection == null) __FinancialArrangementCollection = new FinancialArrangementCollection();
				return __FinancialArrangementCollection;
			}
			set {__FinancialArrangementCollection = value;}
		}

		public FinancialArrangementsCollection()
		{
		}
	}
}