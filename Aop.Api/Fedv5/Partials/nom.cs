using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NOMv5
{
    public partial class nom
    {
        [XmlIgnore]
        public string this[string itemKey]
        {
            get
            {
                if (String.IsNullOrWhiteSpace(itemKey) || !this.item.Any(e => itemKey.Equals(e.key)))
                {
                    throw new Exception("The items of this nom do not contain an element with key " + itemKey);
                }
                else
                {
                    item item = this.item.FirstOrDefault(e => itemKey.Equals(e.key));
                    return item.value;
                }
            }
            set
            {
                if (String.IsNullOrWhiteSpace(itemKey) || !this.item.Any(e => itemKey.Equals(e.key)))
                {
                    throw new Exception("The items of this nom do not contain an element with key " + itemKey);
                }
                else
                {
                    item item = this.item.FirstOrDefault(e => itemKey.Equals(e.key));
                    item.value = value;
                }
            }
        }
    }
}
