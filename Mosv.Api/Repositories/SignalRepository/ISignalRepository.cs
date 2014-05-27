using System.Collections.Generic;
using Mosv.Api.Models;
using System;

namespace Mosv.Api.Repositories.SignalRepository
{
    public interface ISignalRepository
    {
        IEnumerable<MosvViewSignal> GetSignals(
            string incomingLot,
            string incomingNumber,
            DateTime? incomingDate,
            string applicant,
            string institution,
            string violation,
            bool exact = false,
            int offset = 0,
            int? limit = null);
    }
}