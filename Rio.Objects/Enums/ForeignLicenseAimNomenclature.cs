using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ForeignLicenseAimNomenclature : BaseNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.ForeignLicenseAimNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly ForeignLicenseAimNomenclature CommercialAirTransport = new ForeignLicenseAimNomenclature { ResourceKey = "CommercialAirTransport", Code = "01" };
        public static readonly ForeignLicenseAimNomenclature NonCommercialWithRights = new ForeignLicenseAimNomenclature { ResourceKey = "NonCommercialWithRights", Code = "02" };
        public static readonly ForeignLicenseAimNomenclature NonCommercialWithoutRights = new ForeignLicenseAimNomenclature { ResourceKey = "NonCommercialWithoutRights", Code = "03" };
    }
}
