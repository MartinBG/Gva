﻿using System;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using System.Linq;
using System.Data.Entity;
using Regs.Api.Repositories.LotRepositories;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Gva.Api.Controllers
{
    public abstract class GvaLotsController : ApiController
    {
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private IUnitOfWork unitOfWork;
        private ILotEventDispatcher lotEventDispatcher;

        public GvaLotsController(
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IUnitOfWork unitOfWork,
            ILotEventDispatcher lotEventDispatcher)
        {
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.unitOfWork = unitOfWork;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        public virtual IHttpActionResult GetPart(int lotId, string path)
        {
            var part = this.lotRepository.GetLotIndex(lotId).GetPart(path);

            return Ok(new PartVersionDO(part));
        }

        public virtual IHttpActionResult GetRegPart(int lotId)
        {
            var part = this.lotRepository.GetLotIndex(lotId).GetParts("aircraftCertRegistrationsFM").OrderBy(e => e.CreateDate).Last();

            return Ok(new PartVersionDO(part));
        }

        public virtual IHttpActionResult GetFilePart(int lotId, string path, int? caseTypeId)
        {
            var partVersion = this.lotRepository.GetLotIndex(lotId).GetPart(path);
            var lotFiles = this.fileRepository.GetFileReferences(partVersion.PartId, caseTypeId);

            return Ok(new PartVersionDO(partVersion, lotFiles));
        }

        public virtual IHttpActionResult GetParts(int lotId, string path)
        {
            var parts = this.lotRepository.GetLotIndex(lotId).GetParts(path);

            return Ok(parts.Select(pv => new PartVersionDO(pv)));
        }

        public virtual IHttpActionResult GetFileParts(int lotId, string path, int? caseTypeId)
        {
            var partVersions = this.lotRepository.GetLotIndex(lotId).GetParts(path);

            List<PartVersionDO> partVersionDOs = new List<PartVersionDO>();
            foreach (var partVersion in partVersions)
            {
                var lotFiles = this.fileRepository.GetFileReferences(partVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFiles.Length != 0)
                {
                    partVersionDOs.Add(new PartVersionDO(partVersion, lotFiles));
                }
            }

            return Ok(partVersionDOs);
        }

        public virtual IHttpActionResult PostNewPart(int lotId, string path, JObject content)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);

            PartVersion partVersion = lot.CreatePart(path + "/*", content.Get<JObject>("part"), userContext);

            this.fileRepository.AddFileReferences(partVersion, content.GetItems<FileDO>("files"));

            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        public virtual IHttpActionResult PostPart(int lotId, string path, JObject content)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            PartVersion partVersion = lot.UpdatePart(path, content.Get<JObject>("part"), userContext);

            this.fileRepository.AddFileReferences(partVersion, content.GetItems<FileDO>("files"));

            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        public virtual IHttpActionResult DeletePart(int lotId, string path)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            var partVersion = lot.DeletePart(path, userContext);
            this.fileRepository.DeleteFileReferences(partVersion);
            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }
    }
}