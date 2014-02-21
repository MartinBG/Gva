using AutoMapper;
using Common.Api.UserContext;
using Common.Data;
using Gva.Web.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Gva.Web.Controllers
{
    public class LotController : ApiController
    {
        private ILotRepository lotRepository;
        private UserContext userContext;
        private IUnitOfWork unitOfWork;

        public LotController(ILotRepository lotRepository, IUserContextProvider userContextProvider, IUnitOfWork unitOfWork)
        {
            this.lotRepository = lotRepository;
            this.userContext = userContextProvider.GetCurrentUserContext();
            this.unitOfWork = unitOfWork;
        }

        public HttpResponseMessage GetParts(int lotId, string path)
        {
            var parts = this.lotRepository.GetLotIndex(lotId).GetParts(path);

            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                Mapper.Map<PartVersion[], PartVersionDO[]>(parts));
        }

        public HttpResponseMessage GetPart(int lotId, string path)
        {
            var part = this.lotRepository.GetLotIndex(lotId).GetPart(path);

            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                Mapper.Map<PartVersion, PartVersionDO>(part));
        }

        public HttpResponseMessage PostNewPart(int lotId, string path, JObject part)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            lot.CreatePart(path + "/*", part.Value<JObject>("part"), this.userContext);
            lot.Commit(this.userContext);

            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage PostPart(int lotId, string path, JObject part)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            lot.UpdatePart(path, part.Value<JObject>("part"), this.userContext);
            lot.Commit(this.userContext);

            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}