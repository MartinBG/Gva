//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000118
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000118";
	}




	[XmlType(TypeName="RelativeData",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class RelativeData
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000005.PersonNames),ElementName="PersonNames",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000005.PersonNames __PersonNames;
		
		[XmlIgnore]
		public R_0009_000005.PersonNames PersonNames
		{
			get {return __PersonNames;}
			set {__PersonNames = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000006.PersonIdentifier),ElementName="PersonIdentifier",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000006.PersonIdentifier __PersonIdentifier;
		
		[XmlIgnore]
		public R_0009_000006.PersonIdentifier PersonIdentifier
		{
			get {return __PersonIdentifier;}
			set {__PersonIdentifier = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="BirthDate",Form=XmlSchemaForm.Qualified,DataType="date",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public DateTime? __BirthDate;
		
		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlIgnore]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool __BirthDateSpecified { get { return __BirthDate.HasValue; } }
		
		[XmlIgnore]
		public DateTime? BirthDate
		{ 
			get { return __BirthDate; }
			set { __BirthDate = value; }
		}
		


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="Relationship",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Relationship;
		
		[XmlIgnore]
		public string Relationship
		{ 
			get { return __Relationship; }
			set { __Relationship = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000094.PersonAddress),ElementName="PersonAddress",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000094.PersonAddress __PersonAddress;
		
		[XmlIgnore]
		public R_0009_000094.PersonAddress PersonAddress
		{
			get {return __PersonAddress;}
			set {__PersonAddress = value;}
		}

		public RelativeData()
		{
		}
	}
}