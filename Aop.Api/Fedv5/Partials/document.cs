using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FEDv5
{
    public partial class document
    {
        [XmlIgnore]
        public documentsection this[string sectionId]
        {
            get
            {
                if (String.IsNullOrWhiteSpace(sectionId) || !this.documentsection.Any(e => sectionId.Equals(e.id)))
                {
                    throw new Exception("The sections of this document do not contain an element with id " + sectionId);
                }
                else
                {
                    documentsection document = this.documentsection.FirstOrDefault(e => sectionId.Equals(e.id));
                    return document;
                }
            }
            set
            {
                throw new Exception("The sections cannot be set.");
            }
        }
    }
}
