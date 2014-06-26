using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class ApplicantTypeNomenclature
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
                    return App_LocalResources.ApplicantTypeNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly ApplicantTypeNomenclature CommercialRegistered = new ApplicantTypeNomenclature { ResourceKey = "CommercialRegistered", Code = "01" };
        public static readonly ApplicantTypeNomenclature DealerRegistered = new ApplicantTypeNomenclature { ResourceKey = "DealerRegistered", Code = "02" };

        public static readonly ApplicantTypeNomenclature Physical = new ApplicantTypeNomenclature { ResourceKey = "Physical", Code = "03" };
        public static readonly ApplicantTypeNomenclature ForeignPhysical = new ApplicantTypeNomenclature { ResourceKey = "ForeignPhysical", Code = "04" };

        public static readonly ApplicantTypeNomenclature Entity = new ApplicantTypeNomenclature { ResourceKey = "Entity", Code = "05" };
        public static readonly ApplicantTypeNomenclature ForeignEntity = new ApplicantTypeNomenclature { ResourceKey = "ForeignEntity", Code = "06" };

    }
}
