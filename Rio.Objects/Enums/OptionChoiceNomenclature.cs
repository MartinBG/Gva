using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class OptionChoiceNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.OptionChoiceNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly OptionChoiceNomenclature Yes = new OptionChoiceNomenclature { ResourceKey = "Yes", Code = "01" };
        public static readonly OptionChoiceNomenclature No = new OptionChoiceNomenclature { ResourceKey = "No", Code = "02" };

        public static List<OptionChoiceNomenclature> Values = new List<OptionChoiceNomenclature>()
        {
            Yes,
            No
        };
    }
}
