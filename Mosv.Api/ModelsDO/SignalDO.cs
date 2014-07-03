using Mosv.Api.Models;
using System;

namespace Mosv.Api.ModelsDO
{
    public class SignalDO
    {
        public SignalDO()
        {
        }

        public SignalDO(MosvViewSignal signal)
        {
            this.Id = signal.LotId;
            this.ApplicationDocId = signal.ApplicationDocId;
            this.IncomingLot = signal.IncomingLot;
            this.IncomingNumber = signal.IncomingNumber;
            this.IncomingDate = signal.IncomingDate;
            this.Applicant = signal.Applicant;
            this.Institution = signal.Institution;
            this.Violation = signal.Violation;
        }

        public int Id { get; set; }

        public int? ApplicationDocId { get; set; }

        public string IncomingNumber { get; set; }

        public string IncomingLot { get; set; }

        public string Applicant { get; set; }

        public DateTime? IncomingDate { get; set; }

        public string Institution { get; set; }

        public string Violation { get; set; }

        public Docs.Api.DataObjects.DocRelationDO ApplicationDocRelation { get; set; }
    }
}