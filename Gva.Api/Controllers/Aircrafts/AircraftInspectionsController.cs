﻿using System.Linq;
using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using System.Collections.Generic;
using Common.Api.UserContext;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Models;
using Common.Api.Models;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/inspections")]
    [Authorize]
    public class AircraftInspectionsController : GvaCaseTypePartController<AircraftInspectionDO>
    {
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private ICaseTypeRepository caseTypeRepository;
        private IFileRepository fileRepository;

        public AircraftInspectionsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("inspections", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.fileRepository = fileRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewInspection(int lotId, int? appId = null)
        {
            GvaCaseType caseType = this.caseTypeRepository.GetCaseTypesForSet("aircraft").Single();

            CaseDO caseDO = new CaseDO() 
            {
                CaseType = new NomValue()
                {
                    NomValueId = caseType.GvaCaseTypeId,
                    Name = caseType.Name,
                    Alias = caseType.Alias
                },
                BookPageNumber = this.fileRepository.GetNextBPN(lotId, caseType.GvaCaseTypeId).ToString()
            };

            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                caseDO.IsAdded = true;
                caseDO.Applications = new List<ApplicationNomDO>()
                {
                    this.applicationRepository.GetInitApplication(appId)
                };
            }

            return Ok(new CaseTypePartDO<AircraftInspectionDO>(new AircraftInspectionDO(), caseDO));
        }
    }
}