using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class contract_notice
	{
		// ATTRIBUTES
		[XmlAttribute("value")]
		public string value  { get; set; }
		
		// ELEMENTS
		[XmlElement("msg")]
		public List<contract_noticemsg> contract_noticemsg { get; set; }
		
		// CONSTRUCTOR
		public contract_notice()
		{}
	}
}
