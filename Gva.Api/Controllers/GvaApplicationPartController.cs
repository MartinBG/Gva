using System.Collections.Generic;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.ApplicationRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    public abstract class GvaApplicationPartController<T> : ApiController
        where T : class, new()
    {
        private string path;
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public GvaApplicationPartController(
            string path,
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher)
        {
            this.path = path;
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("")]
        [Validate]
        public virtual IHttpActionResult PostNewPart(int lotId, ApplicationPartVersionDO<T> partVersionDO)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);

            PartVersion partVersion = lot.CreatePart(this.path + "/*", JObject.FromObject(partVersionDO.Part), userContext);

            this.applicationRepository.AddApplicationRefs(partVersion.Part, partVersionDO.Applications);

            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok(new ApplicationPartVersionDO<T>(partVersion));
        }

        [Route("{partIndex}")]
        [Validate]
        public virtual IHttpActionResult PostPart(int lotId, int partIndex, ApplicationPartVersionDO<T> partVersionDO)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            PartVersion partVersion = lot.UpdatePart(
                string.Format("{0}/{1}", this.path, partIndex),
                JObject.FromObject(partVersionDO.Part),
                userContext);

            this.applicationRepository.AddApplicationRefs(partVersion.Part, partVersionDO.Applications);

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

            this.applicationRepository.DeleteApplicationRefs(partVersion);
            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        [Route("{partIndex}")]
        public virtual IHttpActionResult GetPart(int lotId, int partIndex)
        {
            var partVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart(string.Format("{0}/{1}", this.path, partIndex));
            var lotObjects = this.applicationRepository.GetApplicationRefs(partVersion.PartId);

            return Ok(new ApplicationPartVersionDO<T>(partVersion, lotObjects));
        }

        [Route("")]
        public virtual IHttpActionResult GetParts(int lotId)
        {
            var partVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts(this.path);

            List<ApplicationPartVersionDO<T>> partVersionDOs = new List<ApplicationPartVersionDO<T>>();
            foreach (var partVersion in partVersions)
            {
                var gvaApplications = this.applicationRepository.GetApplicationRefs(partVersion.PartId);
                partVersionDOs.Add(new ApplicationPartVersionDO<T>(partVersion, gvaApplications));
            }

            return Ok(partVersionDOs);
        }
    }
}