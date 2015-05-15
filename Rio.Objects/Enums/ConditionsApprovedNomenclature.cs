using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ConditionsApprovedNomenclature
    {
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
                    return App_LocalResources.ConditionsApprovedNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly ConditionsApprovedNomenclature ConditionsApproved = new ConditionsApprovedNomenclature { ResourceKey = "ConditionsApproved", Code = "01" };
        public static readonly ConditionsApprovedNomenclature ApplicationForApproval = new ConditionsApprovedNomenclature { ResourceKey = "ApplicationForApproval", Code = "02" };
        public static readonly ConditionsApprovedNomenclature ApplicationForApprovalTogether = new ConditionsApprovedNomenclature { ResourceKey = "ApplicationForApprovalTogether", Code = "03" };

        public static readonly IEnumerable<ConditionsApprovedNomenclature> Values =
            new List<ConditionsApprovedNomenclature>
            {
                ConditionsApproved,
                ApplicationForApproval,
                ApplicationForApprovalTogether
            };
    }
}
