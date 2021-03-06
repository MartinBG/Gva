﻿using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;
using System.Linq;

namespace Gva.Api.WordTemplates
{
    public class ExportCert : IDataGenerator
    {
        private ILotRepository lotRepository;

        public ExportCert(ILotRepository lotRepository)
        {
            this.lotRepository = lotRepository;
        }

        public string GeneratorCode
        {
            get
            {
                return "exportCert";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Експортно удостоверение";
            }
        }

        public object GetData(int lotId, string path)
        {
            Lot lot = this.lotRepository.GetLotIndex(lotId);
            AircraftDataDO aircraftData = lot.Index.GetPart<AircraftDataDO>("aircraftData").Content;
            AircraftCertRegistrationFMDO registration = lot.Index.GetPart<AircraftCertRegistrationFMDO>(path).Content;

            var json = new
            {
                root = new
                {
                    MSN = aircraftData.ManSN,
                    NUMBER = registration.Removal.DocumentNumber,
                    TEXT = registration.Removal.Export.Text,
                    TEXT_ALT = registration.Removal.Export.TextAlt,
                    ENGINE_MODEL = aircraftData.EngineAlt ?? aircraftData.Engine,
                    PRODUCER = aircraftData.AircraftProducer != null ? aircraftData.AircraftProducer.NameAlt : null,
                    AIRCRAFT = aircraftData.ModelAlt ?? aircraftData.Model,
                    PROPELLER_MODEL = aircraftData.PropellerAlt ?? aircraftData.Propeller,
                    tick1 = registration.Removal.Export.AircraftNewOld == true,
                    tick2 = registration.Removal.Export.AircraftNewOld == false
                }
            };

            return json;
        }
    }
}