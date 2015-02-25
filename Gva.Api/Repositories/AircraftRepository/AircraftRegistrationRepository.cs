using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Data;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.ModelsDO.Aircrafts;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Repositories.AircraftRepository
{
    public class AircraftRegistrationRepository : IAircraftRegistrationRepository
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;

        public AircraftRegistrationRepository(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
        }

        public int? GetLastActNumber(int registerId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewAircraftRegistration>()
                .Where(v => v.CertRegisterId == registerId)
                .Max(v => (int?)v.ActNumber);
        }

        public List<NomValue> GetAircraftRegistrationNoms(int lotId, string term = null)
        {
            var registrations = this.lotRepository.GetLotIndex(lotId).Index
                .GetParts<AircraftCertRegistrationFMDO>("aircraftCertRegistrationsFM")
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
