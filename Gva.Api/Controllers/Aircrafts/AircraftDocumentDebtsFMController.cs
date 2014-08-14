﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Aircrafts
{
    [RoutePrefix("api/aircrafts/{lotId}/aircraftDocumentDebtsFM")]
    [Authorize]
    public class AircraftDocumentDebtsFMController : GvaFilePartController<AircraftDocumentDebtFMDO>
    {
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;

        public AircraftDocumentDebtsFMController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base("aircraftDocumentDebtsFM", unitOfWork, lotRepository, fileRepository, lotEventDispatcher)
        {
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentDebtFM (int lotId, int? appId = null)
        {
            var files = new List<FileDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                files.Add(new FileDO()
                {
                    IsAdded = true,
                    Applications = new List<ApplicationNomDO>()
                    {
                        this.applicationRepository.GetInitApplication(appId)
                    }
                });
            }

            return Ok(new FilePartVersionDO<AircraftDocumentDebtFMDO>(new AircraftDocumentDebtFMDO(), files));
        }
    }
}