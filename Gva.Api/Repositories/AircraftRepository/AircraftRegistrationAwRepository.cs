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
    public class AircraftRegistrationAwRepository : IAircraftRegistrationAwRepository
    {
        private IUnitOfWork unitOfWork;

        public AircraftRegistrationAwRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public GvaViewAircraftAw GetLastAw(int lotId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewAircraftAw>()
                .Where(aw => aw.LotId == lotId)
                .OrderByDescending(aw => aw.ValidFromDate).FirstOrDefault();
        }
    }
}
