using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class FlightSubaimNomenclature
    {
        public string ResourceKey { get; private set; }
        public string Code { get; private set; }
        public string ParentValue { get; private set; }

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
                    return App_LocalResources.FlightSubaimNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        public static readonly FlightSubaimNomenclature TestingAircraft = new FlightSubaimNomenclature { ResourceKey = "TestingAircraft", Code = "01", ParentValue=FlightPurposeNomenclature.Development.Code };
        public static readonly FlightSubaimNomenclature TestingConcepts = new FlightSubaimNomenclature { ResourceKey = "TestingConcepts", Code = "02", ParentValue=FlightPurposeNomenclature.Development.Code };
        public static readonly FlightSubaimNomenclature TestingTechniques = new FlightSubaimNomenclature { ResourceKey = "TestingTechniques", Code = "03", ParentValue=FlightPurposeNomenclature.Development.Code };

        public static readonly FlightSubaimNomenclature FlightTestCertificate = new FlightSubaimNomenclature { ResourceKey = "FlightTestCertificate", Code = "04", ParentValue=FlightPurposeNomenclature.CertificationSpecifications.Code };
        public static readonly FlightSubaimNomenclature FlightTestCertificateSupplemental = new FlightSubaimNomenclature { ResourceKey = "FlightTestCertificateSupplemental", Code = "05", ParentValue=FlightPurposeNomenclature.CertificationSpecifications.Code };
        public static readonly FlightSubaimNomenclature FlightTestCertificateChanges = new FlightSubaimNomenclature { ResourceKey = "FlightTestCertificateChanges", Code = "06", ParentValue=FlightPurposeNomenclature.CertificationSpecifications.Code };
        public static readonly FlightSubaimNomenclature FlightTestEuropeanStandards = new FlightSubaimNomenclature { ResourceKey = "FlightTestEuropeanStandards", Code = "07", ParentValue=FlightPurposeNomenclature.CertificationSpecifications.Code };

        public static readonly FlightSubaimNomenclature FlightApprovedDesign = new FlightSubaimNomenclature { ResourceKey = "FlightApprovedDesign", Code = "08", ParentValue=FlightPurposeNomenclature.CrewTraining.Code };
        public static readonly FlightSubaimNomenclature FlightAirworthiness = new FlightSubaimNomenclature { ResourceKey = "FlightAirworthiness", Code = "09", ParentValue=FlightPurposeNomenclature.CrewTraining.Code };

        public static readonly FlightSubaimNomenclature ComplianceVerify = new FlightSubaimNomenclature { ResourceKey = "ComplianceVerify", Code = "10", ParentValue=FlightPurposeNomenclature.FlightTesting.Code };

        public static readonly FlightSubaimNomenclature FlightGreenAircraft = new FlightSubaimNomenclature { ResourceKey = "FlightGreenAircraft", Code = "11", ParentValue=FlightPurposeNomenclature.FlightAircraftProduction.Code };

        public static readonly FlightSubaimNomenclature AircraftSelling = new FlightSubaimNomenclature { ResourceKey = "AircraftSelling", Code = "12", ParentValue=FlightPurposeNomenclature.FlightCustomerAcceptance.Code };

        public static readonly FlightSubaimNomenclature AircraftRegistration = new FlightSubaimNomenclature { ResourceKey = "AircraftRegistration", Code = "13", ParentValue=FlightPurposeNomenclature.DeliverExportAircraft.Code };

        public static readonly FlightSubaimNomenclature FlightTestCompetentAuthority = new FlightSubaimNomenclature { ResourceKey = "FlightTestCompetentAuthority", Code = "14", ParentValue=FlightPurposeNomenclature.FlightAuthorityAcceptance.Code };

        public static readonly FlightSubaimNomenclature FlightMarketResearch = new FlightSubaimNomenclature { ResourceKey = "FlightMarketResearch", Code = "15", ParentValue=FlightPurposeNomenclature.MarketResearch.Code };

        public static readonly FlightSubaimNomenclature FlightAirExhibition = new FlightSubaimNomenclature { ResourceKey = "FlightAirExhibition", Code = "16", ParentValue=FlightPurposeNomenclature.ExhibitionDemonstration.Code };

        public static readonly FlightSubaimNomenclature FlightMaintenanceNotPerformed = new FlightSubaimNomenclature { ResourceKey = "FlightMaintenanceNotPerformed", Code = "17", ParentValue=FlightPurposeNomenclature.FlightMaintenanceAirworthiness.Code };

        public static readonly FlightSubaimNomenclature SurveillanceFlights = new FlightSubaimNomenclature { ResourceKey = "SurveillanceFlights", Code = "18", ParentValue=FlightPurposeNomenclature.FlightExceedingCertificatedWeight.Code };

        public static readonly FlightSubaimNomenclature TrainingOrientationFlights = new FlightSubaimNomenclature { ResourceKey = "TrainingOrientationFlights", Code = "19", ParentValue=FlightPurposeNomenclature.RecordBreaking.Code };

        public static readonly FlightSubaimNomenclature FlightCompliance = new FlightSubaimNomenclature { ResourceKey = "FlightCompliance", Code = "20", ParentValue=FlightPurposeNomenclature.EnvironmentalRequirements.Code };

        public static readonly FlightSubaimNomenclature AircraftApplicableCertificationSpecifications = new FlightSubaimNomenclature { ResourceKey = "AircraftApplicableCertificationSpecifications", Code = "21", ParentValue = FlightPurposeNomenclature.FlightsNoncommercialPurpose.Code };

        public static readonly List<FlightSubaimNomenclature> Values =
            new List<FlightSubaimNomenclature>
            {
                TestingAircraft,
                TestingConcepts,
                TestingTechniques,

                FlightTestCertificate,
                FlightTestCertificateSupplemental,
                FlightTestCertificateChanges,
                FlightTestEuropeanStandards,

                FlightApprovedDesign,
                FlightAirworthiness,

                ComplianceVerify,

                FlightGreenAircraft,

                AircraftSelling,

                AircraftRegistration,

                FlightTestCompetentAuthority,

                FlightMarketResearch,

                FlightAirExhibition,

                FlightMaintenanceNotPerformed,

                SurveillanceFlights,

                TrainingOrientationFlights,

                FlightCompliance,

                AircraftApplicableCertificationSpecifications
            };
    }
}
