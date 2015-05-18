using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Common.Api.Models;
using Common.Data;
using Common.Linq;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.ModelsDO.Aircrafts;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Repositories.AircraftRepository
{
    public class AircraftRegistrationRepository : IAircraftRegistrationRepository
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IAircraftRepository aircraftRepository;

        public AircraftRegistrationRepository(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IAircraftRepository aircraftRepository)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.aircraftRepository = aircraftRepository;
        }

        public int? GetLastActNumber(int? registerId = null, string alias = null)
        {
            var predicate = PredicateBuilder.True<GvaViewAircraftRegistration>();

            if (registerId.HasValue)
            {
                predicate = predicate.And(r => r.CertRegisterId == registerId);
            }

            if (!string.IsNullOrEmpty(alias))
            {
                predicate = predicate.And(a => a.Register.Alias == alias);
            }

            return this.unitOfWork.DbContext.Set<GvaViewAircraftRegistration>()
                .Where(predicate)
                .Max(v => (int?)v.ActNumber);
        }

        public List<GvaViewAircraftRegistration> GetAircraftsRegistrations(string regMark = null, int? registerId = null, int? certNumber = null, int? actNumber = null)
        {
            var predicate = PredicateBuilder.True<GvaViewAircraftRegistration>();

            if (registerId.HasValue)
            {
                predicate = predicate.And(a => a.CertRegisterId == registerId.Value);
            }

            if (!string.IsNullOrEmpty(regMark))
            {
                predicate = predicate.And(a => a.RegMark.Contains(regMark));
            }

            if (certNumber.HasValue)
            {
                predicate = predicate.And(a => a.CertNumber == certNumber.Value);
            }

            if (actNumber.HasValue)
            {
                predicate = predicate.And(a => a.ActNumber == actNumber.Value);
            }

            List<GvaViewAircraftRegistration> invalidActNumbers = new List<GvaViewAircraftRegistration>();
            if (string.IsNullOrEmpty(regMark) && !certNumber.HasValue)
            {
                invalidActNumbers = this.aircraftRepository.GetInvalidActNumbers(actNumber, registerId)
                    .Select(a => new GvaViewAircraftRegistration()
                    {
                        ActNumber = a.ActNumber,
                        Register = a.Register
                    })
                    .ToList();
            }

            var registrations = this.unitOfWork.DbContext.Set<GvaViewAircraftRegistration>()
                .Include(r => r.Register)
                .Where(predicate)
                .ToList();

            return registrations.Union(invalidActNumbers).OrderBy(i => i.ActNumber).ToList();
        }

        public List<NomValue> GetAircraftRegistrationNoms(int lotId, string term = null)
        {
            var registrations = this.lotRepository.GetLotIndex(lotId).Index
                .GetParts<AircraftCertRegistrationFMDO>("aircraftCertRegistrationsFM")
                .OrderByDescending(r => r.Content.CertDate)
                .Select(i => new NomValue()
                {
                    NomValueId = i.Part.Index,
                    Name = string.Format(
                        i.Content.RegMark +
                        (i.Content.CertNumber.HasValue ? string.Format("/рег.№ {0}", i.Content.CertNumber.ToString()) : string.Empty) +
                        (i.Content.ActNumber.HasValue ? string.Format("/дел.№ {0}", i.Content.ActNumber.ToString()) : string.Empty))
                });

            if (!string.IsNullOrEmpty(term))
            {
                registrations = registrations.Where(a => a.Name.Contains(term));
            }

            return registrations.ToList();
        }
    }
}
