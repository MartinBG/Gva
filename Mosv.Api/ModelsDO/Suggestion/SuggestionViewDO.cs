using System;
using Mosv.Api.Models;

namespace Mosv.Api.ModelsDO.Suggestion
{
    public class SuggestionViewDO
    {
        public SuggestionViewDO()
        {
        }

        public SuggestionViewDO(MosvViewSuggestion suggestion)
        {
            this.Id = suggestion.LotId;
            this.ApplicationDocId = suggestion.ApplicationDocId;
            this.IncomingLot = suggestion.IncomingLot;
            this.IncomingNumber = suggestion.IncomingNumber;
            this.IncomingDate = suggestion.IncomingDate.HasValue ? suggestion.IncomingDate.Value.ToString("dd.MM.yyyy") : null;
            this.Applicant = suggestion.Applicant;
        }

        public int Id { get; set; }

        public int? ApplicationDocId { get; set; }

        public string IncomingNumber { get; set; }

        public string IncomingLot { get; set; }

        public string Applicant { get; set; }

        public string IncomingDate { get; set; }

        public Docs.Api.DataObjects.DocRelationDO ApplicationDocRelation { get; set; }
    }
}
