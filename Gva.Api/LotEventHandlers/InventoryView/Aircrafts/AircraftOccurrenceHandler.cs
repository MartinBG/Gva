using System;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.InventoryView
{
    public class AircraftOccurrenceHandler : CommitEventHandler<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public AircraftOccurrenceHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Aircraft",
                setPartAlias: "aircraftOccurrence",
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
            invItem.Name = partVersion.Content.Get<string>("aircraftOccurrenceClass.name");
            invItem.Date = partVersion.Content.Get<DateTime?>("localDate");

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
