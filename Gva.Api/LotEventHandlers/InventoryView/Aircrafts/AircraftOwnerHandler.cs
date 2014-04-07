using System;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.InventoryView
{
    public class AircraftOwnerHandler : CommitEventHandler<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public AircraftOwnerHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Aircraft",
                setPartAlias: "aircraftOwner",
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
            invItem.Name = partVersion.DynamicContent.aircraftRelation.name;
            invItem.Number = partVersion.DynamicContent.documentNumber;
            invItem.Date = partVersion.DynamicContent.documentDate;
            invItem.Valid = null;
            invItem.FromDate = partVersion.DynamicContent.fromDate;
            invItem.ToDate = partVersion.DynamicContent.toDate;

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

        public override void Clear(GvaViewInventoryItem inventory)
        {
            throw new NotSupportedException();
        }
    }
}
