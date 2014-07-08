using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Data;
using Common.Linq;
using Gva.Api.Models.Views.Airport;

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
            var gvaAirports = this.unitOfWork.DbContext.Set<GvaViewAirport>()
                .Include(a => a.AirportType);

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
                .Include(a => a.AirportType)
                .SingleOrDefault(p => p.LotId == airportId);
        }
    }
}
