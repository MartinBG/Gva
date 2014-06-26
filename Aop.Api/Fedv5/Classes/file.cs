using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class file
	{
		// ATTRIBUTES
		[XmlAttribute("key")]
		public string key  { get; set; }
		
		// ELEMENTS
		[XmlElement("name")]
		public name name { get; set; }
		
		[XmlElement("size")]
		public size size { get; set; }
		
		[XmlElement("data")]
		public data data { get; set; }
		
		// CONSTRUCTOR
		public file()
		{}
	}
}
