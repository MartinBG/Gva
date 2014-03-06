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

        public IHttpActionResult GetPart(int lotId, string path)
        {
            var part = this.lotRepository.GetLotIndex(lotId).GetPart(path);

            return Ok(Mapper.Map<PartVersion, PartVersionDO>(part));
        }

        //public IHttpActionResult GetFilePart(int lotId, string path)
        //{

        //}

        public IHttpActionResult GetParts(int lotId, string path)
        {
            var parts = this.lotRepository.GetLotIndex(lotId).GetParts(path);

            return Ok(Mapper.Map<PartVersion[], PartVersionDO[]>(parts));
        }

        //public IHttpActionResult GetFileParts(int lotId, string path)
        //{

        //}

        public IHttpActionResult PostNewPart(int lotId, string path, dynamic content)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            lot.CreatePart(path + "/*", content.part, this.userContext);
            lot.Commit(this.userContext);

            if (content.file != null)
            {

            }

            this.unitOfWork.Save();

            return Ok();
        }

        public IHttpActionResult PostPart(int lotId, string path, dynamic content)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            lot.UpdatePart(path, content.part, this.userContext);
            lot.Commit(this.userContext);

            if (content.file != null)
            {

            }

            this.unitOfWork.Save();

            return Ok();
        }

        public IHttpActionResult DeletePart(int lotId, string path)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            lot.DeletePart(path, this.userContext);
            lot.Commit(this.userContext);

            this.unitOfWork.Save();

            return Ok();
        }
    }
}