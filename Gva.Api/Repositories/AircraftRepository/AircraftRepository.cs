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
    public class AircraftRepository : IAircraftRepository
    {
        private IUnitOfWork unitOfWork;

        public AircraftRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GvaViewAircraft> GetAircrafts(string manSN,
            string model,
            string icao,
            string category,
            string producer,
            bool exact,
            int offset = 0,
            int? limit = null)
        {
            var gvaAircrafts =
                this.unitOfWork.DbContext.Set<GvaViewAircraft>()
                .Include(a => a.AircraftCategory)
                .Include(a => a.AircraftProducer);

            var predicate = PredicateBuilder.True<GvaViewAircraft>();

            predicate = predicate
                .AndStringMatches(p => p.ManSN, manSN, exact)
                .AndStringMatches(p => p.Model, model, exact)
                .AndStringMatches(p => p.ICAO, icao, exact)
                .AndStringMatches(p => p.AircraftCategory.Name, category, exact)
                .AndStringMatches(p => p.AircraftProducer.Name, producer, exact);

            return gvaAircrafts
                .Where(predicate)
                .OrderBy(p => p.Model)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }
        public GvaViewAircraft GetAircraft(int aircraftId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewAircraft>()
                .Include(a => a.AircraftCategory)
                .Include(a => a.AircraftProducer)
                .SingleOrDefault(p => p.LotId == aircraftId);
        }

        public bool IsUniqueMSN(string msn, int? aircraftId = null)
        {
            if (aircraftId != null)
            {
                return !this.unitOfWork.DbContext.Set<GvaViewAircraft>()
                    .Where(p => p.ManSN == msn && p.LotId != aircraftId).Any();
            }
            else
            {
                return !this.unitOfWork.DbContext.Set<GvaViewAircraft>()
                    .Where(p => p.ManSN == msn).Any();
            }
        }
    }
}
