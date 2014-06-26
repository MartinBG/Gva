using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace NOMv5
{
	public partial class nomenclature
	{
		
		// ELEMENTS
		[XmlElement("nom")]
		public List<nom> nom { get; set; }
		
		// CONSTRUCTOR
		public nomenclature()
		{}
	}
}
