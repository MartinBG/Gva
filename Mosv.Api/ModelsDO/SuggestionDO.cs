using System;
using Mosv.Api.Models;

namespace Mosv.Api.ModelsDO
{
    public class SuggestionDO
    {
        public SuggestionDO(MosvViewSuggestion suggestion)
        {
            this.Id = suggestion.LotId;
            this.IncomingLot = suggestion.IncomingLot;
            this.IncomingNumber = suggestion.IncomingNumber;
            this.IncomingDate = suggestion.IncomingDate.HasValue ? suggestion.IncomingDate.Value.ToString("dd.MM.yyyy") : null;
            this.Applicant = suggestion.Applicant;
        }

        public int Id { get; set; }

        public string IncomingNumber { get; set; }

        public string IncomingLot { get; set; }

        public string Applicant { get; set; }

        public string IncomingDate { get; set; }
    }
}
