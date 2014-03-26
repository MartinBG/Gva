using System;
using System.Web.Http;
using AutoMapper;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using System.Linq;
using System.Data.Entity;
using Regs.Api.Repositories.LotRepositories;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Gva.Api.Mappers.Resolvers;

namespace Gva.Api.Controllers
{
    public abstract class GvaLotsController : ApiController
    {
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private IUnitOfWork unitOfWork;
        private ILotEventDispatcher lotEventDispatcher;
        private FileResolver fileResolver;

        public GvaLotsController(
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IUnitOfWork unitOfWork,
            ILotEventDispatcher lotEventDispatcher,
            FileResolver fileResolver)
        {
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.unitOfWork = unitOfWork;
            this.lotEventDispatcher = lotEventDispatcher;
            this.fileResolver = fileResolver;
        }

        public IHttpActionResult GetPart(int lotId, string path)
        {
            var part = this.lotRepository.GetLotIndex(lotId).GetPart(path);

            return Ok(Mapper.Map<PartVersion, PartVersionDO>(part));
        }

        public IHttpActionResult GetFilePart(int lotId, string path, int? caseTypeId)
        {
            var partVersion = this.lotRepository.GetLotIndex(lotId).GetPart(path);

            return Ok(Mapper.Map<FilePartVersionDO>(Tuple.Create(partVersion, caseTypeId, fileResolver)));
        }

        public IHttpActionResult GetParts(int lotId, string path)
        {
            var parts = this.lotRepository.GetLotIndex(lotId).GetParts(path);

            return Ok(Mapper.Map<PartVersion[], PartVersionDO[]>(parts));
        }

        public IHttpActionResult GetFileParts(int lotId, string path, int? caseTypeId)
        {
            var partVersions = this.lotRepository.GetLotIndex(lotId).GetParts(path);
            if (caseTypeId.HasValue)
            {
                partVersions = partVersions
                    .Join(
                        this.fileRepository.GetFileReferencesForLot(lotId, caseTypeId.Value),
                        (pv) => pv.PartId,
                        (f) => f.LotPartId,
                        (pv, f) => pv)
                    .Distinct()
                    .ToArray();
            }

            return Ok(Mapper.Map<IEnumerable<FilePartVersionDO>>(partVersions.Select(pv => Tuple.Create(pv, caseTypeId, fileResolver))));
        }

        public IHttpActionResult PostNewPart(int lotId, string path, dynamic content)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);

            PartVersion partVersion = lot.CreatePart(path + "/*", content.part, userContext);

            this.fileRepository.AddFileReferences(partVersion, content.files);

            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        public IHttpActionResult PostPart(int lotId, string path, dynamic content)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            PartVersion partVersion = lot.UpdatePart(path, content.part, userContext);

            this.fileRepository.AddFileReferences(partVersion, content.files);

            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        public IHttpActionResult DeletePart(int lotId, string path)
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