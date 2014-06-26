using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FEDv5
{
    public partial class section_setsectiongroup
    {
        [XmlIgnore]
        public string this[string fieldKey]
        {
            get
            {
                if (String.IsNullOrWhiteSpace(fieldKey) || !this.section_setsectiongroupfield.Any(e => fieldKey.Equals(e.key)))
                {
                    throw new Exception("The fields of this group do not contain an element with key " + fieldKey);
                }
                else
                {
                    section_setsectiongroupfield field = this.section_setsectiongroupfield.FirstOrDefault(e => fieldKey.Equals(e.key));
                    return field.value;
                }
            }
            set
            {
                if (String.IsNullOrWhiteSpace(fieldKey) || !this.section_setsectiongroupfield.Any(e => fieldKey.Equals(e.key)))
                {
                    throw new Exception("The fields of this group do not contain an element with key " + fieldKey);
                }
                else
                {
                    section_setsectiongroupfield field = this.section_setsectiongroupfield.FirstOrDefault(e => fieldKey.Equals(e.key));
                    field.value = value;
                }
            }
        }
    }
}
