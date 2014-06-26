using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{
	
	public class address
	{
		// ATTRIBUTES
		[XmlAttribute("key")]
		public string key  { get; set; }
		
		// ELEMENTS
		[XmlElement("title")]
		public title title { get; set; }
		
		[XmlElement("OfficialName")]
		public OfficialName OfficialName { get; set; }
		
		[XmlElement("Number")]
		public Number Number { get; set; }
		
		[XmlElement("PostalAddress")]
		public PostalAddress PostalAddress { get; set; }
		
		[XmlElement("Town")]
		public Town Town { get; set; }
		
		[XmlElement("PostalCode")]
		public PostalCode PostalCode { get; set; }
		
		[XmlElement("Country")]
		public Country Country { get; set; }
		
		[XmlElement("ContactPoints")]
		public ContactPoints ContactPoints { get; set; }
		
		[XmlElement("ForTheAttentionOf")]
		public ForTheAttentionOf ForTheAttentionOf { get; set; }
		
		[XmlElement("Telephone")]
		public Telephone Telephone { get; set; }
		
		[XmlElement("Email")]
		public Email Email { get; set; }
		
		[XmlElement("Fax")]
		public Fax Fax { get; set; }
		
		[XmlElement("URL1")]
		public URL1 URL1 { get; set; }
		
		[XmlElement("URL2")]
		public URL2 URL2 { get; set; }
		
		// CONSTRUCTOR
		public address()
		{}
	}
}
