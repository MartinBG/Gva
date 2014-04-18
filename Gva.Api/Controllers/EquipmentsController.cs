using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.EquipmentRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.InventoryRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/equipments")]
    [Authorize]
    public class EquipmentsController : GvaLotsController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IInventoryRepository inventoryRepository;
        private IEquipmentRepository equipmentRepository;
        private IFileRepository fileRepository;
        private IApplicationRepository applicationRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public EquipmentsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IInventoryRepository inventoryRepository,
            IEquipmentRepository equipmentRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base(applicationRepository, lotRepository, fileRepository, unitOfWork, lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.inventoryRepository = inventoryRepository;
            this.equipmentRepository = equipmentRepository;
            this.fileRepository = fileRepository;
            this.applicationRepository = applicationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("")]
        public IHttpActionResult GetEquipments(string name = null, bool exact = false)
        {
            var equipments = this.equipmentRepository.GetEquipments(name, exact);

            return Ok(equipments.Select(a => new EquipmentDO(a)));
        }

        [Route("{lotId}")]
        public IHttpActionResult GetEquipment(int lotId)
        {
            var equipment = this.equipmentRepository.GetEquipment(lotId);

            return Ok(new EquipmentDO(equipment));
        }

        [Route("")]
        public IHttpActionResult PostEquipment(JObject equipment)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.GetSet("Equipment").CreateLot(userContext);

                newLot.CreatePart("equipmentData", equipment.Get<JObject>("equipmentData"), userContext);
                this.caseTypeRepository.AddCaseTypes(newLot, equipment.GetItems<JObject>("equipmentData.caseTypes"));

                newLot.Commit(userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route("{lotId}/inventory")]
        public IHttpActionResult GetInventory(int lotId, [FromUri] string[] documentTypes = null, int? caseTypeId = null)
        {
            this.lotRepository.GetLotIndex(lotId);
            var inventoryItems = this.inventoryRepository.GetInventoryItemsForLot(lotId);

            List<InventoryItemDO> inventory;
            if (caseTypeId.HasValue)
            {
                var lotFiles = this.fileRepository.GetFileReferencesForLot(lotId, caseTypeId.Value);

                inventory = inventoryItems
                    .Join(
                        lotFiles,
                        i => i.PartId,
                        f => f.LotPartId,
                        (i, f) => new InventoryItemDO(i, f))
                    .ToList();
            }
            else
            {
                inventory = new List<InventoryItemDO>();
                foreach (var inventoryItem in inventoryItems)
                {
                    var lotFiles = this.fileRepository.GetFileReferences(inventoryItem.PartId, null);

                    if (lotFiles.Length == 0)
                    {
                        inventory.Add(new InventoryItemDO(inventoryItem, null));
                    }

                    foreach (var lotFile in lotFiles)
                    {
                        inventory.Add(new InventoryItemDO(inventoryItem, lotFile));
                    }
                }
            }

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

        [Route(@"{lotId}/{*path:regex(^equipmentDocumentApplications$)}")]
        public IHttpActionResult PostNewApplication(int lotId, string path, JObject content)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var lot = this.lotRepository.GetLotIndex(lotId);

                PartVersion partVersion = lot.CreatePart(path + "/*", content.Get<JObject>("part"), userContext);

                this.fileRepository.AddFileReferences(partVersion, content.GetItems<FileDO>("files"));

                lot.Commit(userContext, lotEventDispatcher);

                GvaApplication application = new GvaApplication()
                {
                    Lot = lot,
                    GvaAppLotPart = partVersion.Part
                };

                applicationRepository.AddGvaApplication(application);

                transaction.Commit();
            }

            this.unitOfWork.Save();

            return Ok();
        }

        [Route(@"{lotId}/{*path:regex(^equipmentDocumentApplications/\d+$)}")]
        public IHttpActionResult DeleteApplication(int lotId, string path)
        {
            IHttpActionResult result;

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var partVersion = this.lotRepository.GetLotIndex(lotId).GetPart(path);

                applicationRepository.DeleteGvaApplication(partVersion.Part.PartId);

                result = base.DeletePart(lotId, path);

                transaction.Commit();
            }

            this.unitOfWork.Save();

            return result;
        }

        [Route(@"{lotId}/{*path:regex(^equipmentData$)}"),
         Route(@"{lotId}/{*path:regex(^equipmentCertOperationals/\d+$)}")]
        public override IHttpActionResult GetPart(int lotId, string path)
        {
            return base.GetPart(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^equipmentDocumentOwners/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^equipmentDocumentApplications/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^equipmentDocumentOthers/\d+$)}")]
        public override IHttpActionResult GetFilePart(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFilePart(lotId, path, caseTypeId);
        }

        [Route(@"{lotId}/{*path:regex(^inspections/\d+$)}")]
        public override IHttpActionResult GetApplicationPart(int lotId, string path)
        {
            return base.GetApplicationPart(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^equipmentData$)}")]
        public IHttpActionResult PostEquipmentData(int lotId, string path, JObject content)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            //this.caseTypeRepository.AddCaseTypes(lot, (content as dynamic).part.caseTypes);

            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^equipmentCertOperationals$)}")]
        public override IHttpActionResult GetParts(int lotId, string path)
        {
            return base.GetParts(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^equipmentDocumentOwners$)}"),
         Route(@"{lotId}/{*path:regex(^equipmentDocumentApplications$)}"),
         Route(@"{lotId}/{*path:regex(^equipmentDocumentOthers$)}")]
        public override IHttpActionResult GetFileParts(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFileParts(lotId, path, caseTypeId);
        }

        [Route(@"{lotId}/{*path:regex(^inspections$)}")]
        public override IHttpActionResult GetApplicationParts(int lotId, string path)
        {
            return base.GetApplicationParts(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^equipmentDocumentOwners$)}"),
         Route(@"{lotId}/{*path:regex(^equipmentDocumentOthers$)}"),
         Route(@"{lotId}/{*path:regex(^inspections$)}"),
         Route(@"{lotId}/{*path:regex(^equipmentCertOperationals$)}")]
        public override IHttpActionResult PostNewPart(int lotId, string path, JObject content)
        {
            return base.PostNewPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^equipmentDocumentOwners/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^equipmentDocumentApplications/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^equipmentDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^inspections/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^equipmentCertOperationals/\d+$)}")]
        public override IHttpActionResult PostPart(int lotId, string path, JObject content)
        {
            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^equipmentDocumentOwners/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^equipmentDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^inspections/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^equipmentCertOperationals/\d+$)}")]
        public override IHttpActionResult DeletePart(int lotId, string path)
        {
            return base.DeletePart(lotId, path);
        }
    }
}