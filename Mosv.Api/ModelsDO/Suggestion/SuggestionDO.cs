using System;
using Common.Api.Models;

namespace Mosv.Api.ModelsDO.Suggestion
{
    public class SuggestionDO
    {
        public string Lot { get; set; }

        public string Number { get; set; }

        public DateTime? Date { get; set; }

        public NomValue SubmitType { get; set; }

        public string Applicant { get; set; }

        public NomValue Institution { get; set; }

        public string Character { get; set; }

        public NomValue Status { get; set; }

        public string Actions { get; set; }
    }
}
