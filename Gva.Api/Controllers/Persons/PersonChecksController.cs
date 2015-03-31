using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personDocumentChecks")]
    [Authorize]
    public class PersonChecksController : GvaCaseTypePartController<PersonCheckDO>
    {
        private INomRepository nomRepository;
        private IUnitOfWork unitOfWork;

        public PersonChecksController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentChecks", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.unitOfWork = unitOfWork;
        }

        [Route("new")]
        public IHttpActionResult GetNewCheck(int lotId)
        {
            PersonCheckDO newCheck = new PersonCheckDO()
            {
                DocumentDateValidFrom = DateTime.Now,
                Valid = this.nomRepository.GetNomValue("boolean", "yes"),
                Reports = new List<RelatedReportDO>()
            };

            return Ok(new CaseTypePartDO<PersonCheckDO>(newCheck));
        }

        [Route("{partIndex}/report")]
        public IHttpActionResult GetReport(int lotId, int partIndex)
        {
            var report = (from r in this.unitOfWork.DbContext.Set<GvaViewPersonReport>().Include(r => r.Person)
                      join rc in this.unitOfWork.DbContext.Set<GvaViewPersonReportCheck>() on
                        new { LotId = r.LotId, PartIndex = r.PartIndex }
                        equals
                        new { LotId = rc.ReportLotId, PartIndex = rc.ReportPartIndex }
                          where rc.CheckPartIndex == partIndex && rc.CheckLotId == lotId
                             select new {
                                PartIndex = r.PartIndex,
                                LotId = r.LotId,
                                Date = r.Date,
                                DocumentNumber = r.DocumentNumber,
                                Person = r.Person
                              })
                              .SingleOrDefault();

            RelatedReportDO result = null;
            if(report != null)
            {
                result = new RelatedReportDO()
                {
                    PartIndex = report.PartIndex,
                    LotId = report.LotId,
                    Date = report.Date,
                    DocumentNumber = report.DocumentNumber,
                    Publisher = string.Format("{0} {1}", report.Person.Lin, report.Person.Names)
                };
            }

            return Ok(new
            {
                result = result
            });
        }
    }
}