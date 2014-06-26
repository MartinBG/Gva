using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class OperationAreaNomenclature
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
                    return App_LocalResources.OperationAreaNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly OperationAreaNomenclature BG = new OperationAreaNomenclature { ResourceKey = "BG", Code = "01" };
        public static readonly OperationAreaNomenclature EU = new OperationAreaNomenclature { ResourceKey = "EU", Code = "02" };
        public static readonly OperationAreaNomenclature ME = new OperationAreaNomenclature { ResourceKey = "ME", Code = "03" };
        public static readonly OperationAreaNomenclature AF = new OperationAreaNomenclature { ResourceKey = "AF", Code = "04" };
        public static readonly OperationAreaNomenclature AS = new OperationAreaNomenclature { ResourceKey = "AS", Code = "05" };
        public static readonly OperationAreaNomenclature NA = new OperationAreaNomenclature { ResourceKey = "NA", Code = "06" };
        public static readonly OperationAreaNomenclature SA = new OperationAreaNomenclature { ResourceKey = "SA", Code = "07" };
        public static readonly OperationAreaNomenclature AU = new OperationAreaNomenclature { ResourceKey = "AU", Code = "08" };

        public static readonly OperationAreaNomenclature Other = new OperationAreaNomenclature { ResourceKey = "Other", Code = "09" };


        public static List<OperationAreaNomenclature> Values = new List<OperationAreaNomenclature>()
        {
            BG,
            EU,
            ME,
            AF,
            AS,
            NA,
            SA,
            AU
        };
    }
}
