using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class documentsectiongroupcpvsmcpv
	{
		// ATTRIBUTES
		[XmlAttribute("value")]
		public string value  { get; set; }
		
		// ELEMENTS
		[XmlElement("scpv")]
		public scpv scpv { get; set; }
		
		// CONSTRUCTOR
		public documentsectiongroupcpvsmcpv()
		{}
	}
}
