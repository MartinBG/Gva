using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/{lotId}/personAddresses")]
    [Authorize]
    public class PersonAddressesController : GvaCaseTypesPartController<PersonAddressDO>
    {
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;

        public PersonAddressesController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            INomRepository nomRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("personAddresses", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewAddress(int lotId)
        {
            var cases = this.caseTypeRepository.GetCaseTypesForSet("person")
                .Select(ct => new CaseDO()
                    {
                        CaseType = new NomValue()
                        {
                            NomValueId = ct.GvaCaseTypeId,
                            Name = ct.Name,
                            Alias = ct.Alias
                        },
                        IsAdded = true
                    })
                .ToList();

            PersonAddressDO newAddress = new PersonAddressDO()
            {
                ValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId
            };

            return Ok(new CaseTypesPartDO<PersonAddressDO>(newAddress, cases));
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var partVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts<PersonAddressDO>("personAddresses");

            List<PersonAddressViewDO> partVersionDOs = new List<PersonAddressViewDO>();
            foreach (var partVersion in partVersions)
            {
                var lotFiles = this.fileRepository.GetFileReferences(partVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFiles.Length != 0)
                {
                    partVersionDOs.Add(new PersonAddressViewDO()
                    {
                        PartIndex = partVersion.Part.Index,
                        Address = partVersion.Content.Address,
                        AddressAlt = partVersion.Content.AddressAlt,
                        AddressType = partVersion.Content.AddressTypeId.HasValue? this.nomRepository.GetNomValue("addressTypes", partVersion.Content.AddressTypeId.Value) : null,
                        Phone = partVersion.Content.Phone,
                        PostalCode = partVersion.Content.PostalCode,
                        Settlement = partVersion.Content.SettlementId.HasValue ? this.nomRepository.GetNomValue("cities", partVersion.Content.SettlementId.Value) : null,
                        Valid = partVersion.Content.ValidId.HasValue ? this.nomRepository.GetNomValue("boolean", partVersion.Content.ValidId.Value) : null
                    });
                }
            }

            return Ok(partVersionDOs);
        }
    }
}