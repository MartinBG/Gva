using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Repositories.AircraftRepository
{
    public class AircraftDocumentDebtFMRepository : IAircraftDocumentDebtFMRepository
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private string path;

        public AircraftDocumentDebtFMRepository(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.path = "aircraftDocumentDebtsFM";
        }

        public List<CaseTypePartDO<AircraftDocumentDebtFMDO>> GetRegistrationDebts(int lotId, int partIndex, int? caseTypeId = null)
        {
            var partVersions = this.lotRepository.GetLotIndex(lotId).Index
                .GetParts<AircraftDocumentDebtFMDO>(this.path)
                .Where(e => e.Content.Registration.NomValueId == partIndex);

            List<CaseTypePartDO<AircraftDocumentDebtFMDO>> partVersionDOs = new List<CaseTypePartDO<AircraftDocumentDebtFMDO>>();
            foreach (var partVersion in partVersions)
            {
                var lotFile = this.fileRepository.GetFileReference(partVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    partVersionDOs.Add(new CaseTypePartDO<AircraftDocumentDebtFMDO>(partVersion, lotFile));
                }
            }

            return partVersionDOs;
        }

        public List<CaseTypePartDO<AircraftDocumentDebtFMViewDO>> GetDocumentDebts(int lotId, int? partIndex = null, int? caseTypeId = null)
        {
            var partVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts<AircraftDocumentDebtFMDO>(this.path);
            if (partIndex.HasValue)
            {
                partVersions = partVersions.Where(pv => pv.Content.Registration.NomValueId == partIndex).ToArray();
            }

            List<CaseTypePartDO<AircraftDocumentDebtFMViewDO>> partVersionDOs = new List<CaseTypePartDO<AircraftDocumentDebtFMViewDO>>();
            foreach (var partVersion in partVersions)
            {
                PartVersion<AircraftDocumentDebtFMViewDO> aircraftDocumentDebtsViewPartVersion = new PartVersion<AircraftDocumentDebtFMViewDO>(partVersion);

                var registration = this.lotRepository.GetLotIndex(lotId).Index
                    .GetParts<AircraftCertRegistrationFMDO>("aircraftCertRegistrationsFM")
                    .Where(e => e.Part.Index == partVersion.Content.Registration.NomValueId)
                    .FirstOrDefault();

                if (registration != null)
                {
                    aircraftDocumentDebtsViewPartVersion.Content.RegistrationActNumber = registration.Content.ActNumber;
                    aircraftDocumentDebtsViewPartVersion.Content.RegistrationCertNumber = registration.Content.CertNumber;
                }

                var lotFile = this.fileRepository.GetFileReference(partVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    partVersionDOs.Add(new CaseTypePartDO<AircraftDocumentDebtFMViewDO>(aircraftDocumentDebtsViewPartVersion, lotFile));
                }
            }

            return partVersionDOs;
        }
    }
}
