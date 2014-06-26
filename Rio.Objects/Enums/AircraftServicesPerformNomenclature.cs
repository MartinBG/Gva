using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class AircraftServicesPerformNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.AircraftServicesPerformNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly AircraftServicesPerformNomenclature TwentyOrMoreSeats = new AircraftServicesPerformNomenclature { ResourceKey = "TwentyOrMoreSeats", Code = "01" };
        public static readonly AircraftServicesPerformNomenclature NineteenOrLessSeats = new AircraftServicesPerformNomenclature { ResourceKey = "NineteenOrLessSeats", Code = "02" };

    }
}
