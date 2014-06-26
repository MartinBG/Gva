using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class documentsectiongroupcpvs
	{
		
		// ELEMENTS
		[XmlElement("mcpv")]
		public List<documentsectiongroupcpvsmcpv> documentsectiongroupcpvsmcpv { get; set; }
		
		// CONSTRUCTOR
		public documentsectiongroupcpvs()
		{}
	}
}
