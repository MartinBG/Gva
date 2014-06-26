using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace NUTv5
{
	
	public class nuts
	{
		
		// ELEMENTS
		[XmlElement("item")]
		public List<item> item { get; set; }
		
		// CONSTRUCTOR
		public nuts()
		{}
	}
}
