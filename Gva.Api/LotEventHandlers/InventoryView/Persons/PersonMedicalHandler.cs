using System;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models;
using Regs.Api.Extensions;
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
            invItem.Lot = partVersion.Part.Lot;
            invItem.Part = partVersion.Part;
            invItem.SetPartAlias = partVersion.Part.SetPart.Alias;

            invItem.Name = partVersion.Part.SetPart.Name;
            invItem.Type = null;
            invItem.Number = string.Format(
                    "{0}-{1}-{2}-{3}",
                    partVersion.GetString("documentNumberPrefix"),
                    partVersion.GetString("documentNumber"),
                    partVersion.Part.Lot.GetPart("personData").GetString("lin"),
                    partVersion.GetString("documentNumberSuffix"));
            invItem.Date = partVersion.GetDate("documentDateValidFrom");
            invItem.Publisher = partVersion.GetString("documentPublisher.name");
            invItem.Valid = null;
            invItem.FromDate = partVersion.GetDate("documentDateValidFrom");
            invItem.ToDate = partVersion.GetDate("documentDateValidTo");

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
