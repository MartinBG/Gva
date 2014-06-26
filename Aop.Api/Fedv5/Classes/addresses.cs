using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class addresses
	{
		
		// ELEMENTS
		[XmlElement("address")]
		public List<address> address { get; set; }
		
		// CONSTRUCTOR
		public addresses()
		{}
	}
}
