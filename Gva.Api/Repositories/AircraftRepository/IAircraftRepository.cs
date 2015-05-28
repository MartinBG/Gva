using System;
using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.Models.Views.Aircraft;

namespace Gva.Api.Repositories.AircraftRepository
{
    public interface IAircraftRepository
    {
        IEnumerable<Tuple<GvaViewAircraft, GvaViewAircraftRegistration>> GetAircrafts(
            string mark = null,
            string manSN = null,
            string modelAlt = null,
            string icao = null,
            string airCategory = null,
            string aircraftProducer = null,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        Tuple<GvaViewAircraft, GvaViewAircraftRegistration> GetAircraft(int aircraftId);

        IEnumerable<GvaInvalidActNumber> GetInvalidActNumbers(int? actNumber = null, int? registerId = null);

        bool DevalidateActNumber(int actNumber, string reason);

        bool IsUniqueMSN(string msn, int? aircraftId = null);

        int? GetLastNumberPerForm(int? formPrefix = null, string formName = null);

        bool IsUniqueFormNumber(string formName, string number, int lotId, int? partIndex = null);
    }
}
