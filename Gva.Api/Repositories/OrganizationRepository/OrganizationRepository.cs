using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models;
using System;
using Common.Linq;
using System.Data.Entity;
using Gva.Api.Models.Views.Organization;
using Common.Extensions;
using Newtonsoft.Json.Linq;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.FileRepository;

namespace Gva.Api.Repositories.OrganizationRepository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private IUnitOfWork unitOfWork;
        private IFileRepository fileRepository;

        public OrganizationRepository(IUnitOfWork unitOfWork, IFileRepository fileRepository)
        {
            this.unitOfWork = unitOfWork;
            this.fileRepository = fileRepository;
        }

        public IEnumerable<GvaViewOrganization> GetOrganizations(
            string name,
            int? caseTypeId,
            string cao,
            string uin,
            DateTime? dateValidTo,
            DateTime? dateCaoValidTo,
            bool exact,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<GvaViewOrganization>()
                .AndStringMatches(o => o.Cao, cao, exact)
                .AndStringMatches(o => o.Uin, uin, exact)
                .AndStringMatches(o => o.Name, name, exact);

            if (dateValidTo.HasValue)
            {
                var maxDate = dateValidTo.Value.Date.AddDays(1);
                predicate = predicate.And(o => o.DateValidTo < maxDate);
            }

            if (dateCaoValidTo.HasValue)
            {
                var maxDate = dateCaoValidTo.Value.Date.AddDays(1);
                predicate = predicate.And(o => o.DateCaoValidTo < maxDate);
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

        public IEnumerable<GvaViewOrganizationRecommendation> GetInspectionRecommendations(int lotId, int inspectionPartIndex)
        {
            return (from r in this.unitOfWork.DbContext.Set<GvaViewOrganizationRecommendation>()
                   join ir in this.unitOfWork.DbContext.Set<GvaViewOrganizationInspectionRecommendation>() on r.PartIndex equals ir.RecommendationPartIndex
                   where ir.InspectionPartIndex == inspectionPartIndex && ir.LotId == lotId
                   select r)
                   .ToList();
        }

        public IEnumerable<GvaViewOrganizationRecommendation> GetRecommendations(int lotId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewOrganizationRecommendation>().Where(r => r.LotId == lotId);
        }

        public IEnumerable<GvaViewOrganizationApproval> GetApprovals(int lotId, int? caseTypeId)
        {
            var approvals = this.unitOfWork.DbContext.Set<GvaViewOrganizationApproval>()
                .Include(a => a.ApprovalState)
                .Include(a => a.ApprovalType)
                .Include(a => a.Amendments)
                .Where(r => r.LotId == lotId)
                .ToList();

            if (caseTypeId.HasValue)
            {
                return (from r in approvals
                        join f in this.unitOfWork.DbContext.Set<GvaLotFile>().Include(f => f.GvaCaseType) on r.PartId equals f.LotPartId
                        where f.GvaCaseTypeId == caseTypeId.Value
                        select r);
            }
            else
            {
                return approvals;
            }
        }

        public int GetLastApprovalAmendmentIndex(int lotId, int approvalPartIndex)
        {
            return this.unitOfWork.DbContext.Set<GvaViewOrganizationAmendment>()
                .Where(e => e.LotId == lotId && e.ApprovalPartIndex == approvalPartIndex)
                .OrderByDescending(a => a.Index)
                .First()
                .PartIndex;
        }
    }
}
