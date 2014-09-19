using System;
using Common.Api.Models;

namespace Mosv.Api.ModelsDO.Signal
{
    public class SignalDO
    {
        public string IncomingLot { get; set; }

        public string Number { get; set; }

        public DateTime? Date { get; set; }

        public NomValue SubmitType { get; set; }

        public string Applicant { get; set; }

        public NomValue Institution { get; set; }

        public string Violation { get; set; }

        public string ViolationPlace { get; set; }

        public string AffectedParts { get; set; }

        public string CheckTime { get; set; }

        public string Damages { get; set; }

        public string Precautions { get; set; }

        public NomValue Status { get; set; }

        public string Actions { get; set; }
    }
}
