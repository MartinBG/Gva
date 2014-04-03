using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models;
using System;
using Common.Linq;

namespace Gva.Api.Repositories.OrganizationRepository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private IUnitOfWork unitOfWork;

        public OrganizationRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GvaViewOrganization> GetOrganizations(
            string name,
            string CAO,
            string uin,
            DateTime? dateValidTo,
            DateTime? dateCAOValidTo,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<GvaViewOrganization>()
                .AndStringContains(o => o.CAO, CAO)
                .AndStringContains(o => o.Uin, uin)
                .AndStringContains(o => o.Name, name);

            if (dateValidTo != null)
            {
                var maxDate = dateValidTo.Value.Date.AddDays(1);
                predicate = predicate.And(o => o.DateValidTo < maxDate);
            }

            if (dateCAOValidTo != null)
            {
                var maxDate = dateCAOValidTo.Value.Date.AddDays(1);
                predicate = predicate.And(o => o.DateCAOValidTo < maxDate);
            }

            return this.unitOfWork.DbContext.Set<GvaViewOrganization>()
                .Where(predicate)
                .OrderBy(o => o.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public GvaViewOrganization GetOrganization(int organizationId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewOrganization>()
                .SingleOrDefault(p => p.GvaViewOrganizationLotId == organizationId);
        }

        public void AddOrganization(GvaViewOrganization organization)
        {
            this.unitOfWork.DbContext.Set<GvaViewOrganization>().Add(organization);
        }
    }
}
