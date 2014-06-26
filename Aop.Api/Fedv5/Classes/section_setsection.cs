using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public partial class section_setsection
	{
		// ATTRIBUTES
		[XmlAttribute("id")]
		public string id { get; set; }
		
		[XmlAttribute("type")]
		public string type { get; set; }
		
		// ELEMENTS
		[XmlElement("group")]
		public List<section_setsectiongroup> section_setsectiongroup { get; set; }
		
		// CONSTRUCTOR
		public section_setsection()
		{}
	}
}
