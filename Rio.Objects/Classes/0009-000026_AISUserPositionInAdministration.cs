//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.0.0.0
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace R_0009_000026
{

	public struct Declarations
	{
		public const string SchemaVersion = "http://ereg.egov.bg/segment/0009-000026";
	}




	[XmlType(TypeName="AISUserPositionInAdministration",Namespace=Declarations.SchemaVersion),Serializable]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public partial class AISUserPositionInAdministration
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="AdministrativeBodyName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string AdministrativeBodyName { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(Type=typeof(Position),ElementName="Position",IsNullable=false,Form=XmlSchemaForm.Qualified,Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public Position Position { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="OnCivilContract",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="boolean",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public bool OnCivilContract { get; set; }

		public AISUserPositionInAdministration()
		{
		}
	}


	[XmlType(TypeName="Position",Namespace=Declarations.SchemaVersion),Serializable]
	public partial class Position
	{

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="EKDAPositonName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string EKDAPositonName { get; set; }

		[System.Web.Script.Serialization.ScriptIgnore]
		[XmlElement(ElementName="DepartmentName",IsNullable=false,Form=XmlSchemaForm.Qualified,DataType="string",Namespace=Declarations.SchemaVersion)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public string DepartmentName { get; set; }

		public Position()
		{
		}
	}
}
