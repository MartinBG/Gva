//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_5024
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-5024";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class TechnicalContractCollection : System.Collections.Generic.List<R_5022.TechnicalContract>
	{
	}



	[XmlType(TypeName="TechnicalContracts",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class TechnicalContracts
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_5022.TechnicalContract),ElementName="TechnicalContract",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public TechnicalContractCollection __TechnicalContractCollection;
		
		[XmlIgnore]
		public TechnicalContractCollection TechnicalContractCollection
		{
			get
			{
				if (__TechnicalContractCollection == null) __TechnicalContractCollection = new TechnicalContractCollection();
				return __TechnicalContractCollection;
			}
			set {__TechnicalContractCollection = value;}
		}

		public TechnicalContracts()
		{
		}
	}
}