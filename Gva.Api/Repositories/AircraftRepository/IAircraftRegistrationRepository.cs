using System.Collections.Generic;
using Common.Api.Models;

namespace Gva.Api.Repositories.AircraftRepository
{
    public interface IAircraftRegistrationRepository
    {
        int? GetLastActNumber(int registerId);

        List<NomValue> GetAircraftRegistrationNoms(int lotId, string term = null);
    }
}
