using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class OperationRegionNomenclature
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
                    return App_LocalResources.OperationRegionNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly OperationRegionNomenclature FIR = new OperationRegionNomenclature { ResourceKey = "FIR", Code = "01" };
        public static readonly OperationRegionNomenclature Between10 = new OperationRegionNomenclature { ResourceKey = "Between10", Code = "02" };
        public static readonly OperationRegionNomenclature West = new OperationRegionNomenclature { ResourceKey = "West", Code = "03" };
        public static readonly OperationRegionNomenclature Between60S = new OperationRegionNomenclature { ResourceKey = "Between60S", Code = "04" };
        public static readonly OperationRegionNomenclature Between60N = new OperationRegionNomenclature { ResourceKey = "Between60N", Code = "05" };
        public static readonly OperationRegionNomenclature Worldwide = new OperationRegionNomenclature { ResourceKey = "Worldwide", Code = "06" };

        public static readonly OperationRegionNomenclature Other = new OperationRegionNomenclature { ResourceKey = "Other", Code = "07" };


        public static List<OperationRegionNomenclature> Values = new List<OperationRegionNomenclature>()
        {
            FIR,
            Between10,
            West,
            Between60S,
            Between60N,
            Worldwide
        };
    }
}
