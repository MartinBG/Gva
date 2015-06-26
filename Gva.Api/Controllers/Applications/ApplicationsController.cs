using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Docs.Api.DataObjects;
using Docs.Api.Enums;
using Docs.Api.Models;
using Docs.Api.Repositories.DocRepository;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.ModelsDO.Airports;
using Gva.Api.ModelsDO.Applications;
using Gva.Api.ModelsDO.Common;
using Gva.Api.ModelsDO.Equipments;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.AirportRepository;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.EquipmentRepository;
using Gva.Api.Repositories.ExaminationSystemRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Gva.Api.Repositories.PersonRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Docs.Api.Models.ClassificationModels;
using Docs.Api.Models.UnitModels;

namespace Gva.Api.Controllers.Applications
{
    [RoutePrefix("api/apps")]
    [Authorize]
    public class ApplicationsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IPersonRepository personRepository;
        private IOrganizationRepository organizationRepository;
        private IAircraftRepository aircraftRepository;
        private IAirportRepository airportRepository;
        private IEquipmentRepository equipmentRepository;
        private IDocRepository docRepository;
        private IApplicationRepository applicationRepository;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;
        private IExaminationSystemRepository examinationSystemRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private UserContext userContext;

        public ApplicationsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IPersonRepository personRepository,
            IOrganizationRepository organizationRepository,
            IAircraftRepository aircraftRepository,
            IAirportRepository airportRepository,
            IEquipmentRepository equipmentRepository,
            IDocRepository docRepository,
            IApplicationRepository applicationRepository,
            INomRepository nomRepository,
            IFileRepository fileRepository,
            IExaminationSystemRepository examinationSystemRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.personRepository = personRepository;
            this.organizationRepository = organizationRepository;
            this.aircraftRepository = aircraftRepository;
            this.airportRepository = airportRepository;
            this.equipmentRepository = equipmentRepository;
            this.docRepository = docRepository;
            this.applicationRepository = applicationRepository;
            this.nomRepository = nomRepository;
            this.examinationSystemRepository = examinationSystemRepository;
            this.fileRepository = fileRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.userContext = userContext;
        }

        [Route("")]
        public IHttpActionResult GetApplications(
            string set = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? personLin = null,
            string aircraftIcao = null,
            string organizationUin = null,
            int? stageId = null,
            int? inspectorId = null,
            int? applicationTypeId = null,
            int offset = 0, 
            int limit = 10
            )
        {
            var applications = this.applicationRepository.GetApplications(
                lotSetAlias: set,
                fromDate: fromDate,
                toDate: toDate,
                personLin: personLin,
                aircraftIcao: aircraftIcao,
                organizationUin: organizationUin,
                stageId: stageId,
                inspectorId: inspectorId,
                applicationTypeId : applicationTypeId,
                limit: limit,
                offset: offset);

            return Ok(applications);
        }

        [Route("exams")]
        public IHttpActionResult GetApplicationExams(int offset = 0,  int? limit = null)
        {
            var applications = this.applicationRepository.GetPersonApplicationExams(offset, limit)
                .OrderByDescending(a => a.DocumentDate)
                .Take(1000);

            return Ok(applications);
        }

        [Route("{id:int}")]
        public IHttpActionResult GetApplication(int id)
        {
            var application = this.unitOfWork.DbContext.Set<GvaApplication>()
                .Include(a => a.Doc)
                .Include(a => a.GvaViewApplication)
                .Include(a => a.GvaViewApplication.ApplicationType)
                .Include(a => a.GvaAppLotPart)
                .SingleOrDefault(a => a.GvaApplicationId == id);

            if (application == null)
            {
                throw new Exception("Cannot find application with id " + id);
            }

            Set set = this.unitOfWork.DbContext.Set<Lot>()
                .Single(l => l.LotId == application.LotId)
                .Set;

            ApplicationDO returnValue = new ApplicationDO(application, set.Alias);

            if (set.Alias == "Person")
            {
                returnValue.Person = new PersonViewDO(this.personRepository.GetPerson(application.LotId));
            }
            else if (set.Alias == "Organization")
            {
                returnValue.Organization = new OrganizationViewDO(this.organizationRepository.GetOrganization(application.LotId));
            }
            else if (set.Alias == "Aircraft")
            {
                var aircraft = this.aircraftRepository.GetAircraft(application.LotId);
                returnValue.Aircraft = new AircraftViewDO(aircraft.Item1, aircraft.Item2);
            }
            else if (set.Alias == "Airport")
            {
                returnValue.Airport = new AirportViewDO(this.airportRepository.GetAirport(application.LotId));
            }
            else if (set.Alias == "Equipment")
            {
                returnValue.Equipment = new EquipmentViewDO(this.equipmentRepository.GetEquipment(application.LotId));
            }

            var appFilesAll = this.unitOfWork.DbContext.Set<GvaAppLotFile>()
                .Include(e => e.GvaLotFile.LotPart.SetPart)
                .Include(e => e.GvaLotFile.GvaCaseType)
                .Include(e => e.GvaLotFile.DocFile)
                .Include(e => e.GvaLotFile.GvaFile)
                .Include(e => e.DocFile)
                .Where(e => e.GvaApplicationId == id)
                .ToList();

            if (application.DocId.HasValue)
            {
                var docRelations = this.docRepository.GetCaseRelationsByDocId(
                    application.DocId.Value,
                    e => e.Doc.DocFiles,
                    e => e.Doc.DocDirection,
                    e => e.Doc.DocType,
                    e => e.Doc.DocStatus,
                    e => e.Doc.DocEntryType,
                    e => e.Doc.DocSourceType);

                List<GvaAppLotFile> appFilesInCase = new List<GvaAppLotFile>();

                foreach (var dr in docRelations)
                {
                    ApplicationDocRelationDO appDocRelation = new ApplicationDocRelationDO(dr);

                    if (dr.Doc.DocEntryType.Alias == "Document")
                    {
                        foreach (var docFile in dr.Doc.DocFiles)
                        {
                            GvaAppLotFile appFileInDoc = appFilesAll.FirstOrDefault(e => e.DocFileId == docFile.DocFileId);
                            if (appFileInDoc != null)
                            {
                                appFilesInCase.Add(appFileInDoc);
                            }

                            appDocRelation.ApplicationLotFiles.Add(new ApplicationLotFileDO(appFileInDoc, docFile));
                        }

                        returnValue.AppDocCase.Add(appDocRelation);
                    }
                }

                returnValue.AppFilesNotInCase = appFilesAll
                    .Except(appFilesInCase)
                    .ToList()
                    .Select(e => new ApplicationLotFileDO(e, null))
                    .ToList();
            }
            else
            {
                returnValue.AppFilesNotInCase = appFilesAll
                    .ToList()
                    .Select(e => new ApplicationLotFileDO(e, null))
                    .ToList();
            }

            return Ok(returnValue);
        }

        [Route("create")]
        public IHttpActionResult PostNewApplication(ApplicationNewDO applicationNewDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                ApplicationMainDO newAppMainData = this.applicationRepository.CreateNewApplication(applicationNewDO, this.userContext);

                transaction.Commit();

                return Ok(new
                {
                    LotId = newAppMainData.LotId,
                    GvaApplicationId = newAppMainData.GvaApplicationId,
                    PartIndex = newAppMainData.PartIndex
                });
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult UnlinkApplication(int id)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var application = this.unitOfWork.DbContext.Set<GvaApplication>()
                    .Include(a => a.Stages)
                    .Include(a => a.GvaAppLotFiles)
                    .Include(a => a.GvaAppLotPart)
                    .SingleOrDefault(a => a.GvaApplicationId == id);

                if (application.GvaAppLotPart != null)
                {
                    var lot = this.lotRepository.GetLotIndex(application.LotId);

                    var partVersion = lot.DeletePart<DocumentApplicationDO>(application.GvaAppLotPart.Path, this.userContext);

                    this.fileRepository.DeleteFileReferences(partVersion.Part);

                    lot.Commit(this.userContext, lotEventDispatcher);
                }

                this.unitOfWork.DbContext.Set<GvaAppLotFile>().RemoveRange(application.GvaAppLotFiles);

                this.unitOfWork.DbContext.Set<GvaApplicationStage>().RemoveRange(application.Stages);

                this.unitOfWork.DbContext.Set<GvaApplication>().Remove(application);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [Route(@"appPart/{lotId}/{partIndex}")]
        public IHttpActionResult GetApplicationPart(int lotId, int partIndex)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            string path = string.Format("{0}DocumentApplications/{1}", lot.Set.Alias.ToLower(), partIndex);

            return Ok(this.applicationRepository.GetApplicationPart(path, lotId));
        }

        [Route(@"appPart/{lotId}/{partIndex}/qualifications")]
        public IHttpActionResult GetApplicationQualifications(int lotId, int partIndex)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            string path = string.Format("{0}DocumentApplications/{1}", lot.Set.Alias.ToLower(), partIndex);

            return Ok(this.applicationRepository.GetApplicationQualifications(path, lotId));
        }

        [Route(@"appPart/{lotId}/{partIndex}")]
        public IHttpActionResult PostApplicationPart(int lotId, int partIndex, CaseTypePartDO<DocumentApplicationDO> application)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                string path = string.Format("{0}DocumentApplications/{1}", lot.Set.Alias.ToLower(), partIndex);

                PartVersion<DocumentApplicationDO> partVersion = lot.UpdatePart(path, application.Part, this.userContext);

                this.fileRepository.AddFileReference(partVersion.Part, application.Case);

                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok(new CaseTypePartDO<DocumentApplicationDO>(partVersion));
            }
        }

        [Route("link")]
        public IHttpActionResult PostLinkApplication(ApplicationLinkDO applicationLinkDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                GvaApplication application = new GvaApplication()
                {
                    LotId = applicationLinkDO.LotId,
                    DocId = applicationLinkDO.DocId
                };

                applicationRepository.AddGvaApplication(application);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok(new
                {
                    applicationId = application.GvaApplicationId,
                    docId = application.DocId,
                });
            }
        }

        [Route("notLinkedDocs")]
        public IHttpActionResult GetNotLinkedDocs(
            int limit = 10,
            int offset = 0,
            string set = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string regUri = null,
            string docName = null,
            int? docTypeId = null,
            int? docStatusId = null,
            string corrs = null,
            string units = null
            )
        {
            UnitUser unitUser = this.unitOfWork.DbContext.Set<UnitUser>().FirstOrDefault(e => e.UserId == this.userContext.UserId);
            ClassificationPermission readPermission = this.unitOfWork.DbContext.Set<ClassificationPermission>().SingleOrDefault(e => e.Alias == "Read");
            DocCasePartType docCasePartType = this.unitOfWork.DbContext.Set<DocCasePartType>().SingleOrDefault(e => e.Alias == "Control");
            List<DocStatus> docStatuses = this.unitOfWork.DbContext.Set<DocStatus>().Where(e => e.IsActive).ToList();

            int totalCount = 0;
            DocView docView = DocView.Normal;

            var gvaApplciations = applicationRepository.GetLinkedToDocsApplications().ToList();

            List<int> excludedDocIds = gvaApplciations.Where(e => e.DocId.HasValue).Select(e => e.DocId.Value).ToList();

            List<Doc> docs = this.docRepository.GetCurrentExclusiveCaseDocs(
                fromDate,
                toDate,
                regUri,
                docName,
                docTypeId,
                docStatusId,
                false,
                true,
                corrs,
                units,
                null,
                excludedDocIds,
                limit,
                offset,
                docCasePartType,
                docStatuses,
                readPermission,
                unitUser,
                out totalCount);

            List<DocListItemDO> returnValue = docs.Select(e => new DocListItemDO(e, unitUser)).ToList();

            foreach (var item in returnValue)
            {
                var docCorrespondents = this.unitOfWork.DbContext.Set<DocCorrespondent>()
                    .Include(e => e.Correspondent.CorrespondentType)
                    .Where(e => e.DocId == item.DocId)
                    .ToList();

                item.DocCorrespondents.AddRange(docCorrespondents.Select(e => new DocCorrespondentDO(e)).ToList());
            }

            StringBuilder sb = new StringBuilder();

            if (totalCount >= 10000)
            {
                sb.Append("Има повече от 10000 резултата, моля, въведете допълнителни филтри.");
            }

            return Ok(new
            {
                docView = docView.ToString(),
                documents = returnValue,
                documentCount = totalCount,
                msg = sb.ToString()
            });
        }

        [Route("appByDocId")]
        [HttpGet]
        public IHttpActionResult GetApplicationByDocId(int? docId = null)
        {
            if (docId == null)
            {
                return BadRequest();
            }

            var application = this.applicationRepository.GetGvaApplicationByDocId(docId.Value);

            if (application != null)
            {
                return Ok(new { id = application.GvaApplicationId });
            }

            return Ok();
        }

        [Route("getGvaCorrespodents")]
        [HttpGet]
        public IHttpActionResult GetGvaCorrespodents(int lotId)
        {
            var gvaCorrespodents = this.applicationRepository.GetGvaCorrespondentsByLotId(lotId);

            if (gvaCorrespodents.Any())
            {
                return Ok(new { corrs = gvaCorrespodents.Select(e => e.CorrespondentId).ToList() });
            }

            return Ok(new { corrs = new int[0] });
        }
    }
}