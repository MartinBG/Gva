using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;
using System.Linq;
using System.Collections.Generic;
using Common.Data;
using Gva.Api.Models.Views.Aircraft;

namespace Gva.Api.WordTemplates
{
    public class NoiseCert : IDataGenerator
    {
        private ILotRepository lotRepository;
        private IUnitOfWork unitOfWork;

        public NoiseCert(
            ILotRepository lotRepository,
            IUnitOfWork unitOfWork)
        {
            this.lotRepository = lotRepository;
            this.unitOfWork = unitOfWork;
        }

        public string GeneratorCode
        {
            get
            {
                return "noiseCert";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Удостоверение за шум";
            }
        }

        public object GetData(int lotId, string path)
        {
            Lot lot = this.lotRepository.GetLotIndex(lotId);
            AircraftDataDO aircraftData = lot.Index.GetPart<AircraftDataDO>("aircraftData").Content;
            AircraftCertNoiseDO noiseData = lot.Index.GetPart<AircraftCertNoiseDO>(path).Content;
            var registration = this.unitOfWork.DbContext.Set<GvaViewAircraftRegistration>()
                                .Where(r => r.LotId == lotId)
                                .OrderByDescending(r => r.ActNumber)
                                .FirstOrDefault();

            var json = new
            {
                root = new
                {
                    DOCUMENT_NUMBER = noiseData.IssueNumber, 
                    PRODUCER_ALT = aircraftData.AircraftProducer != null ? aircraftData.AircraftProducer.NameAlt: null,
                    PRODUCER_DESIGNATION_ALT = aircraftData.ModelAlt, 
                    REG_MARK = registration != null ? registration.RegMark : null,
                    MSN = aircraftData.ManSN, 
                    ENGINE = aircraftData.EngineAlt, 
                    PROPELLER = string.IsNullOrEmpty(aircraftData.PropellerAlt) && string.IsNullOrEmpty(aircraftData.Propeller) ? 
                        "Not applicable" : (aircraftData.PropellerAlt ?? aircraftData.Propeller), 
                    MAX_TAKE_OFF_MASS = aircraftData.MaxMassT, 
                    MAX_LANDING_MASS = aircraftData.MaxMassL, 
                    NOISE_STANDART = noiseData.Chapter,
                    LATERAL_NOISE_LEVEL = noiseData.Lateral.HasValue ? string.Format("{0} dB", noiseData.Lateral.ToString()) : "N/A",
                    APPROACH_NOISE_LEVEL = noiseData.Approach.HasValue ? string.Format("{0} dB", noiseData.Approach.ToString()) : "N/A",
                    OVERFLIGHT_NOISE_LEVEL = noiseData.Overflight.HasValue ? string.Format("{0} dB", noiseData.Overflight.ToString()) : "N/A",
                    FLYOVER_NOISE_LEVEL = noiseData.Flyover.HasValue ? string.Format("{0} dB", noiseData.Flyover.ToString()) : "N/A",
                    TAKE_OFF_NOISE_LEVEL = noiseData.Takeoff.HasValue ? string.Format("{0} dB", noiseData.Takeoff.ToString()) : "N/A", 
                    ADDITIONAL_MODIFICATIONS = string.IsNullOrEmpty(noiseData.AdditionalModificationsAlt) && string.IsNullOrEmpty(noiseData.AdditionalModifications) ? 
                        "None" : noiseData.AdditionalModifications,
                    ADDITIONAL_MODIFICATIONS_ALT = noiseData.AdditionalModificationsAlt,
                    NOTES = noiseData.Remarks
                }
            };

            return json;
        }
    }
}
