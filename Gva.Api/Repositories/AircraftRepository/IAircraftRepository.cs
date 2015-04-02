using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.Models.Views.Aircraft;

namespace Gva.Api.Repositories.AircraftRepository
{
    public interface IAircraftRepository
    {
        IEnumerable<GvaViewAircraft> GetAircrafts(
            string mark,
            string manSN,
            string modelAlt,
            string icao,
            string airCategory,
            string aircraftProducer,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        GvaViewAircraft GetAircraft(int aircraftId);

        IEnumerable<GvaViewAircraft> GetAircraftModels(
            string airCategory,
            string aircraftProducer,
            int offset = 0,
            int? limit = null);

        IEnumerable<GvaInvalidActNumber> GetInvalidActNumbers();

        bool DevalidateActNumber(int actNumber, string reason);

        GvaViewAircraft GetAircraftModel(int aircraftId);

        bool IsUniqueMSN(string msn, int? aircraftId = null);
    }
}
