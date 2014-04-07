using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.InventoryRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Gva.Api.Repositories.OrganizationRepository;

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
        private IFileRepository fileRepository;
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
            : base(lotRepository, fileRepository, unitOfWork, lotEventDispatcher)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.inventoryRepository = inventoryRepository;
            this.organizationRepository = organizationRepository;
            this.fileRepository = fileRepository;
            this.applicationRepository = applicationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("")]
        public IHttpActionResult GetOrganizations(string name = null, string CAO = null, string uin = null, DateTime? dateValidTo = null, DateTime? dateCAOValidTo = null, bool exact = false)
        {
            var organizations = this.organizationRepository.GetOrganizations(name, CAO, uin, dateValidTo, dateCAOValidTo, exact);
            return Ok(organizations.Select(o => new OrganizationDO(o)));
        }

        [Route("{lotId}")]
        public IHttpActionResult GetOrganization(int lotId)
        {
            var organization = this.organizationRepository.GetOrganization(lotId);
            OrganizationDO returnValue = new OrganizationDO(organization);

            return Ok(returnValue);
        }

        [Route("{lotId}/organizationInventory")]
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

                if (documentTypes.Length > 0)
                {
                    inventory = inventory.Where(item => documentTypes.Contains(item.SetPartAlias)).ToList();
                }
            }

            return Ok(inventory);
        }

        [Route("{lotId}/applications")]
        public IHttpActionResult GetApplications(int lotId, string term = null)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);

            var applications = this.applicationRepository.GetNomApplications(lotId).Select(a => new ApplicationNomDO(a));

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.ToLower();
                applications = applications.Where(n => n.ApplicationName.ToLower().Contains(term)).ToArray();
            }

            return Ok(applications);
        }

        [Route("")]
        public IHttpActionResult PostOrganization(JObject organization)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var newLot = this.lotRepository.GetSet("Organization").CreateLot(userContext);

                dynamic organizationData = organization.Value<JObject>("organizationData");
                newLot.CreatePart("organizationData", organizationData, userContext);
                this.caseTypeRepository.AddCaseTypes(newLot, organizationData.Value<JArray>("caseTypes"));

                newLot.Commit(userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new { id = newLot.LotId });
            }
        }

        [Route(@"{lotId}/{*path:regex(^organizationData$)}"),
         Route(@"{lotId}/{*path:regex(^organizationAddresses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationAuditplans/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertAirportOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertGroundServiceOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationGroundServiceOperatorsSnoOperational/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationApprovals/\d+/amendments/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegAirportOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegGroundServiceOperators/\d+$)}")]
        public override IHttpActionResult GetPart(int lotId, string path)
        {
            return base.GetPart(lotId, path);
        }

         [Route(@"{lotId}/{*path:regex(^organizationInspections/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRecommendations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationStaffManagement/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationStaffExaminers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationDocumentApplications/\d+$)}")]
        public override IHttpActionResult GetFilePart(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFilePart(lotId, path, caseTypeId);
        }

        [Route(@"{lotId}/{*path:regex(^organizationAddresses$)}"),
         Route(@"{lotId}/{*path:regex(^organizationAuditplans$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertAirportOperators$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertGroundServiceOperators$)}"),
         Route(@"{lotId}/{*path:regex(^organizationGroundServiceOperatorsSnoOperational$)}"),
         Route(@"{lotId}/{*path:regex(^organizationApprovals/\d+/amendments$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegAirportOperators$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegGroundServiceOperators$)}")]
        public override IHttpActionResult GetParts(int lotId, string path)
        {
            return base.GetParts(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^organizationInspections$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRecommendations$)}"),
         Route(@"{lotId}/{*path:regex(^organizationStaffManagement$)}"),
         Route(@"{lotId}/{*path:regex(^organizationStaffExaminers$)}"),
         Route(@"{lotId}/{*path:regex(^organizationDocumentOthers$)}"),
         Route(@"{lotId}/{*path:regex(^organizationDocumentApplications$)}")]
        public override IHttpActionResult GetFileParts(int lotId, string path, int? caseTypeId = null)
        {
            return base.GetFileParts(lotId, path, caseTypeId);
        }

        [Route(@"{lotId}/{*path:regex(^organizationApprovals/\d+$)}")]
        public IHttpActionResult GetApprovalPart(int lotId, string path, int? caseTypeId = null)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);

            var part = lot.GetPart(path);
            var firstAmendment = lot.GetParts(path + "/amendments").FirstOrDefault();

            return Ok(new ApprovalPartVersionDO(part, firstAmendment, null));
        }


        [Route(@"{lotId}/{path:regex(^organizationApprovals$)}")]
        public IHttpActionResult GetApprovalParts(int lotId, string path, int? caseTypeId = null)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var parts = lot.GetParts(path);

            var result = new List<ApprovalPartVersionDO>();
            foreach (var part in parts)
            {
                var partEditions = lot.GetParts(part.Part.Path + "/amendments");

                result.Add(new ApprovalPartVersionDO(
                        part,
                        partEditions.FirstOrDefault(),
                        partEditions.LastOrDefault()));
            }

            return Ok(result);
        }

        [Route(@"{lotId}/{*path:regex(^organizationAddresses$)}"),
         Route(@"{lotId}/{*path:regex(^organizationAuditplans$)}"),
         Route(@"{lotId}/{*path:regex(^organizationStaffManagement$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertAirportOperators$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertGroundServiceOperators$)}"),
         Route(@"{lotId}/{*path:regex(^organizationGroundServiceOperatorsSnoOperational$)}"),
         Route(@"{lotId}/{*path:regex(^organizationInspections$)}"),
         Route(@"{lotId}/{*path:regex(^organizationApprovals/\d+/amendments$)}"),
         Route(@"{lotId}/{*path:regex(^organizationStaffExaminers$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegAirportOperators$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegGroundServiceOperators$)}"),
         Route(@"{lotId}/{*path:regex(^organizationDocumentOthers$)}")]
        public IHttpActionResult PostNewPart(int lotId, string path, JObject content)
        {
            return base.PostNewPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^organizationRecommendations$)}")]
        public IHttpActionResult PostNewRecommendationPart(int lotId, string path, dynamic content)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);

            PartVersion partVersion = lot.CreatePart(path + "/*", content.part, userContext);
         
            this.fileRepository.AddFileReferences(partVersion, content.files);

            foreach (int auditPartIndex in content.part.includedAudits)
            {
                
                dynamic audit = base.GetFilePart(lotId, "organizationInspections/" + auditPartIndex, null);
                audit.Content.Part.recommendationReports = audit.Content.Part.recommendationReports as JArray;

                string recommendationPartName = content.part.recommendationPart != null ? content.part.recommendationPart.name : null;

                if (audit.Content.Part.recommendationReports == null)
                {
                    audit.Content.Part.recommendationReports = new JArray();
                }

                audit.Content.Part.recommendationReports.Add(new JObject(
                      new JProperty("partIndex", partVersion.Part.Index),
                      new JProperty("formDate", content.part.formDate),
                      new JProperty("formText", content.part.formText),
                      new JProperty("recommendationPartName", recommendationPartName)));

                PartVersion auditPartVersion = lot.UpdatePart("organizationInspections/" + auditPartIndex, audit.Content.Part, userContext);

                this.fileRepository.AddFileReferences(auditPartVersion, audit.Content.Files);

            }

            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

        [Route(@"{lotId}/{*path:regex(^organizationData$)}"),
         Route(@"{lotId}/{*path:regex(^organizationAddresses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationAuditplans/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationStaffManagement/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertAirportOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertGroundServiceOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationGroundServiceOperatorsSnoOperational/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationInspections/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationApprovals/\d+/amendments/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationStaffExaminers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegAirportOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegGroundServiceOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationDocumentOthers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationDocumentApplications/\d+$)}")]
        public IHttpActionResult PostPart(int lotId, string path, JObject content)
        {
            return base.PostPart(lotId, path, content);
        }

        [Route(@"{lotId}/{*path:regex(^organizationRecommendations/\d+$)}")]
        public IHttpActionResult PostRecommendationPart(int lotId, string path, dynamic content)
        {
            base.PostPart(lotId, path, content as JObject);
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);

            foreach (int auditPartIndex in content.part.includedAudits)
            {

                dynamic audit = base.GetFilePart(lotId, "organizationInspections/" + auditPartIndex, null);

                string recommendationPartName = content.part.recommendationPart != null ? content.part.recommendationPart.name : null;
                foreach(dynamic report in audit.Content.Part.recommendationReports)
                {
                    if (report.partIndex == content.partIndex) 
                    {
                        return Ok();
                    }
                }

                if (audit.Content.Part.recommendationReports == null)
                {
                    audit.Content.Part.recommendationReports = new JArray();
                }

                audit.Content.Part.recommendationReports.Add(new JObject(
                        new JProperty("partIndex", content.partIndex),
                        new JProperty("formDate", content.part.formDate),
                        new JProperty("formText", content.part.formText),
                        new JProperty("recommendationPartName", recommendationPartName)));

               
                PartVersion partVersion = lot.UpdatePart("organizationInspections/" + auditPartIndex, audit.Content.Part, userContext);

                this.fileRepository.AddFileReferences(partVersion, audit.Content.Files);

                lot.Commit(userContext, lotEventDispatcher);
            }

            this.unitOfWork.Save();

            return Ok();
        }

        [Route(@"{lotId}/{path:regex(^organizationApprovals$)}")]
        public IHttpActionResult PostNewApproval(int lotId, string path, dynamic content)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);

            PartVersion partVersion = lot.CreatePart(path + "/*", content.approval.part, userContext);

            lot.CreatePart(
                string.Format("{0}/{1}/amendments/*", path, partVersion.Part.Index),
                content.organizationAmendment.part,
                userContext);

            lot.Commit(userContext, lotEventDispatcher);
            this.unitOfWork.Save();

            return Ok();
        }

        [Route(@"{lotId}/{*path:regex(^organizationAddresses/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationAuditplans/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationStaffManagement/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertAirportOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationCertGroundServiceOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationGroundServiceOperatorsSnoOperational/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationInspections/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationStaffExaminers/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegAirportOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRegGroundServiceOperators/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationRecommendations/\d+$)}"),
         Route(@"{lotId}/{*path:regex(^organizationDocumentOthers/\d+$)}")]
        public override IHttpActionResult DeletePart(int lotId, string path)
        {
            return base.DeletePart(lotId, path);
        }

        [Route(@"{lotId}/{*path:regex(^organizationApprovals/\d+/amendments/\d+$)}")]
        public IHttpActionResult DeleteAmendment(int lotId, string path)
        {
            UserContext userContext = this.Request.GetUserContext();
            var lot = this.lotRepository.GetLotIndex(lotId);
            lot.DeletePart(path, userContext, true);

            if (lot.GetParts(Regex.Match(path, @".+/\d+/[^/\d]+").Value).Count() == 0)
            {
                lot.DeletePart(Regex.Match(path, @"[^/\d]+/\d+").Value, userContext);
            }

            lot.Commit(userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            return Ok();
        }

         [Route(@"{lotId}/{*path:regex(^organizationDocumentApplications$)}")]
        public IHttpActionResult PostNewApplication(int lotId, string path, dynamic content)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                UserContext userContext = this.Request.GetUserContext();
                var lot = this.lotRepository.GetLotIndex(lotId);

                PartVersion partVersion = lot.CreatePart(path + "/*", content.part, userContext);

                this.fileRepository.AddFileReferences(partVersion, content.files);

                lot.Commit(userContext, lotEventDispatcher);

                GvaApplication application = new GvaApplication()
                {
                    Lot = lot,
                    GvaAppLotPart = partVersion.Part
                };

                applicationRepository.AddGvaApplication(application);

                this.unitOfWork.Save();

                transaction.Commit();
            }

            return Ok();
        }

        [Route(@"{lotId}/{*path:regex(^organizationDocumentApplications/\d+$)}")]
        public IHttpActionResult DeleteApplication(int lotId, string path)
        {
            IHttpActionResult result;

            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var partVersion = this.lotRepository.GetLotIndex(lotId).GetPart(path);

                applicationRepository.DeleteGvaApplication(partVersion.Part.PartId);

                result = base.DeletePart(lotId, path);

                this.unitOfWork.Save();

                transaction.Commit();
            }

            return result;
        }

    }
}