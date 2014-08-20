using System;
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
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.InventoryRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/organizations")]
    [Authorize]
    public class OrganizationsController : GvaLotsController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IInventoryRepository inventoryRepository;
        private IOrganizationRepository organizationRepository;
        private IApplicationRepository applicationRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotEventDispatcher lotEventDispatcher;

        public OrganizationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IInventoryRepository inventoryRepository,
            IOrganizationRepository organizationRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher)
            : base(applicationRepository, lotRepository, fileRepository, unitOfWork, lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.inventoryRepository = inventoryRepository;
            this.organizationRepository = organizationRepository;
            this.applicationRepository = applicationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("")]
        public IHttpActionResult GetOrganizations(
            string name = null,
            int? caseTypeId = null,
            string CAO = null,
            string uin = null,
            DateTime? dateValidTo = null,
            DateTime? dateCAOValidTo = null,
            bool exact = false)
        {
            var organizations = this.organizationRepository.GetOrganizations(name, caseTypeId, CAO, uin, dateValidTo, dateCAOValidTo, exact);
            return Ok(organizations.Select(o => new OrganizationDO(o)));
        }

        [Route("{lotId}")]
        public IHttpActionResult GetOrganization(int lotId)
        {
            var organization = this.organizationRepository.GetOrganization(lotId);
            OrganizationDO returnValue = new OrganizationDO(organization);
            returnValue.CaseTypes = this.caseTypeRepository.GetCaseTypesForLot(lotId).Select(ct => ct.Alias).ToList();

            return Ok(returnValue);
        }

        [Route("{lotId}/organizationInventory")]
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

        [Route("")]
        public IHttpActionResult PostOrganization(JObject organization)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.CreateLot("Organization", userContext);

                newLot.CreatePart("organizationData", organization.Get<JObject>("organizationData"), userContext);
                this.caseTypeRepository.AddCaseTypes(newLot, organization.GetItems<JObject>("organizationData.caseTypes").Select(ct => ct.Get<int>("nomValueId")));

                newLot.Commit(userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route(@"{lotId}/{*path:regex(^organizationData$)}"),
         Route(@"{lotId}/{*path:regex(^organizationGroundServiceOperatorsSnoOperational/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegAirportOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegGroundServiceOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertAirNavigationServiceDeliverers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertAirCarriers/\d+$)}")]
        public override IHttpActionResult GetPart(int lotId, string path)
        {
            return base.GetPart(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^organizationApprovals/\d+/amendments/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRecommendations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationApprovals/\d+$)}")]
        public override IHttpActionResult GetApplicationPart(int lotId, string path)
        {
            return base.GetApplicationPart(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^organizationGroundServiceOperatorsSnoOperational$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegAirportOperators$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegGroundServiceOperators$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertAirNavigationServiceDeliverers$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertAirCarriers$)}")]
        public override IHttpActionResult GetParts(int lotId, string path)
        {
            return base.GetParts(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^organizationApprovals/\d+/amendments$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRecommendations$)}"),
         Route(@"{lotId}/{*path:regex(^organizationApprovals$)}")]
        public override IHttpActionResult GetApplicationParts(int lotId, string path)
        {
            return base.GetApplicationParts(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^organizationGroundServiceOperatorsSnoOperational$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertAirNavigationServiceDeliverers$)}"),
         Route(@"{lotId}/{*path:regex(^organizationApprovals$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegAirportOperators$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegGroundServiceOperators$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertAirCarriers$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRecommendations$)}")]
        public override IHttpActionResult PostNewPart(int lotId, string path, JObject content)
        {
            return base.PostNewPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^organizationGroundServiceOperatorsSnoOperational/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertAirNavigationServiceDeliverers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationApprovals/\d+/amendments/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegAirportOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegGroundServiceOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertAirCarriers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationApprovals/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRecommendations/\d+$)}")]
        public override IHttpActionResult PostPart(int lotId, string path, JObject content)
        {
            return base.PostPart(lotId, path, content);
        }


        [Route(@"{lotId}/{*path:regex(^organizationData$)}")]
        public IHttpActionResult PostOrganizationData(int lotId, string path, JObject content)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            this.caseTypeRepository.AddCaseTypes(lot, content.GetItems<JObject>("part.caseTypes").Select(ct => ct.Get<int>("nomValueId")));

            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^organizationGroundServiceOperatorsSnoOperational/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertAirNavigationServiceDeliverers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegAirportOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegGroundServiceOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertAirCarriers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationApprovals/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRecommendations/\d+$)}")]
        public override IHttpActionResult DeletePart(int lotId, string path)
        {
            return base.DeletePart(lotId, path);
        }
    }
}