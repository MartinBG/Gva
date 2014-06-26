using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class AirServicesTypeNomenclature
    {
        public string ResourceKey { get; private set; }
        public string Code { get; private set; }

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
                    return App_LocalResources.AirServicesTypeNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        public static readonly AirServicesTypeNomenclature PassengersTransport = new AirServicesTypeNomenclature { ResourceKey = "PassengersTransport", Code = "01" };
        public static readonly AirServicesTypeNomenclature CargoMailTransport = new AirServicesTypeNomenclature { ResourceKey = "CargoMailTransport", Code = "02" };

        public static readonly List<AirServicesTypeNomenclature> Values =
            new List<AirServicesTypeNomenclature>
            {
                PassengersTransport,
                CargoMailTransport
            };
    }
}
