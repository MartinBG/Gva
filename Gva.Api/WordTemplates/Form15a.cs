using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;

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
                return new string[] { "15a" };
            }
        }

        public object GetData(int lotId, string path)
        {
            Lot lot = this.lotRepository.GetLotIndex(lotId);
            AircraftDataDO aircraftData = lot.Index.GetPart<AircraftDataDO>("aircraftData").Content;
            AircraftCertAirworthinessFMDO airworthinessData = lot.Index.GetPart<AircraftCertAirworthinessFMDO>(path).Content;
            string regPath = string.Format("aircraftCertRegistrationsFM/{0}", airworthinessData.Registration.NomValueId);
            AircraftCertRegistrationFMDO registration = lot.Index.GetPart<AircraftCertRegistrationFMDO>(regPath).Content;
            
            var json = new
            {
                root = new
                {
                    REG_MARK = registration != null ? registration.RegMark : null,
                    PRODUCER_ALT = aircraftData.AircraftProducer.NameAlt,
                    PRODUCER_DESIGNATION_ALT = aircraftData.ModelAlt,
                    PRODUCER = aircraftData.AircraftProducer.Name,
                    PRODUCER_DESIGNATION = aircraftData.Model,
                    AIR_CATEGORY = aircraftData.AirCategory.Name,
                    REF_NUMBER = airworthinessData.DocumentNumber,
                    MSN = aircraftData.ManSN
                }
            };

            return json;
        }
    }
}
