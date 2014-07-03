using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Mosv.Api.Models;
using System;
using Common.Linq;
using System.Data.Entity;

namespace Mosv.Api.Repositories.SignalRepository
{
    public class SignalRepository : ISignalRepository
    {
        private IUnitOfWork unitOfWork;

        public SignalRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<MosvViewSignal> GetSignals(
            string incomingLot,
            string incomingNumber,
            DateTime? incomingDate,
            string applicant,
            string institution,
            string violation,
            bool exact,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<MosvViewSignal>()
                .AndStringMatches(a => a.IncomingLot, incomingLot, exact)
                .AndStringMatches(o => o.IncomingNumber, incomingNumber, exact)
                .AndDateTimeGreaterThanOrEqual(o => o.IncomingDate, incomingDate)
                .AndStringMatches(o => o.Applicant, applicant, exact)
                .AndStringMatches(o => o.Institution, institution, exact)
                .AndStringMatches(o => o.Violation, violation, exact);

            return this.unitOfWork.DbContext.Set<MosvViewSignal>()
                .Where(predicate)
                .OrderBy(o => o.IncomingLot)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public MosvViewSignal GetSignal(int singalId)
        {
            return this.unitOfWork.DbContext.Set<MosvViewSignal>()
                .SingleOrDefault(p => p.LotId == singalId);
        }
    }
}
