using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personStatuses")]
    [Authorize]
    public class PersonStatusesController : GvaCaseTypesPartController<PersonStatusDO>
    {
        private ICaseTypeRepository caseTypeRepository;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private INomRepository nomRepository;
        private string path;

        public PersonStatusesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personStatuses", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.caseTypeRepository = caseTypeRepository;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.nomRepository = nomRepository;
            this.path = "personStatuses";
        }

        [Route("new")]
        public IHttpActionResult GetNewStatus(int lotId)
        {
            var cases = this.caseTypeRepository.GetCaseTypesForSet("person")
                .Select(ct => new CaseDO()
                    {
                        CaseType = new NomValue()
                        {
                            NomValueId = ct.GvaCaseTypeId,
                            Name = ct.Name,
                            Alias = ct.Alias
                        },
                        IsAdded = true
                    })
                .ToList();

            PersonStatusDO newStatus = new PersonStatusDO()
            {
                DocumentDateValidFrom = DateTime.Now
            };

            return Ok(new CaseTypesPartDO<PersonStatusDO>(newStatus, cases));
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var statuses = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonStatusDO>(this.path);

            List<PersonStatusViewDO> statusesViewDOs = new List<PersonStatusViewDO>();
            foreach (var statusePartVersion in statuses)
            {
                var lotFiles = this.fileRepository.GetFileReferences(statusePartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFiles.Length != 0)
                {
                    statusesViewDOs.Add(new PersonStatusViewDO()
                    {
                        Cases = lotFiles.Select(lf => new CaseDO(lf)).ToList(),
                        PartIndex = statusePartVersion.Part.Index,
                        PersonStatusType = statusePartVersion.Content.PersonStatusTypeId.HasValue ? this.nomRepository.GetNomValue("personStatusTypes", statusePartVersion.Content.PersonStatusTypeId.Value) : null,
                        DocumentDateValidFrom = statusePartVersion.Content.DocumentDateValidFrom,
                        DocumentDateValidTo= statusePartVersion.Content.DocumentDateValidTo,
                        DocumentNumber = statusePartVersion.Content.DocumentNumber,
                        Notes = statusePartVersion.Content.Notes
                    });
                }
            }
            return Ok(statusesViewDOs);
        }
    }
}