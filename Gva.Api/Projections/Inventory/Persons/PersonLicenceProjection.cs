using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Persons
{
    public class PersonLicenceProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;
        private INomRepository nomRepository;

        public PersonLicenceProjection(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            INomRepository nomRepository)
            : base(unitOfWork, "Person")
        {
            this.userRepository = userRepository;
            this.nomRepository = nomRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var licences = parts.GetAll<PersonLicenceDO>("licences");
            var editions = parts.GetAll<PersonLicenceEditionDO>("licenceEditions");

            List<GvaViewInventoryItem> invView = new List<GvaViewInventoryItem>();
            foreach (var licence in licences)
            {
                var licenceEditions = editions.Where(e => e.Content.LicencePartIndex == licence.Part.Index);

                foreach (var edition in licenceEditions)
                {
                    invView.Add(this.Create(licence, edition));
                }
            }

            return invView;
        }

        private GvaViewInventoryItem Create(PartVersion<PersonLicenceDO> personLicence, PartVersion<PersonLicenceEditionDO> edition)
        {
            var licenceType = this.nomRepository.GetNomValue("licenceTypes", personLicence.Content.LicenceType.NomValueId);

            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = personLicence.Part.Lot.LotId;
            invItem.PartId = edition.Part.PartId;
            invItem.ParentPartId = personLicence.Part.PartId;
            invItem.SetPartAlias = personLicence.Part.SetPart.Alias;
            invItem.Name = personLicence.Part.SetPart.Name;
            invItem.TypeId = licenceType.NomValueId;
            invItem.Number = personLicence.Content.LicenceNumber.HasValue ?
                string.Format("{0} {1} - {2}", personLicence.Content.Publisher.Code, licenceType.TextContent.Get<string>("licenceCode"), personLicence.Content.LicenceNumber) :
                null;
            invItem.Date = edition.Content.DocumentDateValidFrom.Value;
            invItem.Publisher = personLicence.Content.Publisher.Code;
            invItem.Valid = personLicence.Content.Valid == null ? (bool?)null : personLicence.Content.Valid.Code == "Y";
            invItem.FromDate = edition.Content.DocumentDateValidFrom.Value;
            invItem.ToDate = edition.Content.DocumentDateValidTo;
            invItem.Notes = edition.Content.Notes;

            invItem.CreatedBy = this.userRepository.GetUser(personLicence.Part.CreatorId).Fullname;
            invItem.CreationDate = personLicence.Part.CreateDate;

            if (personLicence.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(personLicence.CreatorId).Fullname;
                invItem.EditedDate = personLicence.CreateDate;
            }

            return invItem;
        }
    }
}
