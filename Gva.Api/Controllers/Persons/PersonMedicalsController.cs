﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personDocumentMedicals")]
    [Authorize]
    public class PersonMedicalsController : GvaCaseTypePartController<PersonMedicalDO>
    {
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;

        public PersonMedicalsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personDocumentMedicals", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewMedical(int lotId, int? appId = null)
        {
            PersonMedicalDO newMedical = new PersonMedicalDO()
            {
                DocumentNumberPrefix = "MED BG",
                DocumentDateValidFrom = DateTime.Now
            };

            CaseDO caseDO = null;
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                caseDO = new CaseDO()
                {
                    IsAdded = true,
                    Applications = new List<ApplicationNomDO>()
                    {
                        this.applicationRepository.GetInitApplication(appId)
                    }
                };
            }

            return Ok(new CaseTypePartDO<PersonMedicalDO>(newMedical, caseDO));
        }
    }
}