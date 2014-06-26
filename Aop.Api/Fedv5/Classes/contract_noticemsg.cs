using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class contract_noticemsg
	{
		// ATTRIBUTES
		[XmlAttribute("pref")]
		public string pref  { get; set; }
		
		[XmlAttribute("num")]
		public string num { get; set; }
		
		[XmlAttribute("date")]
		public string date { get; set; }
		
		// ELEMENTS
		[XmlText]
		public string Value { get; set; }
		
		// CONSTRUCTOR
		public contract_noticemsg()
		{}
	}
}
