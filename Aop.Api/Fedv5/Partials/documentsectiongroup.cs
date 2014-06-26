using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FEDv5
{
    public partial class documentsectiongroup
    {
        [XmlIgnore]
        public string this[string fieldKey]
        {
            get
            {
                if (String.IsNullOrWhiteSpace(fieldKey) || !this.documentsectiongroupfield.Any(e => fieldKey.Equals(e.key)))
                {
                    return string.Empty;
                    //throw new Exception("The fields of this group do not contain an element with key " + fieldKey);
                }
                else
                {
                    documentsectiongroupfield field = this.documentsectiongroupfield.FirstOrDefault(e => fieldKey.Equals(e.key));
                    return field.value;
                }
            }
            set
            {
                if (String.IsNullOrWhiteSpace(fieldKey) || !this.documentsectiongroupfield.Any(e => fieldKey.Equals(e.key)))
                {
                    throw new Exception("The fields of this group do not contain an element with key " + fieldKey);
                }
                else
                {
                    documentsectiongroupfield field = this.documentsectiongroupfield.FirstOrDefault(e => fieldKey.Equals(e.key));
                    field.value = value;
                }
            }
        }

        public bool HasField(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                return this.documentsectiongroupfield.Any(f => f.key == key);
            }

            return false;
        }
    }
}
