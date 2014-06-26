using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class award_criteria
	{
		
		// ELEMENTS
		[XmlElement("low_price")]
		public low_price low_price { get; set; }
		
		[XmlElement("criteria")]
		public criteria criteria { get; set; }
		
		[XmlElement("weights")]
		public weights weights { get; set; }
		
		// CONSTRUCTOR
		public award_criteria()
		{}
	}
}
