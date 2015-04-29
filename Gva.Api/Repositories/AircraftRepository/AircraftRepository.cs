using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Api.Models;
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

        public IEnumerable<Tuple<GvaViewAircraft, GvaViewAircraftRegistration>> GetAircrafts(
            string mark = null,
            string manSN = null,
            string modelAlt = null,
            string icao = null,
            string airCategory = null,
            string aircraftProducer = null,
            bool exact = false,
            int offset = 0,
            int? limit = null)
        {
            var gvaAircraftsWithRegData =
                (from a in this.unitOfWork.DbContext.Set<GvaViewAircraft>()
                            .Include(a => a.AirCategory)
                            .Include(a => a.AircraftProducer)
                 join r in this.unitOfWork.DbContext.Set<GvaViewAircraftRegistration>()
                            .Include(v => v.Register)
                         on a.ActNumber equals r.ActNumber into ra
                 from ra1 in ra.DefaultIfEmpty()
                 select new
                 {
                     Aircraft = a,
                     Registration = ra1
                 });

            var registers = gvaAircraftsWithRegData
                .Where(r => r.Registration != null)
                .Select(r => r.Registration.CertRegisterId)
                .ToList();

            this.unitOfWork.DbContext.Set<NomValue>().Where(r => registers.Contains(r.NomValueId)).Load();

            var predicate = PredicateBuilder.True(new
            {
                Aircraft = new GvaViewAircraft(),
                Registration = new GvaViewAircraftRegistration()
            });

            predicate = predicate
                .AndStringMatches(p => p.Aircraft.ManSN, manSN, exact)
                .AndStringMatches(p => p.Aircraft.ModelAlt, modelAlt, exact)
                .AndStringMatches(p => p.Registration.RegMark, mark, exact)
                .AndStringMatches(p => p.Aircraft.ICAO, icao, exact)
                .AndStringMatches(p => p.Aircraft.AirCategory.Name, airCategory, exact)
                .AndStringMatches(p => p.Aircraft.AircraftProducer.Name, aircraftProducer, exact);

            var result = gvaAircraftsWithRegData
                .Where(predicate)
                .OrderBy(p => p.Registration.RegMark)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return result.Select(f => new Tuple<GvaViewAircraft, GvaViewAircraftRegistration>(f.Aircraft, f.Registration));
        }

        public Tuple<GvaViewAircraft, GvaViewAircraftRegistration> GetAircraft(int aircraftId)
        {
            var result = (from a in this.unitOfWork.DbContext.Set<GvaViewAircraft>()
                                .Include(a => a.AirCategory)
                                .Include(a => a.AircraftProducer)
                                .Where(p => p.LotId == aircraftId)
                    join r in this.unitOfWork.DbContext.Set<GvaViewAircraftRegistration>()
                                .Include(v => v.Register)
                             on a.ActNumber equals r.ActNumber into ra
                    from ra1 in ra.DefaultIfEmpty()
                    select new
                    {
                        Aircraft = a,
                        Registration = ra1
                    })
                    .SingleOrDefault();

            if (result.Registration != null)
            {
                this.unitOfWork.DbContext.Set<NomValue>().Where(r => result.Registration.CertRegisterId == r.NomValueId).Load();
            }

            return new Tuple<GvaViewAircraft, GvaViewAircraftRegistration>(result.Aircraft, result.Registration);
        }

        public IEnumerable<GvaInvalidActNumber> GetInvalidActNumbers(int? actNumber = null, int? registerId = null)
        {
            var predicate = PredicateBuilder.True<GvaInvalidActNumber>();

            if (registerId.HasValue)
            {
                predicate = predicate.And(a => a.RegisterId == registerId.Value);
            }

            if (actNumber.HasValue)
            {
                predicate = predicate.And(a => a.ActNumber == actNumber.Value);
            }

            return this.unitOfWork.DbContext.Set<GvaInvalidActNumber>()
                .Include(n => n.Register)
                .Where(predicate);
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
