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
    public class AircraftRegMarkRepository : IAircraftRegMarkRepository
    {
        private IUnitOfWork unitOfWork;

        public AircraftRegMarkRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool RegMarkIsValid(int lotId, string regMark)
        {
            bool hasReservedRegMark =
                this.unitOfWork.DbContext.Set<GvaViewAircraftRegMark>()
                .Where(v => v.LotId != lotId && v.RegMark == regMark)
                .Any();

            bool hasRegMarkInUse =
                this.unitOfWork.DbContext.Set<GvaViewAircraftRegistration>()
                .Where(v => v.LotId != lotId && v.RegMark == regMark)
                .Any();

            return !hasReservedRegMark && !hasRegMarkInUse;
        }
    }
}
