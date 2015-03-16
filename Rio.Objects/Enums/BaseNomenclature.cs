using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rio.Objects.Enums
{
    [Serializable]
    public class BaseNomenclature
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string ParentValue { get; set; }

        public BaseNomenclature() { }
        public BaseNomenclature(string value, string text)
        {
            Value = value;
            Text = text;
            Description = String.Empty;
        }
        public BaseNomenclature(string value, string text, string description)
        {
            Value = value;
            Text = text;
            Description = description;
        }
        public BaseNomenclature(string value, string text, string description, string parentValue)
        {
            Value = value;
            Text = text;
            Description = description;
            ParentValue = parentValue;
        }

        public List<BaseNomenclature> Values { get; set; }
    }
}
