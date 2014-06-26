using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class OptionNameNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.OptionNameNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly OptionNameNomenclature FullyEquippedParagraph1 = new OptionNameNomenclature { ResourceKey = "FullyEquippedParagraph1", Code = "01" };
        public static readonly OptionNameNomenclature FullyEquippedParagraph2 = new OptionNameNomenclature { ResourceKey = "FullyEquippedParagraph2", Code = "02" };
        public static readonly OptionNameNomenclature FullyEquippedParagraph3 = new OptionNameNomenclature { ResourceKey = "FullyEquippedParagraph3", Code = "03" };
        public static readonly OptionNameNomenclature ParagraphOperationsManual = new OptionNameNomenclature { ResourceKey = "ParagraphOperationsManual", Code = "04" };
        public static readonly OptionNameNomenclature ParagraphBulletinsModifications = new OptionNameNomenclature { ResourceKey = "ParagraphBulletinsModifications", Code = "05" };

        public static readonly OptionNameNomenclature MainActivity = new OptionNameNomenclature { ResourceKey = "MainActivity", Code = "06" };
        public static readonly OptionNameNomenclature MainActivityLicense = new OptionNameNomenclature { ResourceKey = "MainActivityLicense", Code = "07" };
    }
}
