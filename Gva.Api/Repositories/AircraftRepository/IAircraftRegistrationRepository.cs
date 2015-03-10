using System.Collections.Generic;
using Common.Api.Models;
using Gva.Api.Models.Views.Aircraft;

namespace Gva.Api.Repositories.AircraftRepository
{
    public interface IAircraftRegistrationRepository
    {
        int? GetLastActNumber(int? registerId = null, string alias = null);

        List<NomValue> GetAircraftRegistrationNoms(int lotId, string term = null);

        List<GvaViewAircraftRegistration> GetAircraftsRegistrations(string regMark = null, int? registerId = null, int? certNumber = null, int? actNumber = null);
    }
}
