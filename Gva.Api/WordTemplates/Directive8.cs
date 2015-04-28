﻿using Regs.Api.Repositories.LotRepositories;
using Regs.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;
using System.Linq;

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
                return new string[] { "directive8" };
            }
        }

        public object GetData(int lotId, string path)
        {
            Lot lot = this.lotRepository.GetLotIndex(lotId);
            AircraftDataDO aircraftData = lot.Index.GetPart<AircraftDataDO>("aircraftData").Content;
            AircraftCertAirworthinessFMDO airworthinessData = lot.Index.GetPart<AircraftCertAirworthinessFMDO>(path).Content;
            string regPath = string.Format("aircraftCertRegistrationsFM/{0}", airworthinessData.Registration.NomValueId);
            AircraftCertRegistrationFMDO registration = lot.Index.GetPart<AircraftCertRegistrationFMDO>(regPath).Content;
            var reviews = airworthinessData.Reviews.Select(r => new
            {
                DATE_FROM = r.IssueDate,
                DATE_TO = r.ValidToDate
            }).ToList<object>();

            var reviewsResult = Utils.FillBlankData(reviews, 17);

            var json = new
            {
                root = new
                {
                    NUMBER = airworthinessData.DocumentNumber,
                    REG_MARK = registration != null ? registration.RegMark : null,
                    ISSUE_DATE = airworthinessData.IssueDate,
                    PRODUCER = aircraftData.AircraftProducer.Name,
                    PRODUCER_DESIGNATION = aircraftData.Model,
                    PRODUCER_ALT = aircraftData.AircraftProducer.NameAlt,
                    PRODUCER_DESIGNATION_ALT = aircraftData.ModelAlt,
                    CATEGORY = string.Join(", ", registration.CatAW.Select(c => c.Name).ToArray()),
                    CATEGORY_ALT = string.Join(", ", registration.CatAW.Select(c => c.NameAlt).ToArray()),
                    NUMBER2 = airworthinessData.DocumentNumber,
                    MAX_MASS = aircraftData.MaxMassT,
                    MSN = aircraftData.ManSN,
                    REVIEWS_PAGE1 = reviews.Take(2),
                    REVIEWS_PAGE2 = reviews.Skip(2).Take(15)
                }
            };

            return json;
        }
    }
}