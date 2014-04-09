using System.Collections.Generic;
using Gva.Api.Models;

namespace Gva.Api.Repositories.AirportRepository
{
    public interface IAirportRepository
    {
        IEnumerable<GvaViewAirport> GetAirports(
            string name = null,
            string icao = null,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        GvaViewAirport GetAirport(int airportId);
    }
}
