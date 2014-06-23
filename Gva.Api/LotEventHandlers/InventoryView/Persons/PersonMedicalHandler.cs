using System;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.InventoryView
{
    public class PersonMedicalHandler : CommitEventHandler<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public PersonMedicalHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Person",
                setPartAlias: "personMedical",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId && v.PartId == pv.Part.PartId)
        {
            this.userRepository = userRepository;
        }

        public override void Fill(GvaViewInventoryItem invItem, PartVersion partVersion)
        {
            invItem.LotId = partVersion.Part.Lot.LotId;
            invItem.Part = partVersion.Part;
            invItem.SetPartAlias = partVersion.Part.SetPart.Alias;

            invItem.Name = partVersion.Part.SetPart.Name;
            invItem.Type = null;
            invItem.Number = string.Format(
                    "{0}-{1}-{2}-{3}",
                    partVersion.Content.Get<string>("documentNumberPrefix"),
                    partVersion.Content.Get<string>("documentNumber"),
                    partVersion.Part.Lot.GetPart("personData").Content.Get<string>("lin"),
                    partVersion.Content.Get<string>("documentNumberSuffix"));
            invItem.Date = partVersion.Content.Get<DateTime?>("documentDateValidFrom");
            invItem.Publisher = partVersion.Content.Get<string>("documentPublisher.name");
            invItem.Valid = null;
            invItem.FromDate = partVersion.Content.Get<DateTime?>("documentDateValidFrom");
            invItem.ToDate = partVersion.Content.Get<DateTime?>("documentDateValidTo");

            if (partVersion.PartOperation == PartOperation.Add)
            {
                invItem.CreatedBy = this.userRepository.GetUser(partVersion.CreatorId).Fullname;
                invItem.CreationDate = partVersion.CreateDate;
            }
            else
            {
                invItem.EditedBy = this.userRepository.GetUser(partVersion.CreatorId).Fullname;
                invItem.EditedDate = partVersion.CreateDate;
            }
        }

        public override void Clear(GvaViewInventoryItem person)
        {
            throw new NotSupportedException();
        }
    }
}
