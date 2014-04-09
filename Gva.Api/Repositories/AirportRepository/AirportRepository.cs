using System.Collections.Generic;

using System.Linq;
using Common.Data;
using Common.Linq;
using Gva.Api.Models;
using System.Data.Entity;
using Gva.Api.ModelsDO;
using Regs.Api.Models;

namespace Gva.Api.Repositories.AirportRepository
{
    public class AirportRepository : IAirportRepository
    {
        private IUnitOfWork unitOfWork;

        public AirportRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IEnumerable<GvaViewAirport> GetAirports(string name,
            string icao,
            bool exact,
            int offset = 0,
            int? limit = null)
        {
            var gvaAirports = this.unitOfWork.DbContext.Set<GvaViewAirport>();

            var predicate = PredicateBuilder.True<GvaViewAirport>();

            predicate = predicate
                .AndStringMatches(p => p.Name, name, exact)
                .AndStringMatches(p => p.ICAO, icao, exact);

            return gvaAirports
                .Where(predicate)
                .OrderBy(p => p.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }
        public GvaViewAirport GetAirport(int airportId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewAirport>()
                .SingleOrDefault(p => p.LotId == airportId);
        }
    }
}
