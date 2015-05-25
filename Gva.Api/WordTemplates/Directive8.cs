using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;
using System.Linq;
using System.Collections.Generic;

namespace Gva.Api.WordTemplates
{
    public class Directive8 : IDataGenerator
    {
        private ILotRepository lotRepository;

        public Directive8(ILotRepository lotRepository)
        {
            this.lotRepository = lotRepository;
        }

        public string[] TemplateNames
        {
            get
            {
                return new string[] { "directive8"};
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
                    NUMBER = airworthinessData.DocumentNumber,
                    REG_MARK = registration != null ? registration.RegMark : null,
                    PRODUCER = aircraftData.AircraftProducer.Name,
                    PRODUCER_DESIGNATION = aircraftData.Model,
                    PRODUCER_ALT = aircraftData.AircraftProducer.NameAlt,
                    PRODUCER_DESIGNATION_ALT = aircraftData.ModelAlt,
                    CATEGORY = string.Join(", ", registration.CatAW.Select(c => c.Name).ToArray()),
                    CATEGORY_ALT = string.Join(", ", registration.CatAW.Select(c => c.NameAlt).ToArray()),
                    NUMBER2 = airworthinessData.DocumentNumber,
                    MAX_MASS = aircraftData.MaxMassT,
                    MSN = aircraftData.ManSN,
                    REVIEWS_PAGE1 = Utils.FillBlankData(new List<object>(), 2),
                    REVIEWS_PAGE2 = Utils.FillBlankData(new List<object>(), 15)
                }
            };

            return json;
        }
    }
}
