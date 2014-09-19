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
using Regs.Api.Models;

namespace Gva.Api.Repositories.ApplicationRepository
{
    public class ApplicationRepository : Repository<GvaApplication>, IApplicationRepository
    {
        public ApplicationRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
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
            var applicationsJoin =
                from a in this.unitOfWork.DbContext.Set<GvaApplication>()
                join lot in this.unitOfWork.DbContext.Set<Lot>() on a.LotId equals lot.LotId
                join set in this.unitOfWork.DbContext.Set<Set>() on lot.SetId equals set.SetId
                join part in this.unitOfWork.DbContext.Set<Part>() on a.GvaAppLotPartId equals part.PartId into part1
                from part2 in part1.DefaultIfEmpty()

                join va in this.unitOfWork.DbContext.Set<GvaViewApplication>() on a.GvaAppLotPartId equals va.PartId into va1
                from va2 in va1.DefaultIfEmpty()
                join nv7 in this.unitOfWork.DbContext.Set<NomValue>() on va2.ApplicationTypeId equals nv7.NomValueId into nv8
                from nv9 in nv8.DefaultIfEmpty()

                join p in this.unitOfWork.DbContext.Set<GvaViewPerson>() on va2.LotId equals p.LotId into p1
                from p2 in p1.DefaultIfEmpty()

                join o in this.unitOfWork.DbContext.Set<GvaViewOrganization>().Include(o => o.OrganizationType) on va2.LotId equals o.LotId into o1
                from o2 in o1.DefaultIfEmpty()
                join nv19 in this.unitOfWork.DbContext.Set<NomValue>() on o2.OrganizationTypeId equals nv19.NomValueId into nv20
                from nv21 in nv20.DefaultIfEmpty()

                join ac in this.unitOfWork.DbContext.Set<GvaViewAircraft>() on va2.LotId equals ac.LotId into ac1
                from ac2 in ac1.DefaultIfEmpty()
                join nv1 in this.unitOfWork.DbContext.Set<NomValue>() on ac2.AirCategoryId equals nv1.NomValueId into nv2
                from nv3 in nv2.DefaultIfEmpty()
                join nv4 in this.unitOfWork.DbContext.Set<NomValue>() on ac2.AircraftProducerId equals nv4.NomValueId into nv5
                from nv6 in nv5.DefaultIfEmpty()

                join ap in this.unitOfWork.DbContext.Set<GvaViewAirport>() on va2.LotId equals ap.LotId into ap1
                from ap2 in ap1.DefaultIfEmpty()
                join nv16 in this.unitOfWork.DbContext.Set<NomValue>() on ap2.AirportTypeId equals nv16.NomValueId into nv17
                from nv18 in nv17.DefaultIfEmpty()

                join eq in this.unitOfWork.DbContext.Set<GvaViewEquipment>() on va2.LotId equals eq.LotId into eq1
                from eq2 in eq1.DefaultIfEmpty()
                join nv10 in this.unitOfWork.DbContext.Set<NomValue>() on eq2.EquipmentTypeId equals nv10.NomValueId into nv11
                from nv12 in nv11.DefaultIfEmpty()
                join nv13 in this.unitOfWork.DbContext.Set<NomValue>() on eq2.EquipmentProducerId equals nv13.NomValueId into nv14
                from nv15 in nv14.DefaultIfEmpty()
                select new
                {
                    GApplication = a,
                    Set = set,
                    GApplicationPart = part2,
                    GViewApplication = va2,
                    GViewApplicationType = nv9,
                    GViewPerson = p2,
                    GViewOrganization = o2,
                    GViewOrganizationType = nv21,
                    GViewAircraft = ac2,
                    GViewAircraftCat = nv3,
                    GViewAircraftProd = nv6,
                    GViewAirport = ap2,
                    GViewAirportType = nv18,
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
                GViewPerson = new GvaViewPerson(),
                GViewOrganization = new GvaViewOrganization(),
                GViewOrganizationType = new NomValue(),
                GViewAircraft = new GvaViewAircraft(),
                GViewAircraftCat = new NomValue(),
                GViewAircraftProd = new NomValue(),
                GViewAirport = new GvaViewAirport(),
                GViewAirportType = new NomValue(),
                GViewEquipment = new GvaViewEquipment(),
                GViewEquipmentType = new NomValue(),
                GViewEquipmentProducer = new NomValue()
            });

