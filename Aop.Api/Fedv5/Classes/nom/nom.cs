using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace NOMv5
{
	public partial class nom
	{
		// ATTRIBUTES
		[XmlAttribute("id")]
		public string id { get; set; }
		
		// ELEMENTS
		[XmlElement("item")]
		public List<item> item { get; set; }
		
		// CONSTRUCTOR
		public nom()
		{}
	}
}
