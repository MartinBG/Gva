using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class FlagIndicationNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.FlagIndicationNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly FlagIndicationNomenclature NewBG = new FlagIndicationNomenclature { ResourceKey = "NewBG", Code = "01" };
        public static readonly FlagIndicationNomenclature UsedBG = new FlagIndicationNomenclature { ResourceKey = "UsedBG", Code = "02" };
        public static readonly FlagIndicationNomenclature NewAnotherCountry = new FlagIndicationNomenclature { ResourceKey = "NewAnotherCountry", Code = "03" };
        public static readonly FlagIndicationNomenclature UsedAnotherCountry = new FlagIndicationNomenclature { ResourceKey = "UsedAnotherCountry", Code = "04" };
        public static readonly FlagIndicationNomenclature ImportedFromCountry = new FlagIndicationNomenclature { ResourceKey = "ImportedFromCountry", Code = "05" };
    }
}
