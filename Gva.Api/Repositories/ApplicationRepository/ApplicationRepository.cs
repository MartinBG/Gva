using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models;
using System;

namespace Gva.Api.Repositories.ApplicationRepository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private IUnitOfWork unitOfWork;

        public ApplicationRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GvaApplication> GetApplications(DateTime? fromDate, DateTime? toDate, string lin, string regUri)
        {
            var applications = this.unitOfWork.DbContext.Set<GvaApplication>().AsQueryable();

            if (fromDate.HasValue)
            {
                applications = applications.Where(p => p.Doc.RegDate.HasValue && p.Doc.RegDate.Value >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                applications = applications.Where(p => p.Doc.RegDate.HasValue && p.Doc.RegDate.Value <= toDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(regUri))
            {
                applications = applications.Where(p => p.Doc.RegUri.Contains(regUri));
            }

            if (!string.IsNullOrWhiteSpace(lin))
            {
                //todo right way?
                var persons = this.unitOfWork.DbContext.Set<GvaPerson>().Where(e => e.Lin == lin).ToList();

                applications = applications.Where(e => persons.Any(a => a.GvaPersonLotId == e.LotId));
            }

            return applications;
        }

        public GvaApplication GetApplication(int applicationId)
        {
            return this.unitOfWork.DbContext.Set<GvaApplication>()
                .SingleOrDefault(p => p.GvaApplicationId == applicationId);
        }

        public void AddApplication(GvaApplication application)
        {
            this.unitOfWork.DbContext.Set<GvaApplication>().Add(application);
        }
    }
}
