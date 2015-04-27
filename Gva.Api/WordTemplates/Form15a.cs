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
            var firstReview = airworthinessData.Reviews.FirstOrDefault();
            var secondReview = airworthinessData.Reviews.Count() > 1 ? airworthinessData.Reviews.Skip(1).First() : null;
            var firstReviewOrganization = firstReview != null ? firstReview.Organization : null;
            var secondReviewOrganization = secondReview != null ? secondReview.Organization : null;
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
                    MSN = aircraftData.ManSN,
                    VALID_FROM = airworthinessData.IssueDate,
                    VALID_TO = airworthinessData.ValidToDate,
                    FIRST_REVIEW_VALID_FROM = firstReview != null ? firstReview.IssueDate : null,
                    FIRST_REVIEW_VALID_TO = firstReview != null ? firstReview.ValidToDate : null,
                    FIRST_REVIEW_ORGANIZATION = firstReviewOrganization != null ? string.Format("{0} / {1}", firstReviewOrganization.Name, firstReviewOrganization.NameAlt) : null,
                    SECOND_REVIEW_VALID_FROM = secondReview != null ? secondReview.IssueDate : null,
                    SECOND_REVIEW_VALID_TO = secondReview != null ? secondReview.ValidToDate : null,
                    SECOND_REVIEW_ORGANIZATION = secondReviewOrganization != null ? string.Format("{0} / {1}", secondReviewOrganization.Name, secondReviewOrganization.NameAlt) : null,
                }
            };

            return json;
        }
    }
}
