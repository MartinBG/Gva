using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class FinancialArrangementsNomenclature
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
                    return App_LocalResources.FinancialArrangementsNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly FinancialArrangementsNomenclature WeightsAssumed = new FinancialArrangementsNomenclature { ResourceKey = "WeightsAssumed", Code = "01" };
        public static readonly FinancialArrangementsNomenclature LoansTaken = new FinancialArrangementsNomenclature { ResourceKey = "LoansTaken", Code = "02" };
        public static readonly FinancialArrangementsNomenclature LeasingAircraft = new FinancialArrangementsNomenclature { ResourceKey = "LeasingAircraft", Code = "03" };
        public static readonly FinancialArrangementsNomenclature OperationalContracts = new FinancialArrangementsNomenclature { ResourceKey = "OperationalContracts", Code = "04" };
        public static readonly FinancialArrangementsNomenclature Other = new FinancialArrangementsNomenclature { ResourceKey = "Other", Code = "05" };

    }
}
