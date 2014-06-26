using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class sgmsg
	{
		
		// ELEMENTS
		[XmlElement("yesno")]
		public yesno yesno { get; set; }
		
		[XmlElement("contract_notice")]
		public contract_notice contract_notice { get; set; }
		
		[XmlElement("other_notice")]
		public other_notice other_notice { get; set; }
		
		// CONSTRUCTOR
		public sgmsg()
		{}
	}
}
