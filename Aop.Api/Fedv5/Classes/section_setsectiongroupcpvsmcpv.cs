using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class section_setsectiongroupcpvsmcpv
	{
		// ATTRIBUTES
		[XmlAttribute("value")]
		public string value  { get; set; }
		
		// ELEMENTS
		[XmlElement("scpv")]
		public section_setsectiongroupcpvsmcpvscpv section_setsectiongroupcpvsmcpvscpv { get; set; }
		
		// CONSTRUCTOR
		public section_setsectiongroupcpvsmcpv()
		{}
	}
}
