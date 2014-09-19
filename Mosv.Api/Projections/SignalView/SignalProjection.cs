using System.Collections.Generic;
using Common.Data;
using Mosv.Api.Models;
using Mosv.Api.ModelsDO.Signal;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Mosv.Api.Projections.SignalView
{
    public class SignalProjection : Projection<MosvViewSignal>
    {
        public SignalProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Signal")
        { }

        public override IEnumerable<MosvViewSignal> Execute(PartCollection parts)
        {
            var signalData = parts.Get<SignalDO>("signalData");

            if (signalData == null)
            {
                return new MosvViewSignal[] { };
            }

            return new[] { this.Create(signalData) };
        }

        private MosvViewSignal Create(PartVersion<SignalDO> signalData)
        {
            MosvViewSignal signal = new MosvViewSignal();

            signal.LotId = signalData.Part.Lot.LotId;
            signal.IncomingLot = signalData.Content.IncomingLot;
            signal.IncomingNumber = signalData.Content.Number;
            signal.IncomingDate = signalData.Content.Date;
            signal.Applicant = signalData.Content.Applicant;
            signal.Institution = signalData.Content.Institution == null ? null : signalData.Content.Institution.Name;
            signal.Violation = signalData.Content.Violation;

            return signal;
        }
    }
}
