using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class ServiceCategoryNomenclature
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
                    return App_LocalResources.ServiceCategoryNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }


        public static readonly ServiceCategoryNomenclature GroundAdministration = new ServiceCategoryNomenclature { ResourceKey = "GroundAdministration", Code = "01" };
        public static readonly ServiceCategoryNomenclature PassengerService = new ServiceCategoryNomenclature { ResourceKey = "PassengerService", Code = "02" };
        public static readonly ServiceCategoryNomenclature BaggageHandling = new ServiceCategoryNomenclature { ResourceKey = "BaggageHandling", Code = "03" };
        public static readonly ServiceCategoryNomenclature CargoHandling = new ServiceCategoryNomenclature { ResourceKey = "CargoHandling", Code = "04" };
        public static readonly ServiceCategoryNomenclature RampHandling = new ServiceCategoryNomenclature { ResourceKey = "RampHandling", Code = "05" };
        public static readonly ServiceCategoryNomenclature AircraftService = new ServiceCategoryNomenclature { ResourceKey = "AircraftService", Code = "06" };
        public static readonly ServiceCategoryNomenclature FuelOilMaintenance = new ServiceCategoryNomenclature { ResourceKey = "FuelOilMaintenance", Code = "07" };
        public static readonly ServiceCategoryNomenclature AircraftMaintenance = new ServiceCategoryNomenclature { ResourceKey = "AircraftMaintenance", Code = "08" };
        public static readonly ServiceCategoryNomenclature FlightOperations = new ServiceCategoryNomenclature { ResourceKey = "FlightOperations", Code = "09" };
        public static readonly ServiceCategoryNomenclature SurfaceTransportation = new ServiceCategoryNomenclature { ResourceKey = "SurfaceTransportation", Code = "10" };
        public static readonly ServiceCategoryNomenclature OnboardBuffet = new ServiceCategoryNomenclature { ResourceKey = "OnboardBuffet", Code = "11" };



        public static List<ServiceCategoryNomenclature> Values = new List<ServiceCategoryNomenclature>()
        {
            GroundAdministration,
            PassengerService,
            BaggageHandling,
            CargoHandling,
            RampHandling,
            AircraftService,
            FuelOilMaintenance,
            AircraftMaintenance,
            FlightOperations,
            SurfaceTransportation,
            OnboardBuffet
        };
    }
}
