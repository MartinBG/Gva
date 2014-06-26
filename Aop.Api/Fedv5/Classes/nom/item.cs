using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace NOMv5
{
	
	public class item
	{
		// ATTRIBUTES
		[XmlAttribute("key")]
		public string key { get; set; }
		
		[XmlAttribute("value")]
		public string value { get; set; }
		
		[XmlAttribute("id")]
		public string id { get; set; }
		
		[XmlAttribute("style")]
		public string style { get; set; }
		
		[XmlIgnore]
		public decimal? area { get; set; }
		[XmlAttribute("area")]
		public string areaString
		{
			get { return area==null ? "" : area.Value.ToString(CultureInfo.InvariantCulture); }
			set
			{
				if (String.IsNullOrWhiteSpace(value)) area = null;
				else area = decimal.Parse(value);
			}
		}
		
		[XmlAttribute("url")]
		public string url { get; set; }
		
		[XmlIgnore]
		public int? _default { get; set; }
		[XmlAttribute("default")]
		public string defaultString
		{
            get { return _default == null ? "" : _default.Value.ToString(CultureInfo.InvariantCulture); }
			set
			{
                if (String.IsNullOrWhiteSpace(value)) _default = null;
				else _default = int.Parse(value);
			}
		}
		
		// ELEMENTS
		[XmlText]
		public string Value { get; set; }
		
		// CONSTRUCTOR
		public item()
		{}
	}
}
