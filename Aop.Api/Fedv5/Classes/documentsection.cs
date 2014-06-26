using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public partial class documentsection
	{
		// ATTRIBUTES
		[XmlAttribute("id")]
		public string id { get; set; }
		
		// ELEMENTS
		[XmlElement("group")]
		public List<documentsectiongroup> documentsectiongroup { get; set; }
		
		// CONSTRUCTOR
		public documentsection()
		{}
	}
}
