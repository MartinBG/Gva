using System.Collections.Generic;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.FileRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    public abstract class GvaFilePartController<T> : ApiController
        where T : class, new()
    {
        private string path;
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public GvaFilePartController(
            string path,
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher)
        {
            this.path = path;
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("")]
        [Validate]
        public virtual IHttpActionResult PostNewPart(int lotId, PartVersionDO2<T> partVersionDO)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);

            PartVersion partVersion = lot.CreatePart(this.path + "/*", JObject.FromObject(partVersionDO.Part), userContext);

            this.fileRepository.AddFileReferences(partVersion, partVersionDO.Files);

            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok(new PartVersionDO2<T>(partVersion));
        }

        [Route("{partIndex}")]
        [Validate]
        public virtual IHttpActionResult PostPart(int lotId, int partIndex, PartVersionDO2<T> partVersionDO)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            PartVersion partVersion = lot.UpdatePart(
                string.Format("{0}/{1}", this.path, partIndex),
                JObject.FromObject(partVersionDO.Part),
                userContext);

            this.fileRepository.AddFileReferences(partVersion, partVersionDO.Files);

            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        [Route("{partIndex}")]
        public virtual IHttpActionResult DeletePart(int lotId, int partIndex)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            var partVersion = lot.DeletePart(string.Format("{0}/{1}", this.path, partIndex), userContext);

            this.fileRepository.DeleteFileReferences(partVersion);
            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        [Route("{partIndex}")]
        public virtual IHttpActionResult GetPart(int lotId, int partIndex, int? caseTypeId = null)
        {
            var partVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart(string.Format("{0}/{1}", this.path, partIndex));
            var lotFiles = this.fileRepository.GetFileReferences(partVersion.PartId, caseTypeId);

            return Ok(new PartVersionDO2<T>(partVersion, lotFiles));
        }

        [Route("")]
        public virtual IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var partVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts(this.path);

            List<PartVersionDO2<T>> partVersionDOs = new List<PartVersionDO2<T>>();
            foreach (var partVersion in partVersions)
            {
                var lotFiles = this.fileRepository.GetFileReferences(partVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFiles.Length != 0)
                {
                    partVersionDOs.Add(new PartVersionDO2<T>(partVersion, lotFiles));
                }
            }

            return Ok(partVersionDOs);
        }
    }
}