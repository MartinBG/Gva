using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class AircraftKindNomenclature
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
                    return App_LocalResources.AircraftKindNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly AircraftKindNomenclature BigPlane = new AircraftKindNomenclature { ResourceKey = "BigPlane", Code = "bigPlane" };
        public static readonly AircraftKindNomenclature SmallPlaneGliders = new AircraftKindNomenclature { ResourceKey = "SmallPlaneGliders", Code = "smallPlaneGliders" };
        public static readonly AircraftKindNomenclature AircraftRotorThrust = new AircraftKindNomenclature { ResourceKey = "AircraftRotorThrust", Code = "aircraftRotorThrust" };
        public static readonly AircraftKindNomenclature BalloonAirship = new AircraftKindNomenclature { ResourceKey = "BalloonAirship", Code = "balloonAirship" };


        public static List<AircraftKindNomenclature> Values = new List<AircraftKindNomenclature>()
        {
            BigPlane,
            SmallPlaneGliders,
            AircraftRotorThrust,
            BalloonAirship
        };
    }
}
