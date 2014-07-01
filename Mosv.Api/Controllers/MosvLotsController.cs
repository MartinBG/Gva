using System;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Regs.Api.LotEvents;
using Mosv.Api.ModelsDO;
using Regs.Api.Models;
using System.Linq;
using System.Data.Entity;
using Regs.Api.Repositories.LotRepositories;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Mosv.Api.Models;

namespace Mosv.Api.Controllers
{
    public abstract class MosvLotsController : ApiController
    {
        private ILotRepository lotRepository;
        private IUnitOfWork unitOfWork;
        private ILotEventDispatcher lotEventDispatcher;

        public MosvLotsController(
            ILotRepository lotRepository,
            IUnitOfWork unitOfWork,
            ILotEventDispatcher lotEventDispatcher)
        {
            this.lotRepository = lotRepository;
            this.unitOfWork = unitOfWork;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        public virtual IHttpActionResult GetPart(int lotId, string path)
        {
            var part = this.lotRepository.GetLotIndex(lotId).Index.GetPart(path);

            return Ok(new PartVersionDO(part));
        }

        public virtual IHttpActionResult GetParts(int lotId, string path)
        {
            var parts = this.lotRepository.GetLotIndex(lotId).Index.GetParts(path);

            return Ok(parts.Select(pv => new PartVersionDO(pv)));
        }

        public virtual IHttpActionResult PostNewPart(int lotId, string path, JObject content)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);

            PartVersion partVersion = lot.CreatePart(path + "/*", content.Get<JObject>("part"), userContext);

            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok(new { partIndex = partVersion.Part.Index });
        }

        public virtual IHttpActionResult PostPart(int lotId, string path, JObject content)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            PartVersion partVersion = lot.UpdatePart(path, content.Get<JObject>("part"), userContext);

            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        public virtual IHttpActionResult DeletePart(int lotId, string path)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            var partVersion = lot.DeletePart(path, userContext);
            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }
    }
}