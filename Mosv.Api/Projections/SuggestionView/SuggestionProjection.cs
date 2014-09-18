using System.Collections.Generic;
using Common.Data;
using Mosv.Api.Models;
using Mosv.Api.ModelsDO.Suggestion;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Mosv.Api.Projections.SuggestionView
{
    public class SuggestionProjection : Projection<MosvViewSuggestion>
    {
        public SuggestionProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Suggestion")
        { }

        public override IEnumerable<MosvViewSuggestion> Execute(PartCollection parts)
        {
            var suggestionData = parts.Get<SuggestionDO>("suggestionData");

            if (suggestionData == null)
            {
                return new MosvViewSuggestion[] { };
            }

            return new[] { this.Create(suggestionData) };
        }

        private MosvViewSuggestion Create(PartVersion<SuggestionDO> suggestionData)
        {
            MosvViewSuggestion suggestion = new MosvViewSuggestion();

            suggestion.LotId = suggestionData.Part.Lot.LotId;
            suggestion.IncomingLot = suggestionData.Content.Lot;
            suggestion.IncomingNumber = suggestionData.Content.Number;
            suggestion.IncomingDate = suggestionData.Content.Date;
            suggestion.Applicant = suggestionData.Content.Applicant;

            return suggestion;
        }
    }
}
