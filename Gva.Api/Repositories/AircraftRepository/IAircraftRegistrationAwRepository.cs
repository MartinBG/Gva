using System.Collections.Generic;
using Gva.Api.Models;

namespace Gva.Api.Repositories.AircraftRepository
{
    public interface IAircraftRegistrationAwRepository
    {
        GvaViewAircraftAw GetLastAw(int lotId);
    }
}
