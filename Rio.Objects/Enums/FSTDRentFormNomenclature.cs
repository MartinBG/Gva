using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class FSTDRentFormNomenclature
    {
        [ScriptIgnore]
        public string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ResourceKey))
                {
                    return string.Empty;
                }
                else
                {
                    return App_LocalResources.FSTDRentFormNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly FSTDRentFormNomenclature Dry = new FSTDRentFormNomenclature { ResourceKey = "Dry", Code = "01" };
        public static readonly FSTDRentFormNomenclature Wet = new FSTDRentFormNomenclature { ResourceKey = "Wet", Code = "02" };


        public static List<FSTDRentFormNomenclature> Values = new List<FSTDRentFormNomenclature>()
        {
            Dry,
            Wet
        };
    }
}
