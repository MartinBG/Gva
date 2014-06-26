using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class section_set
	{
		// ATTRIBUTES
		[XmlAttribute("id")]
		public string id { get; set; }
		
		// ELEMENTS
		[XmlElement("section")]
		public List<section_setsection> section_setsection { get; set; }
		
		// CONSTRUCTOR
		public section_set()
		{}
	}
}
