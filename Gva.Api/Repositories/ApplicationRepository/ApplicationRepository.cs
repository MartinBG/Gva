using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models;
using System;
using System.Data.Entity;
using Gva.Api.ModelsDO;

namespace Gva.Api.Repositories.ApplicationRepository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private IUnitOfWork unitOfWork;

        public ApplicationRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<ApplicationListDO> GetApplications(DateTime? fromDate, DateTime? toDate, string lin)
        {
            var applications = this.unitOfWork.DbContext.Set<GvaApplication>().AsQueryable()
                .Join(this.unitOfWork.DbContext.Set<GvaApplicationSearch>().AsQueryable(), ga => ga.GvaAppLotPartId, gas => gas.LotPartId, (ga, gas) => new { GApplication = ga, GApplicationSearch = gas })
                .Join(this.unitOfWork.DbContext.Set<GvaPerson>().AsQueryable(), ga => ga.GApplication.LotId, gp => gp.GvaPersonLotId, (ga, gp) => new { GApplication = ga.GApplication, GApplicationSearch = ga.GApplicationSearch, GPerson = gp });

            if (fromDate.HasValue)
            {
                applications = applications.Where(e => e.GApplicationSearch.RequestDate.HasValue && e.GApplicationSearch.RequestDate.Value >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                applications = applications.Where(e => e.GApplicationSearch.RequestDate.HasValue && e.GApplicationSearch.RequestDate.Value <= toDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(lin))
            {
                applications = applications.Where(e => e.GPerson.Lin == lin);
            }

            return applications.Select(e => new ApplicationListDO()
            {
                ApplicationId = e.GApplication.GvaApplicationId,
                DocId = e.GApplication.DocId,
                AppPartId = e.GApplication.GvaAppLotPartId,
                AppPartIndex = e.GApplication.GvaAppLotPart.Index,
                AppPartRequestDate = e.GApplicationSearch.RequestDate,
                AppPartDocumentNumber = e.GApplicationSearch.DocumentNumber,
                AppPartApplicationTypeName = e.GApplicationSearch.ApplicationTypeName,
                AppPartStatusName = e.GApplicationSearch.StatusName,
                PersonId = e.GApplication.LotId,
                PersonLin = e.GPerson.Lin,
                PersonNames = e.GPerson.Names
            });
        }

        public GvaApplication[] GetNomApplications(int lotId)
        {
            return this.unitOfWork.DbContext.Set<GvaApplication>()
                .Include(a => a.GvaAppLotPart)
                .Where(a => a.LotId == lotId)
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

        public GvaApplicationSearch GetGvaApplicationSearch(int lotPartId)
        {
            return this.unitOfWork.DbContext.Set<GvaApplicationSearch>()
                .SingleOrDefault(e => e.LotPartId == lotPartId);
        }

        public void AddGvaApplicationSearch(GvaApplicationSearch gvaApplicationSearch)
        {
            this.unitOfWork.DbContext.Set<GvaApplicationSearch>().Add(gvaApplicationSearch);
        }

        public void DeleteGvaApplicationSearch(int lotPartId)
        {
            GvaApplicationSearch gvaApplicationSearch = this.unitOfWork.DbContext.Set<GvaApplicationSearch>()
                .SingleOrDefault(e => e.LotPartId == lotPartId);

            if (gvaApplicationSearch != null)
            {
                this.unitOfWork.DbContext.Set<GvaApplicationSearch>().Remove(gvaApplicationSearch);
            }
        }
    }
}
