using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FEDv5
{
    public partial class documentsection
    {
        [XmlIgnore]
        public documentsectiongroup this[string groupId]
        {
            get
            {
                if (String.IsNullOrWhiteSpace(groupId) || !this.documentsectiongroup.Any(e => groupId.Equals(e.id)))
                {
                    return null;
                    //throw new Exception("The groups of this section do not contain an element with id " + groupId);
                }
                else
                {
                    documentsectiongroup group = this.documentsectiongroup.FirstOrDefault(e => groupId.Equals(e.id));
                    return group;
                }
            }
            set
            {
                throw new Exception("The group cannot be set.");
            }
        }

        public string GetValueByKeyAndGroup(string groupKey, string fieldKey)
        {
            try
            {
                string value = this[groupKey][fieldKey];
                return value;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public bool HasGroup(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                return this.documentsectiongroup.Any(g => g.id.Equals(key));
            }

            return false;
        }
    }
}
