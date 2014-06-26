using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FEDv5
{
    public partial class section_setsection
    {
        [XmlIgnore]
        public section_setsectiongroup this[string groupId]
        {
            get
            {
                if (String.IsNullOrWhiteSpace(groupId) || !this.section_setsectiongroup.Any(e => groupId.Equals(e.id)))
                {
                    throw new Exception("The groups of this section do not contain an element with id " + groupId);
                }
                else
                {
                    section_setsectiongroup group = this.section_setsectiongroup.FirstOrDefault(e => groupId.Equals(e.id));
                    return group;
                }
            }
            set
            {
                throw new Exception("The group cannot be set.");
            }
        }
    }
}
