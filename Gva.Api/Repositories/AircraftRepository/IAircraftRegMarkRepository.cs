using System.Collections.Generic;
using Gva.Api.Models;

namespace Gva.Api.Repositories.AircraftRepository
{
    public interface IAircraftRegMarkRepository
    {
        bool RegMarkIsValid(int lotId, string regMark);
    }
}
