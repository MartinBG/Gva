using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models;
using System;
using Common.Linq;
using System.Data.Entity;
using Gva.Api.Models.Views.Organization;

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
            int? caseTypeId,
            string CAO,
            string uin,
            DateTime? dateValidTo,
            DateTime? dateCAOValidTo,
            bool exact,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<GvaViewOrganization>()
                .AndStringMatches(o => o.CAO, CAO, exact)
                .AndStringMatches(o => o.Uin, uin, exact)
                .AndStringMatches(o => o.Name, name, exact);

            if (dateValidTo.HasValue)
            {
                var maxDate = dateValidTo.Value.Date.AddDays(1);
                predicate = predicate.And(o => o.DateValidTo < maxDate);
            }

            if (dateCAOValidTo.HasValue)
            {
                var maxDate = dateCAOValidTo.Value.Date.AddDays(1);
                predicate = predicate.And(o => o.DateCAOValidTo < maxDate);
            }

            if (caseTypeId.HasValue)
            {
                predicate = predicate.AndCollectionContains(o => o.GvaLotCases.Select(c => c.GvaCaseTypeId), caseTypeId.Value);
            }

            return this.unitOfWork.DbContext.Set<GvaViewOrganization>()
                .Include(o => o.GvaLotCases)
                .Include(o => o.OrganizationType)
                .Where(predicate)
                .OrderBy(o => o.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public GvaViewOrganization GetOrganization(int organizationId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewOrganization>()
                .Include(o => o.OrganizationType)
                .SingleOrDefault(p => p.LotId == organizationId);
        }
    }
}
