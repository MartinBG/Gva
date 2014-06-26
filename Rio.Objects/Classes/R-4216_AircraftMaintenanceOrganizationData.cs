//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_4216
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/R-4216";
	}




	[XmlType(TypeName="AircraftMaintenanceOrganizationData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AircraftMaintenanceOrganizationData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AircraftMaintenanceOrganizationApprovalNumber",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __AircraftMaintenanceOrganizationApprovalNumber;
		
		[XmlIgnore]
		public string AircraftMaintenanceOrganizationApprovalNumber
		{ 
			get { return __AircraftMaintenanceOrganizationApprovalNumber; }
			set { __AircraftMaintenanceOrganizationApprovalNumber = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="EntityName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __EntityName;
		
		[XmlIgnore]
		public string EntityName
		{ 
			get { return __EntityName; }
			set { __EntityName = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000142.Residence),ElementName="Residence",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000142.Residence __Residence;
		
		[XmlIgnore]
		public R_0009_000142.Residence Residence
		{
			get {return __Residence;}
			set {__Residence = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000172.EntityManagementAddress),ElementName="EntityManagementAddress",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000172.EntityManagementAddress __EntityManagementAddress;
		
		[XmlIgnore]
		public R_0009_000172.EntityManagementAddress EntityManagementAddress
		{
			get {return __EntityManagementAddress;}
			set {__EntityManagementAddress = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="PhoneNumbersDesc",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __PhoneNumbersDesc;
		
		[XmlIgnore]
		public string PhoneNumbersDesc
		{ 
			get { return __PhoneNumbersDesc; }
			set { __PhoneNumbersDesc = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="FaxNumbersDesc",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __FaxNumbersDesc;
		
		[XmlIgnore]
		public string FaxNumbersDesc
		{ 
			get { return __FaxNumbersDesc; }
			set { __FaxNumbersDesc = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="EmailAddress",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __EmailAddress;
		
		[XmlIgnore]
		public string EmailAddress
		{ 
			get { return __EmailAddress; }
			set { __EmailAddress = value; }
		}

		public AircraftMaintenanceOrganizationData()
		{
		}
	}
}