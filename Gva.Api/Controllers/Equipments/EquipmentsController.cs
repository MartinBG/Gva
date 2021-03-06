﻿using System.Linq;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
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
        private UserContext userContext;

        public EquipmentsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IInventoryRepository inventoryRepository,
            IEquipmentRepository equipmentRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.inventoryRepository = inventoryRepository;
            this.equipmentRepository = equipmentRepository;
            this.applicationRepository = applicationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.userContext = userContext;
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
        [Validate]
        public IHttpActionResult PostEquipment(EquipmentDataDO equipmentData)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var newLot = this.lotRepository.CreateLot("Equipment");

                var partVersion = newLot.CreatePart("equipmentData", equipmentData, this.userContext);

                int equipmentCaseTypeId = this.caseTypeRepository.GetCaseTypesForSet("Equipment").Single().GvaCaseTypeId;
                this.caseTypeRepository.AddCaseTypes(newLot, new int[] { equipmentCaseTypeId });

                newLot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route(@"{lotId}/equipmentData")]
        public IHttpActionResult GetEquipmentData(int lotId)
        {
            var equipmentData = this.lotRepository.GetLotIndex(lotId).Index.GetPart<EquipmentDataDO>("equipmentData");

            return Ok(equipmentData.Content);
        }

        [Route(@"{lotId}/equipmentData")]
        [Validate]
        public IHttpActionResult PostEquipmentData(int lotId, EquipmentDataDO equipmentData)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                var partVersion = lot.UpdatePart("equipmentData", equipmentData, this.userContext);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok();
            }
        }

        [Route("{lotId}/inventory")]
        public IHttpActionResult GetInventory(int lotId, [FromUri] string[] documentTypes = null, int? caseTypeId = null)
        {
            var inventory = this.inventoryRepository.GetInventoryItems(lotId, caseTypeId);

            if (documentTypes.Length > 0)
            {
                inventory = inventory.Where(item => documentTypes.Contains(item.SetPartAlias)).ToList();
            }

            return Ok(inventory);
        }
    }
}