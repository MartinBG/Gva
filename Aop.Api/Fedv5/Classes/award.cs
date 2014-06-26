using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class award
	{
		// ATTRIBUTES
		[XmlAttribute("criteria")]
		public string criteria { get; set; }
		
		[XmlAttribute("weight")]
		public string weight  { get; set; }
		
		// ELEMENTS
		[XmlText]
		public string Value { get; set; }
		
		// CONSTRUCTOR
		public award()
		{}
	}
}
