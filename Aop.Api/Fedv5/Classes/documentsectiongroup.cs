using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public partial class documentsectiongroup
	{
		// ATTRIBUTES
		[XmlAttribute("id")]
		public string id { get; set; }
		
		[XmlAttribute("type")]
		public string type { get; set; }
		
		// ELEMENTS
		[XmlElement("field")]
		public List<documentsectiongroupfield> documentsectiongroupfield { get; set; }
		
		[XmlElement("cpvs")]
		public documentsectiongroupcpvs documentsectiongroupcpvs { get; set; }
		
		[XmlElement("award_criteria")]
		public award_criteria award_criteria { get; set; }
		
		[XmlElement("sgmsg")]
		public sgmsg sgmsg { get; set; }
		
		[XmlElement("addresses")]
		public addresses addresses { get; set; }
		
		[XmlElement("attachments")]
		public attachments attachments { get; set; }
		
		// CONSTRUCTOR
		public documentsectiongroup()
		{}
	}
}
