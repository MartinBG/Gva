using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    public abstract class GvaSimplePartController<T> : ApiController
        where T : class, new()
    {
        private string path;
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private UserContext userContext;

        public GvaSimplePartController(
            string path,
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
        {
            this.path = path;
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.userContext = userContext;
        }

        [Route("")]
        [Validate]
        public virtual IHttpActionResult PostNewPart(int lotId, SimplePartDO<T> partVersionDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                PartVersion<T> partVersion = lot.CreatePart<T>(this.path + "/*", partVersionDO.Part, this.userContext);

                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok(new SimplePartDO<T>(partVersion));
            }
        }

        [Route("{partIndex}")]
        [Validate]
        public virtual IHttpActionResult PostPart(int lotId, int partIndex, SimplePartDO<T> partVersionDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                PartVersion<T> partVersion = lot.UpdatePart(
                    string.Format("{0}/{1}", this.path, partIndex),
                    partVersionDO.Part,
                    this.userContext);

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

            return Ok(new SimplePartDO<T>(partVersion));
        }

        [Route("")]
        public virtual IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            List<SimplePartDO<T>> partVersionDOs = this.lotRepository
                .GetLotIndex(lotId).Index
                .GetParts<T>(this.path)
                .Select(p => new SimplePartDO<T>(p))
                .ToList();

            return Ok(partVersionDOs);
        }
    }
}