using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Linq;
using Gva.Api.Models;
using System.Data.Entity;
using Gva.Api.ModelsDO;
using Regs.Api.Models;

namespace Gva.Api.Repositories.AircraftRepository
{
    public class AircraftRegistrationRepository : IAircraftRegistrationRepository
    {
        private IUnitOfWork unitOfWork;

        public AircraftRegistrationRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int? GetLastActNumber(int registerId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewAircraftRegistration>()
                .Where(v => v.CertRegisterId == registerId)
                .Max(v => (int?)v.ActNumber);
        }
    }
}
