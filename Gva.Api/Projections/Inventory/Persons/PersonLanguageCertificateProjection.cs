using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Persons
{
    public class PersonLanguageCertificateProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public PersonLanguageCertificateProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Person")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var langCertificates = parts.GetAll<PersonLangCertDO>("personDocumentLangCertificates");

            return langCertificates.Select(t => this.Create(t));
        }

        private GvaViewInventoryItem Create(PartVersion<PersonLangCertDO> langCert)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = langCert.Part.Lot.LotId;
            invItem.PartId = langCert.Part.PartId;
            invItem.SetPartAlias = langCert.Part.SetPart.Alias;
            invItem.Name = langCert.Content.DocumentRole.Name;
            invItem.TypeId = langCert.Content.DocumentType.NomValueId;
            invItem.Number = langCert.Content.DocumentNumber;
            invItem.Date = langCert.Content.DocumentDateValidFrom.Value;
            invItem.Publisher = langCert.Content.DocumentPublisher;
            invItem.Valid = langCert.Content.Valid.Code == "Y";
            invItem.FromDate = langCert.Content.DocumentDateValidFrom.Value;
            invItem.ToDate = langCert.Content.DocumentDateValidTo;
            invItem.Notes = langCert.Content.Notes;

            invItem.CreatedBy = this.userRepository.GetUser(langCert.Part.CreatorId).Fullname;
            invItem.CreationDate = langCert.Part.CreateDate;

            if (langCert.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(langCert.CreatorId).Fullname;
                invItem.EditedDate = langCert.CreateDate;
            }

            return invItem;
        }
    }
}
