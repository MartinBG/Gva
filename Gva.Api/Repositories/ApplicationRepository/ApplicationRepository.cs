﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Api.Repositories;
using Common.Data;
using Common.Linq;
using Gva.Api.Models;
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
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string lin = null,
            int offset = 0,
            int? limit = null
            )
        {
            var applicationsJoin =
                from a in this.unitOfWork.DbContext.Set<GvaApplication>()
                join part in this.unitOfWork.DbContext.Set<Part>() on a.GvaAppLotPartId equals part.PartId into part1
                from part2 in part1.DefaultIfEmpty()
                join va in this.unitOfWork.DbContext.Set<GvaViewApplication>() on a.GvaAppLotPartId equals va.PartId into va1
                from va2 in va1.DefaultIfEmpty()
                join p in this.unitOfWork.DbContext.Set<GvaViewPerson>() on va2.LotId equals p.LotId into p1
                from p2 in p1.DefaultIfEmpty()
                join o in this.unitOfWork.DbContext.Set<GvaViewOrganization>() on va2.LotId equals o.LotId into o1
                from o2 in o1.DefaultIfEmpty()
                select new
                {
                    GApplication = a,
                    GApplicationPart = part2,
                    GViewApplication = va2,
                    GViewPerson = p2,
                    GViewOrganization = o2
                };

            var predicate = PredicateBuilder.True(new
            {
                GApplication = new GvaApplication(),
                GApplicationPart = new Part(),
                GViewApplication = new GvaViewApplication(),
                GViewPerson = new GvaViewPerson(),
                GViewOrganization = new GvaViewOrganization()
            });

            predicate = predicate
                .AndDateTimeGreaterThanOrEqual(e => e.GViewApplication.RequestDate, fromDate)
                .AndDateTimeLessThanOrEqual(e => e.GViewApplication.RequestDate, toDate)
                .AndStringContains(e => e.GViewPerson.Lin, lin);

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
                    AppPartId = e.GApplication.GvaAppLotPartId,
                    AppPartIndex = e.GApplicationPart != null ? e.GApplicationPart.Index : null,
                    AppPartRequestDate = e.GViewApplication != null ? e.GViewApplication.RequestDate : null,
                    AppPartDocumentNumber = e.GViewApplication != null ? e.GViewApplication.DocumentNumber : null,
                    AppPartApplicationTypeName = e.GViewApplication != null ? e.GViewApplication.ApplicationTypeName : null,
                    AppPartStatusName = e.GViewApplication != null ? e.GViewApplication.StatusName : null,
                    PersonId = e.GViewPerson != null ? (int?)e.GViewPerson.LotId : null,
                    PersonLin = e.GViewPerson != null ? e.GViewPerson.Lin : null,
                    PersonNames =  e.GViewPerson != null ? e.GViewPerson.Names : null,
                    GvaOrganizationId = e.GViewOrganization != null ? (int?)e.GViewOrganization.LotId : null,
                    GvaOrganizationName = e.GViewOrganization != null ? e.GViewOrganization.Name : null,
                    GvaOrganizationUin = e.GViewOrganization != null ? e.GViewOrganization.Uin : null
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
    }
}
