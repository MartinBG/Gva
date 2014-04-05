﻿using System;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.InventoryView
{
    public class AircraftDebtHandler : CommitEventHandler<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public AircraftDebtHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(
                unitOfWork: unitOfWork,
                setPartAlias: "aircraftDebtFM",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId && v.PartId == pv.Part.PartId)
        {
            this.userRepository = userRepository;
        }

        public override void Fill(GvaViewInventoryItem invItem, PartVersion partVersion)
        {
            invItem.Lot = partVersion.Part.Lot;
            invItem.Part = partVersion.Part;

            invItem.DocumentType = partVersion.Part.SetPart.Alias;
            invItem.Name = partVersion.DynamicContent.aircraftDebtType.name;
            invItem.Number = partVersion.DynamicContent.documentNumber;
            invItem.Date = partVersion.DynamicContent.documentDate;
            invItem.Publisher = partVersion.DynamicContent.documentPublisher;

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
