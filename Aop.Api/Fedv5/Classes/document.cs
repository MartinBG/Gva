using System;
using System.ComponentModel;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Globalization;

namespace FEDv5
{

    public partial class document
    {
        // ATTRIBUTES
        [XmlAttribute("Id")]
        public string Id { get; set; }

        [XmlAttribute("type")]
        public string type { get; set; }

        [XmlAttribute("version")]
        public string version { get; set; }

        [XmlAttribute("application")]
        public string application { get; set; }

        [XmlAttribute("validated")]
        public string validated { get; set; }

        [XmlAttribute("imported")]
        public string imported { get; set; }

        [XmlAttribute("digest")]
        public string digest { get; set; }

        // ELEMENTS
        [XmlElement("section")]
        public List<documentsection> documentsection { get; set; }

        [XmlElement("section_set")]
        public section_set section_set { get; set; }

        // CONSTRUCTOR
        public document()
        { }
    }
}
