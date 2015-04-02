using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Data;
using Common.Linq;
using Gva.Api.Models;
using Gva.Api.Models.Views.Aircraft;

namespace Gva.Api.Repositories.AircraftRepository
{
    public class AircraftRepository : IAircraftRepository
    {
        private IUnitOfWork unitOfWork;

        public AircraftRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GvaViewAircraft> GetAircrafts(
            string mark,
            string manSN,
            string modelAlt,
            string icao,
            string airCategory,
            string aircraftProducer,
            bool exact,
            int offset = 0,
            int? limit = null)
        {
            var gvaAircrafts =
                this.unitOfWork.DbContext.Set<GvaViewAircraft>()
                .Include(a => a.AirCategory)
                .Include(a => a.AircraftProducer);

            var predicate = PredicateBuilder.True<GvaViewAircraft>();

            predicate = predicate
                .AndStringMatches(p => p.ManSN, manSN, exact)
                .AndStringMatches(p => p.ModelAlt, modelAlt, exact)
                .AndStringMatches(p => p.Mark, mark, exact)
                .AndStringMatches(p => p.ICAO, icao, exact)
                .AndStringMatches(p => p.AirCategory.Name, airCategory, exact)
                .AndStringMatches(p => p.AircraftProducer.Name, aircraftProducer, exact);

            return gvaAircrafts
                .Where(predicate)
                .OrderBy(p => p.Mark)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }
        public GvaViewAircraft GetAircraft(int aircraftId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewAircraft>()
                .Include(a => a.AirCategory)
                .Include(a => a.AircraftProducer)
                .SingleOrDefault(p => p.LotId == aircraftId);
        }

        public IEnumerable<GvaInvalidActNumber> GetInvalidActNumbers()
        {
            return this.unitOfWork.DbContext.Set<GvaInvalidActNumber>()
                .Include(n => n.Register);
        }

        public bool DevalidateActNumber(int actNumber, string reason)
        {
            var registration = this.unitOfWork.DbContext.Set<GvaViewAircraftRegistration>()
                .Where(r => r.ActNumber == actNumber)
                .FirstOrDefault();

            if (registration == null)
            {
                return false;
            }
            else
            {
                this.unitOfWork.DbContext.Set<GvaInvalidActNumber>()
                    .Add(new GvaInvalidActNumber()
                    {
                        RegisterId = registration.CertRegisterId,
                        Reason = reason,
                        ActNumber = actNumber
                    });

                this.unitOfWork.Save();

                return true;
            }
        }
        public IEnumerable<GvaViewAircraft> GetAircraftModels(
            string airCategory,
            string aircraftProducer,
            int offset = 0,
            int? limit = null)
        {
            var gvaAircrafts =
                this.unitOfWork.DbContext.Set<GvaViewAircraft>()
                .Include(a => a.AirCategory)
                .Include(a => a.AircraftProducer);

            var predicate = PredicateBuilder.True<GvaViewAircraft>();

            predicate = predicate
                .AndStringMatches(p => p.AirCategory.Name, airCategory, true)
                .AndStringMatches(p => p.AircraftProducer.Name, aircraftProducer, true);

            return gvaAircrafts
                .Where(predicate)
                .GroupBy(a => a.Model)
                .Select(g => g.FirstOrDefault())
                .OrderBy(p => p.Model)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public GvaViewAircraft GetAircraftModel(int aircraftId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewAircraft>()
                .Include(a => a.AirCategory)
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
