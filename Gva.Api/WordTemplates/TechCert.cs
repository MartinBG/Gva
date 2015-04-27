using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;
using System.Linq;

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
                return new string[] { "vla" };
            }
        }

        public object GetData(int lotId, string path)
        {
            Lot lot = this.lotRepository.GetLotIndex(lotId);
            AircraftCertAirworthinessFMDO airworthinessData = lot.Index.GetPart<AircraftCertAirworthinessFMDO>(path).Content;
            var reviews = airworthinessData.Reviews.Select(r => new
            {
                DATE_FROM = r.IssueDate,
                DATE_TO = r.ValidToDate
            }).ToList<object>();

            var json = new
            {
                root = new
                {
                    REVIEWS = Utils.FillBlankData(reviews, 12),
                    NUMBER = airworthinessData.DocumentNumber
                }
            };

            return json;
        }
    }
}
