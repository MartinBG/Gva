using System.Collections.Generic;
using Gva.Api.Models;

namespace Gva.Api.Repositories.AircraftRepository
{
    public interface IAircraftRepository
    {
        IEnumerable<GvaViewAircraft> GetAircrafts(
            string manSN = null,
            string model = null,
            string icao = null,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        GvaViewAircraft GetAircraft(int aircraftId);

        bool IsUniqueMSN(string msn, int? aircraftId = null);
    }
}
