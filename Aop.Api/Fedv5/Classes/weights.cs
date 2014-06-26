using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class weights
	{
		
		// ELEMENTS
		[XmlElement("award")]
		public List<award> award { get; set; }
		
		// CONSTRUCTOR
		public weights()
		{}
	}
}
