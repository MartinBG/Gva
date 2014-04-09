using System;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models;
using Regs.Api.Extensions;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.InventoryView
{
    public class PersonOtherHandler : CommitEventHandler<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public PersonOtherHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Person",
                setPartAlias: "personOther",
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

            invItem.Name = partVersion.GetString("documentRole.name");
            invItem.Type = partVersion.GetString("documentType.name");
            invItem.Number = partVersion.GetString("documentNumber");
            invItem.Date = partVersion.GetDate("documentDateValidFrom");
            invItem.Publisher = partVersion.GetString("documentPublisher");
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
