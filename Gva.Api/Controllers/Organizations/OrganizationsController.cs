using System;
using System.Linq;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.InventoryRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations")]
    [Authorize]
    public class OrganizationsController : ApiController
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
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.inventoryRepository = inventoryRepository;
            this.organizationRepository = organizationRepository;
            this.applicationRepository = applicationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("new")]
        public IHttpActionResult GetNewOrganization()
        {
            OrganizationDataDO organizationData = new OrganizationDataDO();
            organizationData.Icao = null;
            return Ok(organizationData);
        }

        [Route("")]
        public IHttpActionResult GetOrganizations(
            string name = null,
            int? caseTypeId = null,
            string cao = null,
            string uin = null,
            DateTime? dateValidTo = null,
            DateTime? dateCaoValidTo = null,
            bool exact = false)
        {
            var organizations = this.organizationRepository.GetOrganizations(name, caseTypeId, cao, uin, dateValidTo, dateCaoValidTo, exact);
            return Ok(organizations.Select(o => new OrganizationViewDO(o)));
        }

        [Route("{lotId}")]
        public IHttpActionResult GetOrganization(int lotId)
        {
            var organization = this.organizationRepository.GetOrganization(lotId);
            OrganizationViewDO returnValue = new OrganizationViewDO(organization);
            returnValue.CaseTypes = this.caseTypeRepository.GetCaseTypesForLot(lotId).Select(ct => ct.Alias).ToList();

            return Ok(returnValue);
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostOrganization(OrganizationDataDO organizationData)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.CreateLot("Organization");

                newLot.CreatePart("organizationData", JObject.FromObject(organizationData), userContext);
                this.caseTypeRepository.AddCaseTypes(newLot, organizationData.CaseTypes.Select(ct => ct.NomValueId));

                newLot.Commit(userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route(@"{lotId}/organizationData")]
        public IHttpActionResult GetOrganizationData(int lotId)
        {
            var organizationData = this.lotRepository.GetLotIndex(lotId).Index.GetPart("organizationData");

            return Ok(organizationData.Content.ToObject<OrganizationDataDO>());
        }

        [Route(@"{lotId}/organizationData")]
        [Validate]
        public IHttpActionResult PostOrganizationData(int lotId, OrganizationDataDO organizationData)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var lot = this.lotRepository.GetLotIndex(lotId);

                this.caseTypeRepository.AddCaseTypes(
                    lot,
                    organizationData.CaseTypes.Select(ct => ct.NomValueId));
                lot.UpdatePart("organizationData", JObject.FromObject(organizationData), userContext);
                this.unitOfWork.Save();

                lot.Commit(userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();
                transaction.Commit();
            }
            return Ok();
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
            if (gvaNomApp != null && gvaNomApp.GvaAppLotPart != null)
            {
                return Ok(new ApplicationNomDO(gvaNomApp));
            }

            return Ok();
        }
    }
}