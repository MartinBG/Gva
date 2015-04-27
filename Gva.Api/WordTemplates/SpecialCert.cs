using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;
using System.Linq;

namespace Gva.Api.WordTemplates
{
    public class SpecialCert : IDataGenerator
    {
        private ILotRepository lotRepository;

        public SpecialCert(ILotRepository lotRepository)
        {
            this.lotRepository = lotRepository;
        }

        public string[] TemplateNames
        {
            get
            {
                return new string[] { "special" };
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
                    PRODUCER = aircraftData.AircraftProducer.Name,
                    PRODUCER_DESIGNATION = aircraftData.Model,
                    PRODUCER_ALT = aircraftData.AircraftProducer.NameAlt,
                    PRODUCER_DESIGNATION_ALT = aircraftData.ModelAlt,
                    CATEGORY = string.Join(", ", registration.CatAW.Select(c => c.Name).ToArray()),
                    CATEGORY_ALT = string.Join(", ", registration.CatAW.Select(c => c.NameAlt).ToArray()),
                    NUMBER = airworthinessData.DocumentNumber,
                    MAX_MASS = aircraftData.MaxMassT,
                    MSN = aircraftData.ManSN,
                    ISSUE_DATE = airworthinessData.IssueDate,
                    ISSUE_DATE2 = airworthinessData.IssueDate
                }
            };

            return json;
        }
    }
}
