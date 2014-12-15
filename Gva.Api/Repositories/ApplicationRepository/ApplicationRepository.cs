using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories;
using Common.Data;
using Common.Linq;
using Gva.Api.Models;
using Gva.Api.Models.Views;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.Models.Views.Airport;
using Gva.Api.Models.Views.Equipment;
using Gva.Api.Models.Views.Organization;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Applications;
using Gva.Api.ModelsDO.Common;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Repositories.ApplicationRepository
{
    public class ApplicationRepository : Repository<GvaApplication>, IApplicationRepository
    {
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;

        public ApplicationRepository(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository)
            : base(unitOfWork)
        {
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
        }

        public IEnumerable<ApplicationListDO> GetApplications(
            string lotSetAlias = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? personLin = null,
            string aircraftIcao = null,
            string organizationUin = null,
            int offset = 0,
            int? limit = null
            )
        {
            if (lotSetAlias == "person")
            {
                return this.GetPersonApplications(fromDate, toDate, personLin, offset, limit);
            }
            else if (lotSetAlias == "aircraft")
            {
                return this.GetAircraftApplications(fromDate, toDate, aircraftIcao, offset, limit);
            }
            else if (lotSetAlias == "organization")
            {
                return this.GetOrganizationApplications(fromDate, toDate, organizationUin, offset, limit);
            }
            else if (lotSetAlias == "equipment")
            {
                return this.GetEquipmentApplications(fromDate, toDate, offset, limit);
            }
            else if (lotSetAlias == "airport")
            {
                return this.GetAirportApplications(fromDate, toDate, offset, limit);
            }
            else
            {
                throw new NotSupportedException("Cannot get applications with not selected lot set alias!");
            }
        }

        public IEnumerable<ApplicationListDO> GetPersonApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? personLin = null,
            int offset = 0,
            int? limit = null)
        {
            var applicationsJoin =
                from a in this.unitOfWork.DbContext.Set<GvaApplication>().Include(a => a.GvaAppLotPart)
                join lot in this.unitOfWork.DbContext.Set<Lot>().Include(l => l.Set) on a.LotId equals lot.LotId
                where lot.Set.Alias == "person"
                join part in this.unitOfWork.DbContext.Set<Part>() on a.GvaAppLotPartId equals part.PartId into part1
                from part2 in part1.DefaultIfEmpty()

                join va in this.unitOfWork.DbContext.Set<GvaViewApplication>() on a.GvaAppLotPartId equals va.PartId into va1
                from va2 in va1.DefaultIfEmpty()
                join nv7 in this.unitOfWork.DbContext.Set<NomValue>() on va2.ApplicationTypeId equals nv7.NomValueId into nv8
                from nv9 in nv8.DefaultIfEmpty()

                join p in this.unitOfWork.DbContext.Set<GvaViewPerson>() on va2.LotId equals p.LotId into p1
                from p2 in p1.DefaultIfEmpty()
                select new
                {
                    GApplication = a,
                    Set = lot.Set,
                    GApplicationPart = part2,
                    GViewApplication = va2,
                    GViewApplicationType = nv9,
                    GViewPerson = p2
                };

            var predicate = PredicateBuilder.True(new
            {
                GApplication = new GvaApplication(),
                Set = new Set(),
                GApplicationPart = new Part(),
                GViewApplication = new GvaViewApplication(),
                GViewApplicationType = new NomValue(),
                GViewPerson = new GvaViewPerson()
            });

            predicate = predicate
                .AndDateTimeGreaterThanOrEqual(e => e.GViewApplication.DocumentDate, fromDate)
                .AndDateTimeLessThanOrEqual(e => e.GViewApplication.DocumentDate, toDate)
                .AndEquals(e => e.GViewPerson.Lin, personLin);

            var applications = applicationsJoin
                .Where(predicate)
                .OrderByDescending(p => p.GApplication.GvaApplicationId)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return applications
                .Select(e => new ApplicationListDO()
                {
                    ApplicationId = e.GApplication.GvaApplicationId,
                    DocId = e.GApplication.DocId,
                    LotSetName = e.Set.Name,
                    AppPartId = e.GApplication.GvaAppLotPartId,
                    LotId = e.GApplication.LotId,
                    AppPartIndex = e.GApplicationPart != null ? e.GApplicationPart.Index : (int?)null,
                    AppPartDocumentDate = e.GViewApplication != null ? e.GViewApplication.DocumentDate : null,
                    AppPartDocumentNumber = e.GViewApplication != null ? e.GViewApplication.DocumentNumber : null,
                    AppPartApplicationTypeName = e.GViewApplicationType != null ? e.GViewApplicationType.Name : null,
                    PersonId = e.GViewPerson != null ? (int?)e.GViewPerson.LotId : null,
                    PersonLin = e.GViewPerson != null ? (int?)e.GViewPerson.Lin : null,
                    PersonNames = e.GViewPerson != null ? e.GViewPerson.Names : null
                })
                .ToList();
        }

        public IEnumerable<ApplicationListDO> GetAircraftApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string aircraftIcao = null,
            int offset = 0,
            int? limit = null)
        {
            var applicationsJoin =
                    from a in this.unitOfWork.DbContext.Set<GvaApplication>().Include(a => a.GvaAppLotPart)
                    join lot in this.unitOfWork.DbContext.Set<Lot>().Include(l => l.Set) on a.LotId equals lot.LotId
                    where lot.Set.Alias == "aircraft"
                    join part in this.unitOfWork.DbContext.Set<Part>() on a.GvaAppLotPartId equals part.PartId into part1
                    from part2 in part1.DefaultIfEmpty()

                    join va in this.unitOfWork.DbContext.Set<GvaViewApplication>() on a.GvaAppLotPartId equals va.PartId into va1
                    from va2 in va1.DefaultIfEmpty()
                    join nv7 in this.unitOfWork.DbContext.Set<NomValue>() on va2.ApplicationTypeId equals nv7.NomValueId into nv8
                    from nv9 in nv8.DefaultIfEmpty()

                    join ac in this.unitOfWork.DbContext.Set<GvaViewAircraft>() on va2.LotId equals ac.LotId into ac1
                    from ac2 in ac1.DefaultIfEmpty()
                    join nv1 in this.unitOfWork.DbContext.Set<NomValue>() on ac2.AirCategoryId equals nv1.NomValueId into nv2
                    from nv3 in nv2.DefaultIfEmpty()
                    join nv4 in this.unitOfWork.DbContext.Set<NomValue>() on ac2.AircraftProducerId equals nv4.NomValueId into nv5
                    from nv6 in nv5.DefaultIfEmpty()
                    select new
                    {
                        GApplication = a,
                        Set = lot.Set,
                        GApplicationPart = part2,
                        GViewApplication = va2,
                        GViewApplicationType = nv9,
                        GViewAircraft = ac2,
                        GViewAircraftCat = nv3,
                        GViewAircraftProd = nv6
                    };

            var predicate = PredicateBuilder.True(new
            {
                GApplication = new GvaApplication(),
                Set = new Set(),
                GApplicationPart = new Part(),
                GViewApplication = new GvaViewApplication(),
                GViewApplicationType = new NomValue(),
                GViewAircraft = new GvaViewAircraft(),
                GViewAircraftCat = new NomValue(),
                GViewAircraftProd = new NomValue()
            });

            predicate = predicate
                .AndDateTimeGreaterThanOrEqual(e => e.GViewApplication.DocumentDate, fromDate)
                .AndDateTimeLessThanOrEqual(e => e.GViewApplication.DocumentDate, toDate)
                .AndStringContains(e => e.GViewAircraft.ICAO, aircraftIcao);

            var applications = applicationsJoin
                .Where(predicate)
                .OrderByDescending(p => p.GApplication.GvaApplicationId)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return applications
                .Select(e => new ApplicationListDO()
                {
                    ApplicationId = e.GApplication.GvaApplicationId,
                    DocId = e.GApplication.DocId,
                    LotSetName = e.Set.Name,
                    AppPartId = e.GApplication.GvaAppLotPartId,
                    LotId = e.GApplication.LotId,
                    AppPartIndex = e.GApplicationPart != null ? e.GApplicationPart.Index : (int?)null,
                    AppPartDocumentDate = e.GViewApplication != null ? e.GViewApplication.DocumentDate : null,
                    AppPartDocumentNumber = e.GViewApplication != null ? e.GViewApplication.DocumentNumber : null,
                    AppPartApplicationTypeName = e.GViewApplicationType != null ? e.GViewApplicationType.Name : null,
                    GvaAircraftId = e.GViewAircraft != null ? (int?)e.GViewAircraft.LotId : null,
                    GvaAirCategory = e.GViewAircraftCat != null ? e.GViewAircraftCat.Name : null,
                    GvaAircraftProducer = e.GViewAircraftProd != null ? e.GViewAircraftProd.Name : null,
                    GvaAircraftICAO = e.GViewAircraft != null ? e.GViewAircraft.ICAO : null
                })
                .ToList();
        }

        public IEnumerable<ApplicationListDO> GetOrganizationApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string organizationUin = null,
            int offset = 0,
            int? limit = null)
        {
            var applicationsJoin =
                    from a in this.unitOfWork.DbContext.Set<GvaApplication>().Include(a => a.GvaAppLotPart)
                    join lot in this.unitOfWork.DbContext.Set<Lot>().Include(l => l.Set) on a.LotId equals lot.LotId
                    where lot.Set.Alias == "organization"
                    join part in this.unitOfWork.DbContext.Set<Part>() on a.GvaAppLotPartId equals part.PartId into part1
                    from part2 in part1.DefaultIfEmpty()

                    join va in this.unitOfWork.DbContext.Set<GvaViewApplication>() on a.GvaAppLotPartId equals va.PartId into va1
                    from va2 in va1.DefaultIfEmpty()
                    join nv7 in this.unitOfWork.DbContext.Set<NomValue>() on va2.ApplicationTypeId equals nv7.NomValueId into nv8
                    from nv9 in nv8.DefaultIfEmpty()

                    join o in this.unitOfWork.DbContext.Set<GvaViewOrganization>().Include(o => o.OrganizationType) on va2.LotId equals o.LotId into o1
                    from o2 in o1.DefaultIfEmpty()
                    join nv19 in this.unitOfWork.DbContext.Set<NomValue>() on o2.OrganizationTypeId equals nv19.NomValueId into nv20
                    from nv21 in nv20.DefaultIfEmpty()
                    select new
                    {
                        GApplication = a,
                        Set = lot.Set,
                        GApplicationPart = part2,
                        GViewApplication = va2,
                        GViewApplicationType = nv9,
                        GViewOrganization = o2,
                        GViewOrganizationType = nv21
                    };

            var predicate = PredicateBuilder.True(new
            {
                GApplication = new GvaApplication(),
                Set = new Set(),
                GApplicationPart = new Part(),
                GViewApplication = new GvaViewApplication(),
                GViewApplicationType = new NomValue(),
                GViewOrganization = new GvaViewOrganization(),
                GViewOrganizationType = new NomValue()
            });

            predicate = predicate
                .AndDateTimeGreaterThanOrEqual(e => e.GViewApplication.DocumentDate, fromDate)
                .AndDateTimeLessThanOrEqual(e => e.GViewApplication.DocumentDate, toDate)
                .AndStringContains(e => e.GViewOrganization.Uin, organizationUin);

            var applications = applicationsJoin
                .Where(predicate)
                .OrderByDescending(p => p.GApplication.GvaApplicationId)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return applications
                .Select(e => new ApplicationListDO()
                {
                    ApplicationId = e.GApplication.GvaApplicationId,
                    DocId = e.GApplication.DocId,
                    LotSetName = e.Set.Name,
                    AppPartId = e.GApplication.GvaAppLotPartId,
                    LotId = e.GApplication.LotId,
                    AppPartIndex = e.GApplicationPart != null ? e.GApplicationPart.Index : (int?)null,
                    AppPartDocumentDate = e.GViewApplication != null ? e.GViewApplication.DocumentDate : null,
                    AppPartDocumentNumber = e.GViewApplication != null ? e.GViewApplication.DocumentNumber : null,
                    AppPartApplicationTypeName = e.GViewApplicationType != null ? e.GViewApplicationType.Name : null,
                    GvaOrganizationId = e.GViewOrganization != null ? (int?)e.GViewOrganization.LotId : null,
                    GvaOrganizationName = e.GViewOrganization != null ? e.GViewOrganization.Name : null,
                    GvaOrganizationUin = e.GViewOrganization != null ? e.GViewOrganization.Uin : null
                })
                .ToList();
        }

        public IEnumerable<ApplicationListDO> GetEquipmentApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int offset = 0,
            int? limit = null)
        {
            var applicationsJoin =
                    from a in this.unitOfWork.DbContext.Set<GvaApplication>().Include(a => a.GvaAppLotPart)
                    join lot in this.unitOfWork.DbContext.Set<Lot>().Include(l => l.Set) on a.LotId equals lot.LotId
                    where lot.Set.Alias == "equipment"
                    join part in this.unitOfWork.DbContext.Set<Part>() on a.GvaAppLotPartId equals part.PartId into part1
                    from part2 in part1.DefaultIfEmpty()

                    join va in this.unitOfWork.DbContext.Set<GvaViewApplication>() on a.GvaAppLotPartId equals va.PartId into va1
                    from va2 in va1.DefaultIfEmpty()
                    join nv7 in this.unitOfWork.DbContext.Set<NomValue>() on va2.ApplicationTypeId equals nv7.NomValueId into nv8
                    from nv9 in nv8.DefaultIfEmpty()

                    join eq in this.unitOfWork.DbContext.Set<GvaViewEquipment>() on va2.LotId equals eq.LotId into eq1
                    from eq2 in eq1.DefaultIfEmpty()
                    join nv10 in this.unitOfWork.DbContext.Set<NomValue>() on eq2.EquipmentTypeId equals nv10.NomValueId into nv11
                    from nv12 in nv11.DefaultIfEmpty()
                    join nv13 in this.unitOfWork.DbContext.Set<NomValue>() on eq2.EquipmentProducerId equals nv13.NomValueId into nv14
                    from nv15 in nv14.DefaultIfEmpty()
                    select new
                    {
                        GApplication = a,
                        Set = lot.Set,
                        GApplicationPart = part2,
                        GViewApplication = va2,
                        GViewApplicationType = nv9,
                        GViewEquipment = eq2,
                        GViewEquipmentType = nv12,
                        GViewEquipmentProducer = nv15
                    };

            var predicate = PredicateBuilder.True(new
            {
                GApplication = new GvaApplication(),
                Set = new Set(),
                GApplicationPart = new Part(),
                GViewApplication = new GvaViewApplication(),
                GViewApplicationType = new NomValue(),
                GViewEquipment = new GvaViewEquipment(),
                GViewEquipmentType = new NomValue(),
                GViewEquipmentProducer = new NomValue()
            });

            predicate = predicate
                .AndDateTimeGreaterThanOrEqual(e => e.GViewApplication.DocumentDate, fromDate)
                .AndDateTimeLessThanOrEqual(e => e.GViewApplication.DocumentDate, toDate);

            var applications = applicationsJoin
                .Where(predicate)
                .OrderByDescending(p => p.GApplication.GvaApplicationId)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return applications
                .Select(e => new ApplicationListDO()
                {
                    ApplicationId = e.GApplication.GvaApplicationId,
                    DocId = e.GApplication.DocId,
                    LotSetName = e.Set.Name,
                    AppPartId = e.GApplication.GvaAppLotPartId,
                    LotId = e.GApplication.LotId,
                    AppPartIndex = e.GApplicationPart != null ? e.GApplicationPart.Index : (int?)null,
                    AppPartDocumentDate = e.GViewApplication != null ? e.GViewApplication.DocumentDate : null,
                    AppPartDocumentNumber = e.GViewApplication != null ? e.GViewApplication.DocumentNumber : null,
                    AppPartApplicationTypeName = e.GViewApplicationType != null ? e.GViewApplicationType.Name : null,
                    GvaEquipmentId = e.GViewEquipment != null ? (int?)e.GViewEquipment.LotId : null,
                    GvaEquipmentName = e.GViewEquipment != null ? e.GViewEquipment.Name : null,
                    GvaEquipmentType = e.GViewEquipmentType != null ? e.GViewEquipmentType.Name : null,
                    GvaEquipmentProducer = e.GViewEquipmentProducer != null ? e.GViewEquipmentProducer.Name : null
                })
                .ToList();
        }

        public IEnumerable<ApplicationListDO> GetAirportApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int offset = 0,
            int? limit = null)
        {
            var applicationsJoin =
                    from a in this.unitOfWork.DbContext.Set<GvaApplication>().Include(a => a.GvaAppLotPart)
                    join lot in this.unitOfWork.DbContext.Set<Lot>().Include(l => l.Set) on a.LotId equals lot.LotId
                    where lot.Set.Alias == "airport"
                    join part in this.unitOfWork.DbContext.Set<Part>() on a.GvaAppLotPartId equals part.PartId into part1
                    from part2 in part1.DefaultIfEmpty()

                    join va in this.unitOfWork.DbContext.Set<GvaViewApplication>() on a.GvaAppLotPartId equals va.PartId into va1
                    from va2 in va1.DefaultIfEmpty()
                    join nv7 in this.unitOfWork.DbContext.Set<NomValue>() on va2.ApplicationTypeId equals nv7.NomValueId into nv8
                    from nv9 in nv8.DefaultIfEmpty()

                    join ap in this.unitOfWork.DbContext.Set<GvaViewAirport>() on va2.LotId equals ap.LotId into ap1
                    from ap2 in ap1.DefaultIfEmpty()
                    join nv16 in this.unitOfWork.DbContext.Set<NomValue>() on ap2.AirportTypeId equals nv16.NomValueId into nv17
                    from nv18 in nv17.DefaultIfEmpty()
                    select new
                    {
                        GApplication = a,
                        Set = lot.Set,
                        GApplicationPart = part2,
                        GViewApplication = va2,
                        GViewApplicationType = nv9,
                        GViewAirport = ap2,
                        GViewAirportType = nv18
                    };

            var predicate = PredicateBuilder.True(new
            {
                GApplication = new GvaApplication(),
                Set = new Set(),
                GApplicationPart = new Part(),
                GViewApplication = new GvaViewApplication(),
                GViewApplicationType = new NomValue(),
                GViewAirport = new GvaViewAirport(),
                GViewAirportType = new NomValue()
            });

            predicate = predicate
                .AndDateTimeGreaterThanOrEqual(e => e.GViewApplication.DocumentDate, fromDate)
                .AndDateTimeLessThanOrEqual(e => e.GViewApplication.DocumentDate, toDate);

            var applications = applicationsJoin
                .Where(predicate)
                .OrderByDescending(p => p.GApplication.GvaApplicationId)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return applications
                .Select(e => new ApplicationListDO()
                {
                    ApplicationId = e.GApplication.GvaApplicationId,
                    DocId = e.GApplication.DocId,
                    LotSetName = e.Set.Name,
                    AppPartId = e.GApplication.GvaAppLotPartId,
                    LotId = e.GApplication.LotId,
                    AppPartIndex = e.GApplicationPart != null ? e.GApplicationPart.Index : (int?)null,
                    AppPartDocumentDate = e.GViewApplication != null ? e.GViewApplication.DocumentDate : null,
                    AppPartDocumentNumber = e.GViewApplication != null ? e.GViewApplication.DocumentNumber : null,
                    AppPartApplicationTypeName = e.GViewApplicationType != null ? e.GViewApplicationType.Name : null,
                    GvaAirportId = e.GViewAirport != null ? (int?)e.GViewAirport.LotId : null,
                    GvaAirportType = e.GViewAirportType != null ? e.GViewAirportType.Name : null,
                    GvaAirportName = e.GViewAirport != null ? e.GViewAirport.Name : null
                })
                .ToList();
        }
        public IEnumerable<GvaApplication> GetLinkedToDocsApplications()
        {
            return this.unitOfWork.DbContext.Set<GvaApplication>().Where(e => e.DocId.HasValue).AsQueryable();
        }

        public GvaApplication[] GetNomApplications(int lotId)
        {
            return this.unitOfWork.DbContext.Set<GvaApplication>()
                .Include(a => a.GvaAppLotPart)
                .Where(a => a.LotId == lotId && a.GvaAppLotPart != null)
                .ToArray();
        }

        public GvaApplication GetNomApplication(int applicationId)
        {
            return this.unitOfWork.DbContext.Set<GvaApplication>()
                .Include(a => a.GvaAppLotPart.Lot)
                .FirstOrDefault(a => a.GvaApplicationId == applicationId);
        }

        public void AddGvaApplication(GvaApplication gvaApplication)
        {
            this.unitOfWork.DbContext.Set<GvaApplication>().Add(gvaApplication);
        }

        public void DeleteGvaApplication(int gvaAppLotPartId)
        {
            var application = this.unitOfWork.DbContext.Set<GvaApplication>()
                .Include(a => a.GvaAppLotFiles)
                .SingleOrDefault(a => a.GvaAppLotPartId == gvaAppLotPartId);

            if (application != null)
            {
                foreach (var appFile in application.GvaAppLotFiles.ToList())
                {
                    this.unitOfWork.DbContext.Set<GvaAppLotFile>().Remove(appFile);
                }

                this.unitOfWork.DbContext.Set<GvaApplication>().Remove(application);
            }
        }

        public void AddGvaLotFile(GvaLotFile gvaLotFile)
        {
            this.unitOfWork.DbContext.Set<GvaLotFile>().Add(gvaLotFile);
        }

        public void AddGvaAppLotFile(GvaAppLotFile gvaAppLotFile)
        {
            this.unitOfWork.DbContext.Set<GvaAppLotFile>().Add(gvaAppLotFile);
        }

        public List<GvaCorrespondent> GetGvaCorrespondentsByLotId(int lotId)
        {
            return this.unitOfWork.DbContext.Set<GvaCorrespondent>()
                .Include(e => e.Correspondent)
                .Where(p => p.LotId == lotId)
                .ToList();
        }

        public void AddGvaCorrespondent(GvaCorrespondent gvaCorrespondent)
        {
            this.unitOfWork.DbContext.Set<GvaCorrespondent>().Add(gvaCorrespondent);
        }

        public GvaApplication GetGvaApplicationByDocId(int docId)
        {
            return this.unitOfWork.DbContext.Set<GvaApplication>()
                .FirstOrDefault(e => e.DocId == docId);
        }

        public IEnumerable<Set> GetLotSets(
            string names = null,
            bool exact = false,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<Set>();

            predicate = predicate
                .AndStringMatches(p => p.Name, names, exact);

            var persons = this.unitOfWork.DbContext.Set<Set>()
                .Where(predicate);

            return persons
                .OrderBy(p => p.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public Set GetLotSet(int lotSetId)
        {
            return this.unitOfWork.DbContext.Set<Set>()
                .SingleOrDefault(p => p.SetId == lotSetId);
        }

        public ApplicationNomDO GetInitApplication(int? applicationId)
        {
            if (!applicationId.HasValue)
            {
                return null;
            }

            GvaApplication nomApp = this.GetNomApplication((int)applicationId);
            if (nomApp != null && nomApp.GvaAppLotPart != null)
            {
                return new ApplicationNomDO(nomApp);
            }

            return null;
        }

        public List<CaseTypePartDO<DocumentApplicationDO>> GetApplicationsForLot(int lotId, string path, int? caseTypeId = null)
        {
            var partVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts<DocumentApplicationDO>(path);

            List<CaseTypePartDO<DocumentApplicationDO>> partVersionDOs = new List<CaseTypePartDO<DocumentApplicationDO>>();
            foreach (var partVersion in partVersions)
            {
                var stages = this.unitOfWork.DbContext.Set<GvaApplicationStage>()
                    .Include(s => s.GvaStage)
                    .Where(s => s.GvaApplication.LotId == lotId && s.GvaApplication.GvaAppLotPartId == partVersion.PartId)
                    .OrderByDescending(a => a.GvaAppStageId)
                    .ToList();

                var gvaApplication = this.unitOfWork.DbContext.Set<GvaApplication>()
                    .Include(a => a.GvaAppLotPart)
                    .Where(a => a.GvaAppLotPartId == partVersion.PartId)
                    .Single();

                partVersion.Content.ApplicationId = gvaApplication.GvaApplicationId;

                if (stages.Count() > 0)
                {
                    var applicationStage = stages.First();
                    partVersion.Content.Stage = new NomValue()
                    {
                        Name = applicationStage.GvaStage.Name,
                        Alias = applicationStage.GvaStage.Alias
                    };
                }

                var lotFile = this.fileRepository.GetFileReference(partVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    partVersionDOs.Add(new CaseTypePartDO<DocumentApplicationDO>(partVersion, lotFile));
                }
            }

            return partVersionDOs;
        }

        public CaseTypePartDO<DocumentApplicationDO> GetApplicationPart(string path, int lotId)
        {
            var partVersion = this.lotRepository.GetLotIndex(lotId).Index.GetPart<DocumentApplicationDO>(path);

            var stages = this.unitOfWork.DbContext.Set<GvaApplicationStage>()
                .Include(s => s.GvaStage)
                .Where(s => s.GvaApplication.LotId == lotId && s.GvaApplication.GvaAppLotPartId == partVersion.PartId)
                .OrderByDescending(a => a.GvaAppStageId)
                .ToList();

            var gvaApplication = this.unitOfWork.DbContext.Set<GvaApplication>()
                    .Include(a => a.GvaAppLotPart)
                    .Where(a => a.GvaAppLotPartId == partVersion.PartId)
                    .Single();

            partVersion.Content.ApplicationId = gvaApplication.GvaApplicationId;

            if (stages.Count() > 0)
            {
                var applicationStage = stages.FirstOrDefault();
                partVersion.Content.Stage = new NomValue()
                {
                    Name = applicationStage.GvaStage.Name,
                    Alias = applicationStage.GvaStage.Alias
                };
            }

            var lotFile = this.fileRepository.GetFileReference(partVersion.PartId, null);

            return new CaseTypePartDO<DocumentApplicationDO>(partVersion, lotFile);
        }
    }
}
