
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class NewUsedNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.NewUsedNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly NewUsedNomenclature New = new NewUsedNomenclature { ResourceKey = "New", Code = "01" };
        public static readonly NewUsedNomenclature Used = new NewUsedNomenclature { ResourceKey = "Used", Code = "02" };
        
    }
}
