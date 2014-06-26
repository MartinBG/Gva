//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000036
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000036";
	}


	[Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class SectionCollection : System.Collections.Generic.List<Section>
	{
	}



	[XmlType(TypeName="ClassificationScheme",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class ClassificationScheme
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ID",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ID;
		
		[XmlIgnore]
		public string ID
		{ 
			get { return __ID; }
			set { __ID = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Name;
		
		[XmlIgnore]
		public string Name
		{ 
			get { return __Name; }
			set { __Name = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(Sections),ElementName="Sections",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Sections __Sections;
		
		[XmlIgnore]
		public Sections Sections
		{
			get {return __Sections;}
			set {__Sections = value;}
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(R_0009_000032.AISObjectCreationData),ElementName="ObjectCreationData",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public R_0009_000032.AISObjectCreationData __ObjectCreationData;
		
		[XmlIgnore]
		public R_0009_000032.AISObjectCreationData ObjectCreationData
		{
			get {return __ObjectCreationData;}
			set {__ObjectCreationData = value;}
		}

		public ClassificationScheme()
		{
		}
	}


	[XmlType(TypeName="Sections",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class Sections
	{


		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(Section),ElementName="Section",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SectionCollection __SectionCollection;
		
		[XmlIgnore]
		public SectionCollection SectionCollection
		{
			get
			{
				if (__SectionCollection == null) __SectionCollection = new SectionCollection();
				return __SectionCollection;
			}
			set {__SectionCollection = value;}
		}

		public Sections()
		{
		}
	}


	[XmlType(TypeName="Section",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class Section
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="ID",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __ID;
		
		[XmlIgnore]
		public string ID
		{ 
			get { return __ID; }
			set { __ID = value; }
		}

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="Name",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string __Name;
		
		[XmlIgnore]
		public string Name
		{ 
			get { return __Name; }
			set { __Name = value; }
		}

		public Section()
		{
		}
	}
}
