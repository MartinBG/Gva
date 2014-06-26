using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class section_setsectiongroupcpvs
	{
		
		// ELEMENTS
		[XmlElement("mcpv")]
		public List<section_setsectiongroupcpvsmcpv> section_setsectiongroupcpvsmcpv { get; set; }
		
		// CONSTRUCTOR
		public section_setsectiongroupcpvs()
		{}
	}
}
