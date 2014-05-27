using System;
using Common.Data;
using Common.Json;
using Mosv.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Mosv.Api.LotEventHandlers.SignalView
{
    public class SignalDataHandler : CommitEventHandler<MosvViewSignal>
    {
        public SignalDataHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Signal",
                setPartAlias: "signalData",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId)
        {
        }

        public override void Fill(MosvViewSignal admission, PartVersion part)
        {
            admission.Lot = part.Part.Lot;

            admission.IncomingLot = part.Content.Get<string>("incomingLot");
            admission.IncomingDate = part.Content.Get<DateTime?>("date");
            admission.IncomingNumber = part.Content.Get<string>("number");
            admission.Applicant = part.Content.Get<string>("applicant");
            admission.Institution = part.Content.Get<string>("institution.name");
            admission.Violation = part.Content.Get<string>("violation");
        }

        public override void Clear(MosvViewSignal admission)
        {
            throw new NotSupportedException();
        }
    }
}
