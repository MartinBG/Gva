using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;
using System.Linq;
using System.Collections.Generic;

namespace Gva.Api.WordTemplates
{
    public class TechCert : IDataGenerator
    {
        private ILotRepository lotRepository;

        public TechCert(ILotRepository lotRepository)
        {
            this.lotRepository = lotRepository;
        }

        public string[] TemplateNames
        {
            get
            {
                return new string[] { "vla", "vlaReissue" };
            }
        }

        public object GetData(int lotId, string path)
        {
            Lot lot = this.lotRepository.GetLotIndex(lotId);
            AircraftCertAirworthinessDO airworthinessData = lot.Index.GetPart<AircraftCertAirworthinessDO>(path).Content;
            var json = new
            {
                root = new
                {
                    REVIEWS = Utils.FillBlankData(new List<object>(), 12),
                    NUMBER = airworthinessData.DocumentNumber
                }
            };

            return json;
        }
    }
}
