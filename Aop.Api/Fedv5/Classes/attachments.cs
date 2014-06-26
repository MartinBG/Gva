using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class attachments
	{
		
		// ELEMENTS
		[XmlElement("file")]
		public file file { get; set; }
		
		// CONSTRUCTOR
		public attachments()
		{}
	}
}
