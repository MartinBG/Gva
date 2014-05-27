using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Mosv.Api.Models;
using System;
using Common.Linq;
using System.Data.Entity;

namespace Mosv.Api.Repositories.AdmissionRepository
{
    public class AdmissionRepository : IAdmissionRepository
    {
        private IUnitOfWork unitOfWork;

        public AdmissionRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<MosvViewAdmission> GetAdmissions(
            string incomingLot,
            string incomingNumber,
            DateTime? incomingDate,
            string applicantType,
            string applicant,
            bool exact,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<MosvViewAdmission>()
                .AndStringMatches(a => a.IncomingLot, incomingLot, exact)
                .AndStringMatches(o => o.IncomingNumber, incomingNumber, exact)
                .AndDateTimeGreaterThanOrEqual(o => o.IncomingDate, incomingDate)
                .AndStringMatches(o => o.ApplicantType, applicantType, exact)
                .AndStringMatches(o => o.Applicant, applicant, exact);

            return this.unitOfWork.DbContext.Set<MosvViewAdmission>()
                .Where(predicate)
                .OrderBy(o => o.IncomingLot)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public MosvViewAdmission GetAdmission(int admissionId)
        {
            return this.unitOfWork.DbContext.Set<MosvViewAdmission>()
                .SingleOrDefault(p => p.LotId == admissionId);
        }
    }
}