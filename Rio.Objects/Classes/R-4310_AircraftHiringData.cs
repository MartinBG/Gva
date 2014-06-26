//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4310
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4310";
	}




	[XmlType(TypeName="AircraftHiringData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AircraftHiringData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftOwnerName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AircraftOwnerName;
		
		[XmlIgnore]
		public string AircraftOwnerName
		{ 
			get { return __AircraftOwnerName; }
			set { __AircraftOwnerName = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftHiringTransferContractNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AircraftHiringTransferContractNumber;
		
		[XmlIgnore]
		public string AircraftHiringTransferContractNumber
		{ 
			get { return __AircraftHiringTransferContractNumber; }
			set { __AircraftHiringTransferContractNumber = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftHiringTransferContractDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __AircraftHiringTransferContractDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __AircraftHiringTransferContractDateSpecified { get { return __AircraftHiringTransferContractDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? AircraftHiringTransferContractDate
		{ 
			get { return __AircraftHiringTransferContractDate; }
			set { __AircraftHiringTransferContractDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftHiringTransferContractConclusionPlace",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AircraftHiringTransferContractConclusionPlace;
		
		[XmlIgnore]
		public string AircraftHiringTransferContractConclusionPlace
		{ 
			get { return __AircraftHiringTransferContractConclusionPlace; }
			set { __AircraftHiringTransferContractConclusionPlace = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4286.CorrespondenceAddress),ElementName="CorrespondenceAddress",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4286.CorrespondenceAddress __CorrespondenceAddress;
		
		[XmlIgnore]
		public R_4286.CorrespondenceAddress CorrespondenceAddress
		{
			get {return __CorrespondenceAddress;}
			set {__CorrespondenceAddress = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_4288.CorrespondenceAddressAbroad),ElementName="CorrespondenceAddressAbroad",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_4288.CorrespondenceAddressAbroad __CorrespondenceAddressAbroad;
		
		[XmlIgnore]
		public R_4288.CorrespondenceAddressAbroad CorrespondenceAddressAbroad
		{
			get {return __CorrespondenceAddressAbroad;}
			set {__CorrespondenceAddressAbroad = value;}
		}

		public AircraftHiringData()
		{
		}
	}
}