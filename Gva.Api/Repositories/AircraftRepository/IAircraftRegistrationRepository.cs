using System.Collections.Generic;
using Gva.Api.Models;

namespace Gva.Api.Repositories.AircraftRepository
{
    public interface IAircraftRegistrationRepository
    {
        int? GetLastActNumber(int registerId);
    }
}
