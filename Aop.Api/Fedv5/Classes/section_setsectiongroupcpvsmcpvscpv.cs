using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class section_setsectiongroupcpvsmcpvscpv
	{
		// ATTRIBUTES
		[XmlAttribute("value")]
		public string value { get; set; }
		
		// ELEMENTS
		[XmlText]
		public string Value { get; set; }
		
		// CONSTRUCTOR
		public section_setsectiongroupcpvsmcpvscpv()
		{}
	}
}