            predicate = predicate
                .AndDateTimeGreaterThanOrEqual(e => e.GViewApplication.DocumentDate, fromDate)
                .AndDateTimeLessThanOrEqual(e => e.GViewApplication.DocumentDate, toDate)
                .AndEquals(e => e.GViewPerson.Lin, personLin)
                .AndStringContains(e => e.GViewAircraft.ICAO, aircraftIcao)
                .AndStringContains(e => e.GViewOrganization.Uin, organizationUin)
                .AndStringMatches(e => e.Set.Alias, lotSetAlias, true);

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
                    AppPartIndex = e.GApplicationPart != null ? e.GApplicationPart.Index : (int?)null,
                    AppPartDocumentDate = e.GViewApplication != null ? e.GViewApplication.DocumentDate : null,
                    AppPartDocumentNumber = e.GViewApplication != null ? e.GViewApplication.DocumentNumber : null,
                    AppPartApplicationTypeName = e.GViewApplicationType != null ? e.GViewApplicationType.Name : null,
                    PersonId = e.GViewPerson != null ? (int?)e.GViewPerson.LotId : null,
                    PersonLin = e.GViewPerson != null ? (int?)e.GViewPerson.Lin : null,
                    PersonNames =  e.GViewPerson != null ? e.GViewPerson.Names : null,
                    GvaOrganizationId = e.GViewOrganization != null ? (int?)e.GViewOrganization.LotId : null,
                    GvaOrganizationName = e.GViewOrganization != null ? e.GViewOrganization.Name : null,
                    GvaOrganizationUin = e.GViewOrganization != null ? e.GViewOrganization.Uin : null,
                    GvaAircraftId = e.GViewAircraft != null ? (int?)e.GViewAircraft.LotId : null,
                    GvaAirCategory = e.GViewAircraftCat != null ? e.GViewAircraftCat.Name : null,
                    GvaAircraftProducer = e.GViewAircraftProd != null ? e.GViewAircraftProd.Name : null,
                    GvaAircraftICAO = e.GViewAircraft != null ? e.GViewAircraft.ICAO : null,
                    GvaAirportId = e.GViewAirport != null ? (int?)e.GViewAirport.LotId : null,
                    GvaAirportType = e.GViewAirportType != null ? e.GViewAirportType.Name : null,
                    GvaAirportName = e.GViewAirport != null ? e.GViewAirport.Name : null,
                    GvaEquipmentId = e.GViewEquipment != null ? (int?)e.GViewEquipment.LotId : null,
                    GvaEquipmentName = e.GViewEquipment != null ? e.GViewEquipment.Name : null,
                    GvaEquipmentType = e.GViewEquipmentType != null ? e.GViewEquipmentType.Name : null,
                    GvaEquipmentProducer = e.GViewEquipmentProducer != null ? e.GViewEquipmentProducer.Name : null
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

        public void AddApplicationRefs(Part part, IEnumerable<ApplicationNomDO> applications)
        {
            var lotObjects = this.unitOfWork.DbContext.Set<GvaLotObject>()
                .Where(lo => lo.LotPartId == part.PartId)
                .ToList();
            foreach (var lotObject in lotObjects)
            {
                this.unitOfWork.DbContext.Set<GvaLotObject>().Remove(lotObject);
            }

            foreach (var application in applications)
            {
                GvaLotObject lotObject = new GvaLotObject()
                {
                    GvaApplicationId = application.ApplicationId,
                    LotPart = part
                };
                this.unitOfWork.DbContext.Set<GvaLotObject>().Add(lotObject);
            }
        }

        public GvaApplication[] GetApplicationRefs(int partId)
        {
            return this.unitOfWork.DbContext.Set<GvaLotObject>()
                .Include(lo => lo.GvaApplication)
                .Include(lo => lo.GvaApplication.GvaAppLotPart)
                .Where(lo => lo.LotPartId == partId)
                .Select(ga => ga.GvaApplication)
                .ToArray();
        }

        public void DeleteApplicationRefs(Part part)
        {
            var lotObjects = this.unitOfWork.DbContext.Set<GvaLotObject>()
                .Where(f => f.LotPartId == part.PartId)
                .ToList();

            foreach (var lotObject in lotObjects)
            {
                this.unitOfWork.DbContext.Set<GvaLotObject>().Remove(lotObject);
            }
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
    }
}
