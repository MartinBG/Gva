using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class FlightPurposeNomenclature
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
                    return App_LocalResources.FlightPurposeNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        public static readonly FlightPurposeNomenclature Development = new FlightPurposeNomenclature { ResourceKey = "Development", Code = "01" };
        public static readonly FlightPurposeNomenclature CertificationSpecifications = new FlightPurposeNomenclature { ResourceKey = "CertificationSpecifications", Code = "02" };
        public static readonly FlightPurposeNomenclature CrewTraining = new FlightPurposeNomenclature { ResourceKey = "CrewTraining", Code = "03" };
        public static readonly FlightPurposeNomenclature FlightTesting = new FlightPurposeNomenclature { ResourceKey = "FlightTesting", Code = "04" };
        public static readonly FlightPurposeNomenclature FlightAircraftProduction = new FlightPurposeNomenclature { ResourceKey = "FlightAircraftProduction", Code = "05" };
        public static readonly FlightPurposeNomenclature FlightCustomerAcceptance = new FlightPurposeNomenclature { ResourceKey = "FlightCustomerAcceptance", Code = "06" };
        public static readonly FlightPurposeNomenclature DeliverExportAircraft = new FlightPurposeNomenclature { ResourceKey = "DeliverExportAircraft", Code = "07" };
        public static readonly FlightPurposeNomenclature FlightAuthorityAcceptance = new FlightPurposeNomenclature { ResourceKey = "FlightAuthorityAcceptance", Code = "08" };
        public static readonly FlightPurposeNomenclature MarketResearch = new FlightPurposeNomenclature { ResourceKey = "MarketResearch", Code = "09" };
        public static readonly FlightPurposeNomenclature ExhibitionDemonstration = new FlightPurposeNomenclature { ResourceKey = "ExhibitionDemonstration", Code = "10" };
        public static readonly FlightPurposeNomenclature FlightMaintenanceAirworthiness = new FlightPurposeNomenclature { ResourceKey = "FlightMaintenanceAirworthiness", Code = "11" };
        public static readonly FlightPurposeNomenclature FlightExceedingCertificatedWeight = new FlightPurposeNomenclature { ResourceKey = "FlightExceedingCertificatedWeight", Code = "12" };
        public static readonly FlightPurposeNomenclature RecordBreaking = new FlightPurposeNomenclature { ResourceKey = "RecordBreaking", Code = "13" };
        public static readonly FlightPurposeNomenclature EnvironmentalRequirements = new FlightPurposeNomenclature { ResourceKey = "EnvironmentalRequirements", Code = "14" };
        public static readonly FlightPurposeNomenclature FlightsNoncommercialPurpose = new FlightPurposeNomenclature { ResourceKey = "FlightsNoncommercialPurpose", Code = "15" };

        public static readonly List<FlightPurposeNomenclature> Values =
            new List<FlightPurposeNomenclature>
            {
                Development,
                CertificationSpecifications,
                CrewTraining,
                FlightTesting,
                FlightAircraftProduction,
                FlightCustomerAcceptance, 
                DeliverExportAircraft,
                FlightAuthorityAcceptance,
                MarketResearch,
                ExhibitionDemonstration,
                FlightMaintenanceAirworthiness,
                FlightExceedingCertificatedWeight,
                RecordBreaking,
                EnvironmentalRequirements,
                FlightsNoncommercialPurpose
            };
    }
}
