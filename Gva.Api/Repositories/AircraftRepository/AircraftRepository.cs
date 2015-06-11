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
            List<int> lotIdsOfAircarftWithDefiniteRegMark = new List<int>();
            if (!string.IsNullOrEmpty(mark))
            {
                lotIdsOfAircarftWithDefiniteRegMark = this.unitOfWork.DbContext.Set<GvaViewAircraftRegMark>()
                    .Where(r => r.RegMark.ToLower().Contains(mark.ToLower()))
                    .Select(a => a.LotId)
                    .ToList();
            }

            var gvaAircraftsWithRegData =
                (from a in this.unitOfWork.DbContext.Set<GvaViewAircraft>()
                            .Include(a => a.AirCategory)
                            .Include(a => a.AircraftProducer)
                 join r in this.unitOfWork.DbContext.Set<GvaViewAircraftRegistration>()
                            .Include(v => v.Register)
                         on new { a.ActNumber, a.LotId } equals new { r.ActNumber, r.LotId } into ra
                 from ra1 in ra.DefaultIfEmpty()
                 select new
                 {
                     Aircraft = a,
                     Registration = ra1,
                     AirCategory = a.AirCategory,
                     AircraftProducer = a.AircraftProducer,
                     Register = ra1.Register
                 });

            var predicate = PredicateBuilder.True(new
            {
                Aircraft = new GvaViewAircraft(),
                Registration = new GvaViewAircraftRegistration(),
                AirCategory = new NomValue(),
                AircraftProducer = new NomValue(),
                Register = new NomValue()
            });

            predicate = predicate
                .AndStringMatches(p => p.Aircraft.ManSN, manSN, exact)
                .AndStringMatches(p => p.Aircraft.ModelAlt, modelAlt, exact)
                .AndStringMatches(p => p.Aircraft.ICAO, icao, exact)
                .AndStringMatches(p => p.AirCategory.Name, airCategory, exact)
                .AndStringMatches(p => p.AircraftProducer.Name, aircraftProducer, exact);

            if (lotIdsOfAircarftWithDefiniteRegMark.Count() > 0)
            {
                predicate = predicate.And(a => lotIdsOfAircarftWithDefiniteRegMark.Contains(a.Aircraft.LotId));
            }

            var result = gvaAircraftsWithRegData
                .Where(predicate)
                .OrderBy(p => p.Registration.RegMark)
                .WithOffsetAndLimit(offset, limit)
                .ToList();

            return result.Select(f => new Tuple<GvaViewAircraft, GvaViewAircraftRegistration>(f.Aircraft, f.Registration));
        }

        public Tuple<GvaViewAircraft, GvaViewAircraftRegistration> GetAircraft(int aircraftId)
        {
            var results = (from a in this.unitOfWork.DbContext.Set<GvaViewAircraft>()
                                .Include(a => a.AirCategory)
                                .Include(a => a.AircraftProducer)
                          join r in this.unitOfWork.DbContext.Set<GvaViewAircraftRegistration>()
                                      .Include(v => v.Register)
                                   on new { a.ActNumber, a.LotId } equals new { r.ActNumber, r.LotId } into ra
                          where a.LotId == aircraftId
                          from ra1 in ra.DefaultIfEmpty()
                          select new
                          {
                              AirCategory = a.AirCategory,
                              AircraftProducer = a.AircraftProducer,
                              Aircraft = a,
                              Registration = ra1,
                              Register = ra1.Register
                          });

            GvaViewAircraft aircraftData = null;
            GvaViewAircraftRegistration regData = null;
            if (results.Count() > 1)
            {
                var result = results.OrderByDescending(r => r.Registration.PartIndex).FirstOrDefault();
                regData = result.Registration ?? null;
                aircraftData = result.Aircraft;
            }
            else
            {
                var result = results.FirstOrDefault();
                aircraftData = result.Aircraft;
                regData = result.Registration;
            }

            return new Tuple<GvaViewAircraft, GvaViewAircraftRegistration>(aircraftData, regData);
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

        public int? GetLastNumberPerForm(int? formPrefix, string formName = null)
        {
            int? result = (int?)null;
            GvaViewAircraftCert lastEntry = this.unitOfWork.DbContext.Set<GvaViewAircraftCert>()
                .Where(c => formPrefix.HasValue ? c.FormNumberPrefix == formPrefix : c.FormName == formName)
                .OrderByDescending(c => c.ParsedNumberWithoutPrefix)
                .FirstOrDefault();

            if(lastEntry != null)
            {
                result = lastEntry.ParsedNumberWithoutPrefix;
            }

            return result;
        }

        public bool IsUniqueFormNumber(string formName, string formNumber, int lotId, int? partIndex = null)
        {
            return !this.unitOfWork.DbContext.Set<GvaViewAircraftCert>()
                .Where(c => c.FormName == formName && 
                    c.DocumentNumber == formNumber &&
                    (partIndex.HasValue ? partIndex.Value != c.PartIndex : true)).Any();
        }
    }
}
