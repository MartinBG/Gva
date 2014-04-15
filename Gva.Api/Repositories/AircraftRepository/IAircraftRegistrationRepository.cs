using System.Collections.Generic;
using Gva.Api.Models;

namespace Gva.Api.Repositories.AircraftRepository
{
    public interface IAircraftRegistrationRepository
    {
        IEnumerable<GvaViewAircraftRegistration> GetRegistrations(int? aircraftId = null);

        GvaViewAircraftRegistration GetRegistration(int registrationId);

        int? GetLastCertNumber(int registerId);
    }
}
