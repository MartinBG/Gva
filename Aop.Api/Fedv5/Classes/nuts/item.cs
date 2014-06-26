using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace NUTv5
{
	
	public class item
	{
		// ATTRIBUTES
		[XmlAttribute("key")]
		public string key { get; set; }
		
		[XmlAttribute("value")]
		public string value { get; set; }
		
		// ELEMENTS
		[XmlText]
		public string Value { get; set; }
		
		// CONSTRUCTOR
		public item()
		{}
	}
}
