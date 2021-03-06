﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Common.Json;
using Common.Linq;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.AirportRepository;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.EquipmentRepository;
using Gva.Api.Repositories.ExaminationSystemRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Gva.Api.Repositories.PersonRepository;
using Gva.Api.Repositories.StageRepository;
using Gva.Api.WordTemplates;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/nomenclatures")]
    [Authorize]
    public class GvaNomController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private IPersonRepository personRepository;
        private IAircraftRepository aircraftRepository;
        private IAirportRepository airportRepository;
        private IEquipmentRepository equipmentRepository;
        private IOrganizationRepository organizationRepository;
        private ICaseTypeRepository caseTypeRepository;
        private INomRepository nomRepository;
        private IStageRepository stageRepository;
        private IExaminationSystemRepository examinationSystemRepository;
        private IAircraftRegistrationRepository aircraftRegistrationRepository;
        private IEnumerable<IDataGenerator> dataGenerators;

        public GvaNomController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            IPersonRepository personRepository,
            IAircraftRepository aircraftRepository,
            IAirportRepository airportRepository,
            IEquipmentRepository equipmentRepository,
            IOrganizationRepository organizationRepository,
            ICaseTypeRepository caseTypeRepository,
            INomRepository nomRepository,
            IStageRepository stageRepository,
            IExaminationSystemRepository examinationSystemRepository,
            IAircraftRegistrationRepository aircraftRegistrationRepository,
            IEnumerable<IDataGenerator> dataGenerators)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.personRepository = personRepository;
            this.aircraftRepository = aircraftRepository;
            this.airportRepository = airportRepository;
            this.equipmentRepository = equipmentRepository;
            this.organizationRepository = organizationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.nomRepository = nomRepository;
            this.stageRepository = stageRepository;
            this.examinationSystemRepository = examinationSystemRepository;
            this.aircraftRegistrationRepository = aircraftRegistrationRepository;
            this.dataGenerators = dataGenerators;
        }

        [Route("papers")]
        public IHttpActionResult GetPapers(string term = null)
        {
            var papers = this.unitOfWork.DbContext.Set<GvaPaper>()
                .Where(p => p.IsActive)
                .ToList()
                .Select(p => new NomValue()
                {
                    Name = string.Format("{0} {1}-{2}",
                            p.Name,
                            p.FromDate.ToString("dd.MM.yyyy"),
                            p.ToDate.ToString("dd.MM.yyyy")),
                    NomValueId = p.PaperId
                });

            if (!string.IsNullOrWhiteSpace(term))
            {
                papers = papers
                    .Where(d => d.Name.ToLower().Contains(term.ToLower()))
                    .ToList();
            }

            return Ok(papers);
        }

        [Route("papers/{id:int}")]
        public IHttpActionResult GetPaper(int id)
        {
            var paper = this.unitOfWork.DbContext.Set<GvaPaper>()
                .Where(p => p.PaperId == id)
                .Single();

            string name = string.Format("{0} {1}-{2}",
                paper.Name,
                paper.FromDate.ToString("dd.MM.yyyy"),
                paper.ToDate.ToString("dd.MM.yyyy"));

            return Ok(new
            {
                nomValueId = paper.PaperId,
                name = name,
            });
        }

        [Route("dataGenerators")]
        public IHttpActionResult GetDataDenerators(string term = null)
        {
            var dataGenerators = this.dataGenerators.Select(d => new
                {
                    Code = d.GeneratorCode,
                    Name = d.GeneratorName,
                    NomValueId = d.GeneratorCode
                })
                .ToList();

            if (!string.IsNullOrWhiteSpace(term))
            {
                dataGenerators = dataGenerators
                    .Where(d => d.Name.ToLower().Contains(term.ToLower()))
                    .ToList();
            }

            return Ok(dataGenerators);
        }

        [Route("dataGenerators")]
        public IHttpActionResult GetDataDenerator(string templateName)
        {
            GvaWordTemplate template = this.unitOfWork.DbContext.Set<GvaWordTemplate>()
                    .Where(t => t.Name == templateName)
                    .Single();

            return Ok(this.dataGenerators.Where(dg => dg.GeneratorCode == template.DataGeneratorCode)
                .Select(d => new
                {
                    Code = d.GeneratorCode,
                    Name = d.GeneratorName,
                    NomValueId = d.GeneratorCode
                }).Single());
        }

        [Route("{lotId}/applications")]
        public IHttpActionResult GetApplications(int lotId, string term = null)
        {
            var applications = this.applicationRepository.GetNomApplications(lotId);

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.ToLower();

                return Ok(applications.Where(a => a.ApplicationName.Contains(term)).ToArray());
            }
            else
            {
                return Ok(applications.ToArray());
            }
        }

        [Route("{lotId}/applications/{appId}")]
        public IHttpActionResult GetApplication(int lotId, int appId)
        {
            var application = this.applicationRepository.GetNomApplication(appId);

            return Ok(application);
        }

        [Route("{set:regex(^(?:person|organization)$)}SetParts")]
        public IHttpActionResult GetLotSetParts(string set = null, string term = null)
        {
            var partTypes = this.unitOfWork.DbContext.Set<SetPart>()
                .Where(s => s.Set.Alias.ToLower() == set)
                .Select(i => new
                {
                    nomValueId = i.SetPartId,
                    name = i.Name,
                    alias = i.Alias
                });

            if (!string.IsNullOrEmpty(term))
            {
                return Ok(partTypes.Where(a => a.name.ToLower().Contains(term.ToLower())));
            }

            return Ok(partTypes);
        }

        [Route("appExamSystExams")]
        public IHttpActionResult GetAppExamSystExams(string term = null, string qualificationCode = null, string certCampCode = null)
        {
            var exams = this.examinationSystemRepository.GetExams(qualificationCode, certCampCode)
                .Select(c => new
                {
                    nomValueId = c.Code,
                    code = c.Code,
                    name = c.Name
                });

            if (!string.IsNullOrEmpty(term))
            {
                return Ok(exams.Where(a => a.name.ToLower().Contains(term.ToLower())));
            }

            return Ok(exams);
        }

        [Route("appExSystCertCampaigns")]
        public IHttpActionResult GetCertCampaigns(string term = null)
        {
            var campaigns = this.examinationSystemRepository.GetCertCampaigns()
                .Select(c => new
                { 
                    nomValueId = c.Code,
                    code = c.Code,
                    name = c.Name
                });
                
            if (!string.IsNullOrEmpty(term))
            {
                return Ok(campaigns.Where(a => a.name.ToLower().Contains(term.ToLower())));
            }

            return Ok(campaigns);
        }
        

        [Route("templates")]
        public IHttpActionResult GetTemplates(string term = null)
        {
            var templates = this.unitOfWork.DbContext.Set<GvaWordTemplate>()
                .Select(c => new
                {
                    nomValueId = c.GvaWordTemplateId,
                    code = c.Name,
                    name = c.Description
                });

            if (!string.IsNullOrEmpty(term))
            {
                return Ok(templates.Where(r => r.name.ToLower().Contains(term.ToLower())));
            }

            return Ok(templates);
        }

        [Route("appExSystQualifications")]
        public IHttpActionResult GetQualifications(string term = null)
        {
            var qualifications = this.examinationSystemRepository.GetQualifications()
                .Select(c => new
                {
                    nomValueId = c.Code,
                    code = c.Code,
                    name = c.Name
                });

            if (!string.IsNullOrEmpty(term))
            {
                return Ok(qualifications.Where(a => a.name.ToLower().Contains(term.ToLower())));
            }

            return Ok(qualifications);
        }

        [Route("organizationsAudits")]
        public IHttpActionResult GetOrganizationsAudits(int lotId, string term = null)
        {
            var audits = this.lotRepository.GetLotIndex(lotId).Index.GetParts<OrganizationInspectionDO>("organizationInspections")
                .Select(i => new
                {
                    nomValueId = i.Part.Index,
                    name = string.Format(
                        "№ {0} / {1} - {2}",
                        i.Content.DocumentNumber,
                        i.Content.AuditPart == null ? string.Empty : i.Content.AuditPart.Name,
                        i.Content.AuditReason.Name)
                });

            if (!string.IsNullOrEmpty(term))
            {
                return Ok(audits.Where(a => a.name.ToLower().Contains(term.ToLower())));
            }

            return Ok(audits);
        }

        [Route("aircraftsRegistrations")]
        public IHttpActionResult GetAircraftsRegistrations(int lotId, string term = null)
        {
            return Ok(this.aircraftRegistrationRepository.GetAircraftRegistrationNoms(lotId, term));
        }

        [Route("persons/{id:int}")]
        [Route("inspectors/{id:int}")]
        [Route("awExaminers/{id:int}")]
        [Route("staffExaminers/{id:int}")]
        public IHttpActionResult GetPerson(int id)
        {
            var person = this.personRepository.GetPerson(id);
            return Ok(new
            {
                nomValueId = person.LotId,
                name = string.Format("{0} {1}", person.Lin, person.Names)
            });
        }

        [Route("persons")]
        public IHttpActionResult GetPersons(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.personRepository.GetPersons()
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = string.Format("{0} {1}", e.Lin, e.Names)
                });

            if (!string.IsNullOrWhiteSpace(term))
            {
                returnValue = returnValue.Where(p => p.name.ToLower().Contains(term.ToLower()));
            }

            return Ok(returnValue);
        }

        [Route("commonQuestions")]
        public IHttpActionResult GetCommonQuestions(string term = null, int offset = 0, int? limit = null)
        {
            var aSExamQuestionType = this.nomRepository.GetNomValue("asExamQuestionTypes", "commonQuestions");

            var returnValue =
                this.personRepository.GetQuestions(aSExamQuestionType.NomValueId, name: term, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.ASExamVariantId,
                    name = e.Name
                });

            return Ok(returnValue);
        }

        [Route("specializedQuestions")]
        public IHttpActionResult GetSpecializedQuestions(string term = null, int offset = 0, int? limit = null)
        {
            var aSExamQuestionType = this.nomRepository.GetNomValue("asExamQuestionTypes", "specializedQuestions");

            var returnValue =
                this.personRepository.GetQuestions(aSExamQuestionType.NomValueId, name: term, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.ASExamVariantId,
                    name = e.Name
                });

            return Ok(returnValue);
        }

        [Route("inspectors")]
        public IHttpActionResult GetInspectors(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.personRepository.GetPersons(isInspector: true)
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = string.Format("{0} {1}", e.Lin, e.Names)
                });

            if (!string.IsNullOrEmpty(term))
            {
                returnValue = returnValue.Where(v => v.name.ToLower().Contains(term.ToLower()));
            }

            returnValue = returnValue.WithOffsetAndLimit(offset, limit);

            return Ok(returnValue);
        }

        [Route("awExaminers")]
        public IHttpActionResult GetAwExaminers(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.personRepository.GetAwExaminers()
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = string.Format("{0} {1}", e.Lin, e.Names)
                });

            if (!string.IsNullOrEmpty(term))
            {
                returnValue = returnValue.Where(v => v.name.ToLower().Contains(term.ToLower()));
            }

            returnValue = returnValue.WithOffsetAndLimit(offset, limit);

            return Ok(returnValue);
        }


        [Route("staffExaminers")]
        public IHttpActionResult GetStaffExaminers(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.personRepository.GetStaffExaminers()
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = string.Format("{0} {1}", e.Lin, e.Names)
                });

            if (!string.IsNullOrEmpty(term))
            {
                returnValue = returnValue.Where(v => v.name.ToLower().Contains(term.ToLower()));
            }

            returnValue = returnValue.WithOffsetAndLimit(offset, limit);

            return Ok(returnValue);
        }

        [Route("lotSets/{id:int}")]
        public IHttpActionResult GetLotSet(int id)
        {
            var lotSet = this.applicationRepository.GetLotSet(id);
            return Ok(new
            {
                nomValueId = lotSet.SetId,
                name = lotSet.Name,
                alias = lotSet.Alias
            });
        }

        [Route("lotSets")]
        public IHttpActionResult GetLotSets(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.applicationRepository.GetLotSets(name: term, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.SetId,
                    name = e.Name,
                    alias = e.Alias
                });

            return Ok(returnValue);
        }

        [Route("aircrafts/{id:int}")]
        public IHttpActionResult GetAircraft(int id)
        {
            var ac = this.aircraftRepository.GetAircraft(id);
            return Ok(new
            {
                nomValueId = ac.Item1.LotId,
                name = ac.Item1.Model,
                nameAlt = ac.Item1.ModelAlt,
                textContent =
                    new
                    {
                        airCategory = ac.Item1.AirCategory,
                        aircraftProducer = ac.Item1.AircraftProducer
                    }
            });
        }

        [Route("aircrafts")]
        public IHttpActionResult GetAircrafts(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.aircraftRepository.GetAircrafts(mark: null, manSN: null, modelAlt: null, icao: null, airCategory: null, aircraftProducer: null, exact: false)
                .Where(e => e.Item2 != null )
                .Select(e => new
                {
                    nomValueId = e.Item1.LotId,
                    name = string.Format("{0} ({1})", e.Item2.RegMark, e.Item1.Model),
                    nameAlt = string.Format("{0} ({1})", e.Item2.RegMark, e.Item1.ModelAlt)
                })
                .ToList();

            if (!string.IsNullOrEmpty(term))
            {
                returnValue = returnValue.Where(r => r.name.ToLower().Contains(term.ToLower())).ToList();
            }

            return Ok(returnValue.WithOffsetAndLimit(offset, limit));
        }

        [Route("aircraftModels/{id:int}")]
        public IHttpActionResult GetAircraftModel(int id)
        {
            var ac = this.aircraftRepository.GetAircraft(id);
            return Ok(new
            {
                nomValueId = ac.Item1.LotId,
                name = string.Format("{0} ({1})", ac.Item2.RegMark, ac.Item1.Model),
                nameAlt = string.Format("{0} ({1})", ac.Item2.RegMark, ac.Item1.ModelAlt)
            });
        }

        [Route("aircraftModels")]
        public IHttpActionResult GetAircraftModels(
            string term = null,
            int offset = 0,
            int? limit = null,
            string airCategory = "",
            string aircraftProducer = "")
        {
            var returnValue =
                this.aircraftRepository.GetAircrafts(
                    airCategory: airCategory,
                    aircraftProducer: aircraftProducer,
                    exact: true)
                    .GroupBy(a => a.Item1.Model)
                    .Select(g => g.FirstOrDefault())
                .Select(e => new
                {
                    nomValueId = e.Item1.LotId,
                    name = e.Item1.Model,
                    nameAlt = e.Item1.ModelAlt,
                    textContent =
                        new
                        {
                            airCategory = e.Item1.AirCategory,
                            aircraftProducer = e.Item1.AircraftProducer
                        }
                });

            return Ok(returnValue.WithOffsetAndLimit(offset, limit));
        }

        [Route("airports/{id:int}")]
        public IHttpActionResult GetAirport(int id)
        {
            var returnValue = this.airportRepository.GetAirport(id);
            return Ok(new
            {
                nomValueId = returnValue.LotId,
                name = returnValue.Name
            });
        }

        [Route("airports")]
        public IHttpActionResult GetAirports(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.airportRepository.GetAirports(name: term, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = e.Name
                });

            return Ok(returnValue);
        }

        [Route("equipments/{id:int}")]
        public IHttpActionResult GetEquipment(int id)
        {
            var returnValue = this.equipmentRepository.GetEquipment(id);
            return Ok(new
            {
                nomValueId = returnValue.LotId,
                name = returnValue.Name
            });
        }

        [Route("equipments")]
        public IHttpActionResult GetEquipments(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.equipmentRepository.GetEquipments(name: term, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = e.Name
                });

            return Ok(returnValue);
        }

        [Route("organizations/{id:int}")]
        public IHttpActionResult GetOrganization(int id)
        {
            var returnValue = this.organizationRepository.GetOrganization(id);
            return Ok(new
            {
                NomValueId = returnValue.LotId,
                Name = returnValue.Name,
                NameAlt = returnValue.NameAlt
            });
        }

        [Route("organizations")]
        public IHttpActionResult GetOrganizations(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.organizationRepository.GetOrganizations(name: term, caseTypeId: null, offset: offset, limit: limit, uin: null, cao: null, dateCaoValidTo: null, dateValidTo: null)
                .Select(e => new
                {
                    NomValueId = e.LotId,
                    Name = e.Name,
                    NameAlt = e.NameAlt
                });

            return Ok(returnValue);
        }

        [Route("caseTypes")]
        [Route("{set:regex(^(?:person|organization)$)}CaseTypes")]
        public IHttpActionResult GetCaseTypes(string set = null, string term = null, [FromUri] int[] ids = null)
        {
            IEnumerable<GvaCaseType> caseTypes = null;
            if (!string.IsNullOrEmpty(set))
            {
                caseTypes = this.caseTypeRepository.GetCaseTypesForSet(set);
            }
            else
            {
                caseTypes = this.caseTypeRepository.GetAllCaseTypes();
            }

            if (ids != null && ids.Count() > 0)
            {
                caseTypes = caseTypes.Where(nv => ids.Contains(nv.GvaCaseTypeId));
            }

            if (!string.IsNullOrWhiteSpace(term))
            {
                caseTypes = caseTypes.Where(ct => ct.Name.ToLower().Contains(term.ToLower()));
            }

            return Ok(
                caseTypes.Select(ct => new
                {
                    NomValueId = ct.GvaCaseTypeId,
                    Name = ct.Name,
                    Alias = ct.Alias
                }));
        }

        [Route("caseTypes")]
        public IHttpActionResult GetCaseTypesForLot(int lotId, string term = null)
        {
            term = term ?? string.Empty;

            var caseTypes = this.caseTypeRepository.GetCaseTypesForLot(lotId)
                .Where(ct => ct.Name.ToLower().Contains(term.ToLower()))
                .Select(ct => new
                {
                    NomValueId = ct.GvaCaseTypeId,
                    Name = ct.Name,
                    Alias = ct.Alias
                });

            return Ok(caseTypes);
        }

        [Route("caseTypes/{id:int}"),
         Route("personCaseTypes/{id:int}"),
         Route("organizationCaseTypes/{id:int}")]
        public IHttpActionResult GetCaseType(int id)
        {
            var caseType = this.caseTypeRepository.GetCaseType(id);
            return Ok(new
            {
                NomValueId = caseType.GvaCaseTypeId,
                Name = caseType.Name,
                Alias = caseType.Alias
            });
        }

        [Route("schools")]
        public IHttpActionResult GetSchools(string term = null, int? graduationId = null, int offset = 0, int? limit = null)
        {
            if (graduationId == null)
            {
                return Ok(this.nomRepository.GetNomValues("schools", term, offset: offset, limit: limit));
            }

            return Ok(
                this.nomRepository.GetNomValues("schools", term)
                .Where(nv => nv.TextContent.GetItems<int>("graduationIds").Contains(graduationId.Value))
                .WithOffsetAndLimit(offset, limit));
        }

        [Route("addressTypes")]
        public IHttpActionResult GetAddressTypes(string term = null, string type = null, int offset = 0, int? limit = null)
        {
            if (type == null)
            {
                return Ok(this.nomRepository.GetNomValues("addressTypes", term, offset: offset, limit: limit));
            }

            return Ok(
                this.nomRepository.GetNomValues("addressTypes", term)
                .Where(nv => nv.TextContent.Get<string>("type") == type)
                .WithOffsetAndLimit(offset, limit));
        }

        [Route("documentRoles")]
        public IHttpActionResult GetDocumentRoles(
            string term = null,
            string categoryAlias = null,
            string valueAlias = null,
            [FromUri] string[] caseTypeAliases = null,
            [FromUri] string[] withoutCertsAliases = null,
            int offset = 0,
            int? limit = null)
        {
            IEnumerable<NomValue> nomValues;
            if (categoryAlias == null && (caseTypeAliases == null || caseTypeAliases.Length == 0))
            {
                nomValues = this.nomRepository.GetNomValues("documentRoles", term: term, offset: offset, limit: limit);
            }
            else if (valueAlias != null)
            {
                nomValues = this.nomRepository.GetNomValues("documentRoles", valueAliases: new string[] { valueAlias });
            }
            else
            {
                nomValues =
                    this.nomRepository.GetNomValues("documentRoles", term)
                    .Where(nv =>
                    {
                        bool isMatch = true;

                        if (categoryAlias != null)
                        {
                            isMatch &= nv.TextContent.Get<string>("categoryAlias") == categoryAlias;
                        }

                        if (withoutCertsAliases.Count() > 0)
                        {
                            foreach (var certAlias in withoutCertsAliases)
                            {
                                isMatch &= nv.Alias != certAlias;
                            }
                        }

                        if (isMatch && caseTypeAliases != null && caseTypeAliases.Length > 0)
                        {
                            string[] nomCaseTypeAliases = nv.TextContent.GetItems<string>("caseTypeAliases").ToArray();
                            isMatch &= nomCaseTypeAliases.Count() == 0 || caseTypeAliases.Any(cta => nomCaseTypeAliases.Contains(cta));
                        }

                        return isMatch;
                    })
                    .WithOffsetAndLimit(offset, limit);
            }

            return Ok(nomValues);
        }

        [Route("documentParts")]
        public IHttpActionResult GetDocumentParts(string term = null, string set = null, bool withApplications = false)
        {
            IEnumerable<NomValue> nomValues  = null;
            if (withApplications)
            {
                nomValues = this.nomRepository.GetNomValues("documentParts", term);
            }
            else
            {
                nomValues = this.nomRepository.GetNomValues("documentParts", term).Where(nv => !nv.Code.Contains("Application"));
            }

            if (!string.IsNullOrEmpty(set))
            {
                nomValues = nomValues.Where(p => p.Code.Contains(set));
            }

            return Ok(nomValues);
        }

        [Route("limitations66")]
        public IHttpActionResult GetLimitations66(string term = null, string type = null)
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("limitations66", term);
            if (!string.IsNullOrEmpty(type))
            {
                nomValues = nomValues.Where(l => l.TextContent.Get<string>("type") == type);
            }

            return Ok(nomValues);
        }

        [Route("licenceTypes")]
        public IHttpActionResult GetLicenceTypes(string term = null, string caseTypeAlias = null,  [FromUri] int[] ids = null)
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("licenceTypes", term);

            if (ids.Count() > 0)
            {
                nomValues = nomValues.Where(nv => ids.Contains(nv.NomValueId));
            }

            if (!string.IsNullOrEmpty(caseTypeAlias))
            {
                nomValues = nomValues.Where(l => l.TextContent.Get<string>("caseTypeAlias") == caseTypeAlias);
            }

            return Ok(nomValues);
        }

        [Route("documentTypes")]
        public IHttpActionResult GetDocumentTypes(string term = null, bool? isIdDocument = null, [FromUri] string[] caseTypeAliases = null, int offset = 0, int? limit = null)
        {
            IEnumerable<NomValue> nomValues;
            if (isIdDocument == null && (caseTypeAliases == null || caseTypeAliases.Length > 0))
            {
                nomValues = this.nomRepository.GetNomValues("documentTypes", term: term, offset: offset, limit: limit);
            }
            else
            {
                nomValues =
                    this.nomRepository.GetNomValues("documentTypes", term)
                    .Where(nv =>
                    {
                        bool isMatch = true;

                        if (isIdDocument != null)
                        {
                            isMatch &= nv.TextContent.Get<bool>("isIdDocument") == isIdDocument;
                        }

                        if (isMatch && caseTypeAliases != null && caseTypeAliases.Length > 0)
                        {
                            string[] nomCaseTypeAliases = nv.TextContent.GetItems<string>("caseTypeAliases").ToArray();
                            isMatch &= nomCaseTypeAliases.Count() == 0 || caseTypeAliases.Any(cta => nomCaseTypeAliases.Contains(cta));
                        }

                        return isMatch;
                    })
                    .WithOffsetAndLimit(offset, limit);
            }

            return Ok(nomValues);
        }

        [Route("aircraftProducers")]
        public IHttpActionResult GetAircraftProducers(string term = null, int offset = 0, int? limit = null, bool? makeEngine = false, bool? makeRadio = false, bool? makePropeller = false, bool? makeAircraft = false)
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("aircraftProducers", term).Where(ap =>
            {
                bool isMatch = true;

                if (makeEngine.Value)
                {
                    isMatch &= ap.TextContent != null && ap.TextContent.Get<bool>("makeEngine") ? true : false;
                }
                else if (makeRadio.Value)
                {
                    isMatch &= ap.TextContent != null && ap.TextContent.Get<bool>("makeRadio") ? true : false;
                }
                else if (makePropeller.Value)
                {
                    isMatch &= ap.TextContent != null && ap.TextContent.Get<bool>("makePropeller") ? true : false;
                }
                else if (makeAircraft.Value)
                {
                    isMatch &= ap.TextContent != null && ap.TextContent.Get<bool>("makeAircraft") ? true : false;
                }

                return isMatch;
            });

            return Ok(nomValues);
        }

        [Route("linTypes")]
        public IHttpActionResult GetLinTypes()
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("linTypes");

            return Ok(nomValues);
        }

        [Route("inspectorTypes")]
        public IHttpActionResult GetInspectorTypes(bool? showInspectors = true)
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("inspectorTypes");
            if (!showInspectors.Value)
            {
                nomValues = nomValues.Where(e => e.Alias != "gvaInspector");
            }

            return Ok(nomValues);
        }

        [Route("ratingClasses")]
        public IHttpActionResult GetRatingClasses(string caseTypeAlias = null, string term = null)
        {
            IEnumerable<NomValue> nomValues = null;
            if (!string.IsNullOrEmpty(caseTypeAlias))
            {
                nomValues = this.nomRepository.GetNomValues(
                    alias: "ratingClasses",
                    parentAlias: "ratingClassGroups",
                    parentProp: "caseTypeAlias",
                    parentPropValue: caseTypeAlias);
            }
            else
            {
                nomValues = this.nomRepository.GetNomValues(alias: "ratingClasses");
            }

            nomValues = nomValues.Select(n => new NomValue
            {
                NomValueId = n.NomValueId,
                Name = n.Code == n.Name ? n.Name : string.Format("{0} {1}", n.Code, n.Name),
                Code = n.Code
            });

            if (!string.IsNullOrEmpty(term))
            {
                nomValues = nomValues.Where(n => n.Name.ToLower().Contains(term.ToLower()));
            }

            return Ok(nomValues.OrderBy(n => n.Name));
        }

        [Route("authorizations")]
        public IHttpActionResult GetAuthorizations(string caseTypeAlias = null, string term = null, int offset = 0, int? limit = null)
        {
            IEnumerable<NomValue> nomValues = null;
            if (!string.IsNullOrEmpty(caseTypeAlias))
            {
                nomValues = this.nomRepository.GetNomValues(
                    alias: "authorizations",
                    parentAlias: "authorizationGroups",
                    parentProp: "caseTypeAlias",
                    parentPropValue: caseTypeAlias);
            }
            else
            {
                nomValues = this.nomRepository.GetNomValues(alias: "authorizations");
            }

            nomValues = nomValues.Select(n => new NomValue
            {
                NomValueId = n.NomValueId,
                Name = n.Code == n.Name ? n.Name : string.Format("{0} {1}", n.Code, n.Name),
                Code = n.Code,
                ParentValueId = n.ParentValueId
            });

            if (!string.IsNullOrEmpty(term))
            {
                nomValues = nomValues.Where(n => n.Name.ToLower().Contains(term.ToLower()));
            }

            return Ok(nomValues.OrderBy(n => n.Name).Skip(offset).Take(limit ?? 10).ToList());
        }

        [Route("applicationTypes")]
        public IHttpActionResult GetApplicationTypes(string set = null, string caseTypeAlias = null, int? caseTypeId = null, string code = null, string name = null, int? lotId = null, string term = null, int offset = 0, int? limit = null)
        {
            var nomValues = this.nomRepository.GetNomValues(
                alias: "applicationTypes",
                term: term);

            if (!string.IsNullOrEmpty(set))
            {
                var caseTypeAliases = this.caseTypeRepository.GetCaseTypesForSet(set)
                    .Select(ct => ct.Alias);

                nomValues = nomValues.Where(nv => nv.TextContent.GetItems<string>("caseTypes").Any(ct => caseTypeAliases.Contains(ct)));
            }

            if (!string.IsNullOrWhiteSpace(caseTypeAlias))
            {
                nomValues = nomValues.Where(nv => nv.TextContent.GetItems<string>("caseTypes").Contains(caseTypeAlias));
            }
            else if (caseTypeId.HasValue)
            {
                string alias = this.caseTypeRepository.GetCaseType(caseTypeId.Value).Alias;

                nomValues = nomValues.Where(nv => nv.TextContent.GetItems<string>("caseTypes").Contains(alias));
            }

            if (!string.IsNullOrWhiteSpace(code))
            {
                nomValues = nomValues.Where(nv => nv.Code.ToLower().Contains(code.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                nomValues = nomValues.Where(nv => nv.Name.ToLower().Contains(name.ToLower()));
            }

            if (lotId.HasValue)
            {
                var caseTypeAliases = this.caseTypeRepository.GetCaseTypesForLot(lotId.Value)
                    .Select(ct => ct.Alias);

                nomValues = nomValues.Where(nv => nv.TextContent.GetItems<string>("caseTypes").Any(ct => caseTypeAliases.Contains(ct)));
            }

            nomValues = nomValues.Skip(offset);

            if (limit.HasValue)
            {
                nomValues = nomValues.Take(limit.Value);
            }

            return Ok(nomValues);
        }

        [Route("langLevels")]
        public IHttpActionResult GetLangLevels(string term = null, int? roleId = null, int offset = 0, int? limit = null)
        {
            var langLevels = this.nomRepository.GetNomValues(
                alias: "langLevels",
                term: term,
                offset: offset,
                limit: limit);

            if (roleId.HasValue)
            {
                string roleAlias = this.nomRepository.GetNomValue("documentRoles", roleId.Value).Alias;
                langLevels = langLevels.Where(l => l.TextContent.GetItems<string>("roleAliases").Contains(roleAlias));
            }

            return Ok(langLevels.OrderBy(l => l.TextContent.Get<int?>("seqNumber")));
        }

        [Route("appStages")]
        public IHttpActionResult GetAppStages(string term = null)
        {
            var returnValue = this.stageRepository.GetStages(term)
                .Select(e => new
                {
                    nomValueId = e.GvaStageId,
                    name = e.Name
                });

            return Ok(returnValue);
        }

        [Route("appStages/{id:int}")]
        public IHttpActionResult GetAppStage(int id)
        {
            var stage = this.stageRepository.GetStage(id);

            return Ok(new
            {
                nomValueId = stage.GvaStageId,
                name = stage.Name
            });
        }

        [Route("employments")]
        public IHttpActionResult GetEmployments(int lotId)
        {
            int validFalseId = this.nomRepository.GetNomValue("boolean", "no").NomValueId;
            var returnValue = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonEmploymentDO>("personDocumentEmployments")
                .Select(e =>
                    {
                        var organization = e.Content.OrganizationId.HasValue ?
                            this.organizationRepository.GetOrganization(e.Content.OrganizationId.Value) : null;

                        return new
                        {
                            nomValueId = e.Part.Index,
                            name = string.Format(
                                "{0}, {1} {2}",
                                organization != null ? organization.Name : null,
                                e.Content.Hiredate.Value.ToString("dd.MM.yyyy"),
                                e.Content.ValidId == validFalseId ? "(НЕВАЛИДНА)" : null)
                        };
                    });

            return Ok(returnValue);
        }

        [Route("ratingTypes")]
        public IHttpActionResult GetRatingTypes(string term = null, string caseTypeAlias = null, [FromUri] int[] ids = null)
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("ratingTypes");

            if (ids.Count() > 0)
            {
                nomValues = nomValues.Where(nv => ids.Contains(nv.NomValueId));
            }

            if (!string.IsNullOrEmpty(caseTypeAlias))
            {
                nomValues = nomValues.Where(l => l.TextContent.Get<string>("caseTypeAlias") == caseTypeAlias);
            }

            nomValues = nomValues.Select(n => new NomValue
            {
                NomValueId = n.NomValueId,
                Name = n.Code == n.Name ? n.Name : string.Format("{0} {1}", n.Code, n.Name),
                Code = n.Code
            });

            if (!string.IsNullOrEmpty(term))
            {
                nomValues = nomValues.Where(n => n.Name.ToLower().Contains(term.ToLower()));
            }

            return Ok(nomValues.OrderBy(n => n.Name));
        }

        [Route("aircraftTypeGroups")]
        public IHttpActionResult GetAircraftTypeGroups(string term = null, string caseTypeAlias = null)
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("aircraftTypeGroups");

            if (!string.IsNullOrEmpty(caseTypeAlias))
            {
                nomValues = nomValues.Where(l => l.TextContent.Get<string>("caseTypeAlias") == caseTypeAlias);
            }

            nomValues = nomValues.Select(n => new NomValue
            {
                NomValueId = n.NomValueId,
                Name = n.Code == n.Name ? n.Name : string.Format("{0} {1}", n.Code, n.Name),
                Code = n.Code
            });

            if (!string.IsNullOrEmpty(term))
            {
                nomValues = nomValues.Where(n => n.Name.ToLower().Contains(term.ToLower()));
            }

            return Ok(nomValues.OrderBy(n => n.Name));
        }

        
    }
}

