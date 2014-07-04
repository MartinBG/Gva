//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000086
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000086";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class DocumentDeliveryFromDocumentStorageDataRecordCollection : System.Collections.Generic.List<DocumentDeliveryFromDocumentStorageDataRecord>
	{
	}



	[XmlType(TypeName="DocumentDeliveryFromDocumentStorageData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class DocumentDeliveryFromDocumentStorageData
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(DocumentDeliveryFromDocumentStorageDataRecord),ElementName="Record",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DocumentDeliveryFromDocumentStorageDataRecordCollection RecordCollection { get; set; }

		public DocumentDeliveryFromDocumentStorageData()
		{
		}
	}


	[XmlType(TypeName="DocumentDeliveryFromDocumentStorageDataRecord",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class DocumentDeliveryFromDocumentStorageDataRecord
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000192.OfficerInDocumentStorage2),ElementName="DeliveredBy",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000192.OfficerInDocumentStorage2 DeliveredBy { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DeliveryTime",Form=XmlSchemaForm.Qualified,DataType="dateTime",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? DeliveryTime { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000044.RegisteredDocumentURI),ElementName="RegisteredDocumentURI",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000044.RegisteredDocumentURI RegisteredDocumentURI { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(DocumentDeliveryFromDocumentStorageDataRecordAdditionalData),ElementName="AdditionalData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DocumentDeliveryFromDocumentStorageDataRecordAdditionalData AdditionalData { get; set; }

		public DocumentDeliveryFromDocumentStorageDataRecord()
		{
		}
	}


	[XmlType(TypeName="DocumentDeliveryFromDocumentStorageDataRecordAdditionalData",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class DocumentDeliveryFromDocumentStorageDataRecordAdditionalData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlAnyElement()]
		public System.Xml.XmlElement[] Any;

		public DocumentDeliveryFromDocumentStorageDataRecordAdditionalData()
		{
		}
	}
}
