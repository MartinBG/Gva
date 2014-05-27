using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Linq;
using Mosv.Api.Models;

namespace Mosv.Api.Repositories.SuggestionRepository
{
    public class SuggestionRepository : ISuggestionRepository
    {
        private IUnitOfWork unitOfWork;

        public SuggestionRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<MosvViewSuggestion> GetSuggestions(
            string incomingLot,
            string incomingNumber,
            DateTime? incomingDateFrom,
            DateTime? incomingDateТо,
            string applicant,
            bool exact = false,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<MosvViewSuggestion>()
                .AndStringMatches(s => s.IncomingLot, incomingLot, exact)
                .AndStringMatches(s => s.IncomingNumber, incomingNumber, exact)
                .AndDateTimeGreaterThanOrEqual(s => s.IncomingDate, incomingDateFrom)
                .AndDateTimeLessThanOrEqual(s => s.IncomingDate, incomingDateТо)
                .AndStringMatches(s => s.Applicant, applicant, exact);

            return this.unitOfWork.DbContext.Set<MosvViewSuggestion>()
                .Where(predicate)
                .OrderBy(s => s.IncomingLot)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }
    }
}
