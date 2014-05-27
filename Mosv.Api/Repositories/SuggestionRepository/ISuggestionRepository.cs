using System;
using System.Collections.Generic;
using Mosv.Api.Models;

namespace Mosv.Api.Repositories.SuggestionRepository
{
    public interface ISuggestionRepository
    {
        IEnumerable<MosvViewSuggestion> GetSuggestions(
            string incomingLot,
            string incomingNumber,
            DateTime? incomingDateFrom,
            DateTime? incomingDateТо,
            string applicant,
            bool exact = false,
            int offset = 0,
            int? limit = null);
    }
}