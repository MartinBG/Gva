using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    public abstract class GvaLotsController : ApiController
    {
        private ILotRepository lotRepository;
        private UserContext userContext;
        private IUnitOfWork unitOfWork;

        public GvaLotsController(ILotRepository lotRepository, IUserContextProvider userContextProvider, IUnitOfWork unitOfWork)
        {
            this.lotRepository = lotRepository;
            this.userContext = userContextProvider.GetCurrentUserContext();
            this.unitOfWork = unitOfWork;
        }

        public HttpResponseMessage GetPart(int lotId, string path)
        {
            var part = this.lotRepository.GetLotIndex(lotId).GetPart(path);

            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                Mapper.Map<PartVersion, PartVersionDO>(part));
        }

        //public HttpResponseMessage GetFilePart(int lotId, string path)
        //{

        //}

        public HttpResponseMessage GetParts(int lotId, string path)
        {
            var parts = this.lotRepository.GetLotIndex(lotId).GetParts(path);

            return ControllerContext.Request.CreateResponse(
                HttpStatusCode.OK,
                Mapper.Map<PartVersion[], PartVersionDO[]>(parts));
        }

        //public HttpResponseMessage GetFileParts(int lotId, string path)
        //{

        //}

        public HttpResponseMessage PostNewPart(int lotId, string path, dynamic content)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            lot.CreatePart(path + "/*", content.part, this.userContext);
            lot.Commit(this.userContext);

            if (content.file != null)
            {

            }

            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage PostPart(int lotId, string path, dynamic content)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            lot.UpdatePart(path, content.part, this.userContext);
            lot.Commit(this.userContext);

            if (content.file != null)
            {

            }

            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }

        public HttpResponseMessage DeletePart(int lotId, string path)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            lot.DeletePart(path, this.userContext);
            lot.Commit(this.userContext);

            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}