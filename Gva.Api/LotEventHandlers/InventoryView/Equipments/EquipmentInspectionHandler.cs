﻿using System;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.InventoryView
{
    public class EquipmentInspectionHandler : CommitEventHandler<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public EquipmentInspectionHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Equipment",
                setPartAlias: "equipmentInspection",
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
            invItem.Type = "asd";
            invItem.Number = null;
            invItem.Date = null;
            invItem.Publisher = null;
            invItem.Valid = null;
            invItem.FromDate = partVersion.Content.Get<DateTime?>("inspectionFrom");
            invItem.ToDate = partVersion.Content.Get<DateTime?>("inspectionTo");

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
