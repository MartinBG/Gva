using System.Collections.Generic;
using Gva.Api.Models;

namespace Gva.Api.Repositories.AircraftRepository
{
    public interface IAircraftRepository
    {
        IEnumerable<GvaViewAircraft> GetAircrafts(
            string mark,
            string manSN,
            string model,
            string airCategory,
            string aircraftProducer,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        GvaViewAircraft GetAircraft(int aircraftId);

        bool IsUniqueMSN(string msn, int? aircraftId = null);
    }
}
