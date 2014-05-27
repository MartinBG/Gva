using System;
using Common.Data;
using Common.Json;
using Mosv.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Mosv.Api.LotEventHandlers.SuggestionView
{
    public class SuggestionDataHandler : CommitEventHandler<MosvViewSuggestion>
    {
        public SuggestionDataHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Suggestion",
                setPartAlias: "suggestionData",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId)
        {
        }

        public override void Fill(MosvViewSuggestion suggestion, PartVersion part)
        {
            suggestion.Lot = part.Part.Lot;

            suggestion.IncomingLot = part.Content.Get<string>("lot");
            suggestion.IncomingDate = part.Content.Get<DateTime?>("date");
            suggestion.IncomingNumber = part.Content.Get<string>("number");
            suggestion.Applicant = part.Content.Get<string>("applicant");
        }

        public override void Clear(MosvViewSuggestion suggestion)
        {
            throw new NotSupportedException();
        }
    }
}
