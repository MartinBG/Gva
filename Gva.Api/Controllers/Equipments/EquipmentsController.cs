using System.Linq;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Equipments;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.EquipmentRepository;
using Gva.Api.Repositories.InventoryRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Equipments
{
    [RoutePrefix("api/equipments")]
    [Authorize]
    public class EquipmentsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IInventoryRepository inventoryRepository;
        private IEquipmentRepository equipmentRepository;
        private IApplicationRepository applicationRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public EquipmentsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IInventoryRepository inventoryRepository,
            IEquipmentRepository equipmentRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.inventoryRepository = inventoryRepository;
            this.equipmentRepository = equipmentRepository;
            this.applicationRepository = applicationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("new")]
        public IHttpActionResult GetNewEquipment()
        {
            EquipmentDataDO equipmentData = new EquipmentDataDO();

            return Ok(equipmentData);
        }

        [Route("")]
        public IHttpActionResult GetEquipments(string name = null, bool exact = false)
        {
            var equipments = this.equipmentRepository.GetEquipments(name, exact);

            return Ok(equipments.Select(a => new EquipmentViewDO(a)));
        }

        [Route("{lotId}")]
        public IHttpActionResult GetEquipment(int lotId)
        {
            var equipment = this.equipmentRepository.GetEquipment(lotId);

            return Ok(new EquipmentViewDO(equipment));
        }

        [Route("")]
        public IHttpActionResult PostEquipment(EquipmentDataDO equipmentData)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.CreateLot("Equipment", userContext);

                newLot.CreatePart("equipmentData", JObject.FromObject(equipmentData), userContext);
                int equipmentCaseTypeId = this.caseTypeRepository.GetCaseTypesForSet("Equipment").Single().GvaCaseTypeId;
                this.caseTypeRepository.AddCaseTypes(newLot, new int[] { equipmentCaseTypeId });

                newLot.Commit(userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route(@"{lotId}/equipmentData")]
        public IHttpActionResult GetEquipmentData(int lotId)
        {
            var equipmentData = this.lotRepository.GetLotIndex(lotId).Index.GetPart("equipmentData");

            return Ok(equipmentData.Content.ToObject<EquipmentDataDO>());
        }

        [Route(@"{lotId}/equipmentData")]
        public IHttpActionResult PostEquipmentData(int lotId, EquipmentDataDO equipmentData)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            lot.UpdatePart("equipmentData", JObject.FromObject(equipmentData), userContext);

            lot.Commit(userContext, this.lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        [Route("{lotId}/inventory")]
        public IHttpActionResult GetInventory(int lotId, [FromUri] string[] documentTypes = null, int? caseTypeId = null)
        {
            var inventory = this.inventoryRepository.GetInventoryItemsForLot(lotId, caseTypeId);

            if (documentTypes.Length > 0)
            {
                inventory = inventory.Where(item => documentTypes.Contains(item.SetPartAlias)).ToList();
            }

            return Ok(inventory);
        }

        [Route("{lotId}/applications/{appId}")]
        public IHttpActionResult GetApplication(int lotId, int appId)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);

            GvaApplication gvaNomApp = this.applicationRepository.GetNomApplication(appId);
            if (gvaNomApp != null)
            {
                return Ok(new ApplicationNomDO(gvaNomApp));
            }

            return Ok();
        }
    }
}