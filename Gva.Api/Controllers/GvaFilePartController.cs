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
        private UserContext userContext;

        public GvaFilePartController(
            string path,
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
        {
            this.path = path;
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.userContext = userContext;
        }

        [Route("")]
        [Validate]
        public virtual IHttpActionResult PostNewPart(int lotId, FilePartVersionDO<T> partVersionDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                PartVersion<T> partVersion = lot.CreatePart<T>(this.path + "/*", partVersionDO.Part, this.userContext);

                this.fileRepository.AddFileReferences(partVersion.Part, partVersionDO.Files);

                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok(new FilePartVersionDO<T>(partVersion));
            }
        }

        [Route("{partIndex}")]
        [Validate]
        public virtual IHttpActionResult PostPart(int lotId, int partIndex, FilePartVersionDO<T> partVersionDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                PartVersion<T> partVersion = lot.UpdatePart(
                    string.Format("{0}/{1}", this.path, partIndex),
                    partVersionDO.Part,
                    this.userContext);

                this.fileRepository.AddFileReferences(partVersion.Part, partVersionDO.Files);

                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok();
            }
        }

        [Route("{partIndex}")]
        public virtual IHttpActionResult DeletePart(int lotId, int partIndex)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                var partVersion = lot.DeletePart<T>(string.Format("{0}/{1}", this.path, partIndex), this.userContext);

                this.fileRepository.DeleteFileReferences(partVersion.Part);
                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok();
            }
        }

        [Route("{partIndex}")]
        public virtual IHttpActionResult GetPart(int lotId, int partIndex, int? caseTypeId = null)
        {
            var partVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart<T>(string.Format("{0}/{1}", this.path, partIndex));
            var lotFiles = this.fileRepository.GetFileReferences(partVersion.PartId, caseTypeId);

            return Ok(new FilePartVersionDO<T>(partVersion, lotFiles));
        }

        [Route("")]
        public virtual IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var partVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts<T>(this.path);

            List<FilePartVersionDO<T>> partVersionDOs = new List<FilePartVersionDO<T>>();
            foreach (var partVersion in partVersions)
            {
                var lotFiles = this.fileRepository.GetFileReferences(partVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFiles.Length != 0)
                {
                    partVersionDOs.Add(new FilePartVersionDO<T>(partVersion, lotFiles));
                }
            }

            return Ok(partVersionDOs);
        }
    }
}