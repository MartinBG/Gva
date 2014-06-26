//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000078
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000078";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class RecordCollection : System.Collections.Generic.List<Record>
	{
	}



	[XmlType(TypeName="DocumentRegistrationInDocumentStorageData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class DocumentRegistrationInDocumentStorageData
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(Record),ElementName="Record",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public RecordCollection __RecordCollection;
		
		[XmlIgnore]
		public RecordCollection RecordCollection
		{
			get
			{
				if (__RecordCollection == null) __RecordCollection = new RecordCollection();
				return __RecordCollection;
			}
			set {__RecordCollection = value;}
		}

		public DocumentRegistrationInDocumentStorageData()
		{
		}
	}


	[XmlType(TypeName="Record",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class Record
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000192.OfficerInDocumentStorage2),ElementName="RegisteredBy",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000192.OfficerInDocumentStorage2 __RegisteredBy;
		
		[XmlIgnore]
		public R_0009_000192.OfficerInDocumentStorage2 RegisteredBy
		{
			get {return __RegisteredBy;}
			set {__RegisteredBy = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="RegistrationTime",Form=XmlSchemaForm.Qualified,DataType="dateTime",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __RegistrationTime;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __RegistrationTimeSpecified { get { return __RegistrationTime.HasValue; } }
		
		[XmlIgnore]
		public DateTime? RegistrationTime
		{ 
			get { return __RegistrationTime; }
			set { __RegistrationTime = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000044.RegisteredDocumentURI),ElementName="RegisteredDocumentURI",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000044.RegisteredDocumentURI __RegisteredDocumentURI;
		
		[XmlIgnore]
		public R_0009_000044.RegisteredDocumentURI RegisteredDocumentURI
		{
			get {return __RegisteredDocumentURI;}
			set {__RegisteredDocumentURI = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="StorageLocation",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __StorageLocation;
		
		[XmlIgnore]
		public string StorageLocation
		{ 
			get { return __StorageLocation; }
			set { __StorageLocation = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(RecordAdditionalData),ElementName="AdditionalData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public RecordAdditionalData __AdditionalData;
		
		[XmlIgnore]
		public RecordAdditionalData AdditionalData
		{
			get {return __AdditionalData;}
			set {__AdditionalData = value;}
		}

		public Record()
		{
		}
	}


	[XmlType(TypeName="RecordAdditionalData",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class RecordAdditionalData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAnyElement()]
		public System.Xml.XmlElement[] Any;

		public RecordAdditionalData()
		{
		}
	}
}
