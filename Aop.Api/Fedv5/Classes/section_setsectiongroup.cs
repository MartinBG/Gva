using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public partial class section_setsectiongroup
	{
		// ATTRIBUTES
		[XmlAttribute("id")]
		public string id { get; set; }
		
		[XmlAttribute("type")]
		public string type { get; set; }
		
		// ELEMENTS
		[XmlElement("field")]
		public List<section_setsectiongroupfield> section_setsectiongroupfield { get; set; }
		
		[XmlElement("cpvs")]
		public section_setsectiongroupcpvs section_setsectiongroupcpvs { get; set; }
		
		// CONSTRUCTOR
		public section_setsectiongroup()
		{}
	}
}
