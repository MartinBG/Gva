using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;
using System.Linq;

namespace Gva.Api.WordTemplates
{
    public class Form15a : IDataGenerator
    {
        private ILotRepository lotRepository;

        public Form15a(ILotRepository lotRepository)
        {
            this.lotRepository = lotRepository;
        }

        public string[] TemplateNames
        {
            get
            {
                return new string[] { "15a", "15aReissue" };
            }
        }

        public object GetData(int lotId, string path)
        {
            Lot lot = this.lotRepository.GetLotIndex(lotId);
            AircraftDataDO aircraftData = lot.Index.GetPart<AircraftDataDO>("aircraftData").Content;
            AircraftCertAirworthinessDO airworthinessData = lot.Index.GetPart<AircraftCertAirworthinessDO>(path).Content;
            string regPath = string.Format("aircraftCertRegistrationsFM/{0}", airworthinessData.Registration.NomValueId);
            AircraftCertRegistrationFMDO registration = lot.Index.GetPart<AircraftCertRegistrationFMDO>(regPath).Content;
            var json = new
            {
                root = new
                {
                    REG_MARK = registration != null ? registration.RegMark : null,
                    PRODUCER_ALT = aircraftData.AircraftProducer != null ? aircraftData.AircraftProducer.NameAlt: null,
                    PRODUCER_DESIGNATION_ALT = aircraftData.ModelAlt,
                    PRODUCER = aircraftData.AircraftProducer != null ? aircraftData.AircraftProducer.Name : null,
                    PRODUCER_DESIGNATION = aircraftData.Model,
                    AIR_CATEGORY = aircraftData.AirCategory != null ? aircraftData.AirCategory.Name : null,
                    REF_NUMBER = airworthinessData.DocumentNumber,
                    MSN = aircraftData.ManSN,
                }
            };

            return json;
        }
    }
}
