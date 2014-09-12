using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Equipments;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Equipments
{
    [RoutePrefix("api/equipments/{lotId}/equipmentCertOperationals")]
    [Authorize]
    public class EquipmentCertOperationalsController : GvaApplicationPartController<EquipmentCertOperationalDO>
    {
        public EquipmentCertOperationalsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("equipmentCertOperationals", unitOfWork, lotRepository, applicationRepository, lotEventDispatcher, userContext)
        {
        }

        [Route("new")]
        public IHttpActionResult GetNewCertOperational(int lotId)
        {
            EquipmentCertOperationalDO newCertOperational = new EquipmentCertOperationalDO();

            return Ok(new ApplicationPartVersionDO<EquipmentCertOperationalDO>(newCertOperational));
        }
    }
}