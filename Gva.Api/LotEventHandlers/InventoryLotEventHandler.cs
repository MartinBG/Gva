﻿using System.Linq;
using System.Text.RegularExpressions;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.Repositories.InventoryRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers
{
    public class InventoryLotEventHandler : ILotEventHandler
    {
        private static string[] parts = { "education", "documentId", "training", "medical", "check", "other" };

        private IInventoryRepository inventoryRepository;
        private IUserRepository userRepository;

        public InventoryLotEventHandler(IInventoryRepository inventoryRepository, IUserRepository userRepository)
        {
            this.inventoryRepository = inventoryRepository;
            this.userRepository = userRepository;
        }

        public void Handle(IEvent e)
        {
            CommitEvent commitEvent = e as CommitEvent;
            if (commitEvent != null)
            {
                var commit = commitEvent.Commit;

                var changedPartVersions = commit.ChangedPartVersions.Where(pv => parts.Contains(pv.Part.SetPart.Alias));
                foreach (var partVersion in changedPartVersions)
                {
                    if (partVersion.PartOperation == PartOperation.Add)
                    {
                        this.AddInventoryItem(partVersion);
                    }
                    else
                    {
                        var inventoryItem = this.inventoryRepository.GetInventoryItem(partVersion.Part.PartId);

                        if (partVersion.PartOperation == PartOperation.Update)
                        {
                            this.UpdateInventoryItem(inventoryItem, partVersion);
                        }
                        else
                        {
                            this.inventoryRepository.DeleteInventoryItem(inventoryItem);
                        }
                    }
                }
            }
        }

        private void AddInventoryItem(PartVersion partVersion)
        {
            GvaInventoryItem inventoryItem = new GvaInventoryItem()
            {
                Part = partVersion.Part,
                Lot = partVersion.Part.Lot,
                DocumentType = partVersion.Part.SetPart.Alias,
                CreatedBy = this.userRepository.GetUser(partVersion.CreatorId).Fullname,
                CreationDate = partVersion.CreateDate
            };

            this.ModifyInventoryData(inventoryItem, partVersion.Content, partVersion.Part.SetPart.Alias, partVersion.Part.Lot);

            this.inventoryRepository.AddInventoryItem(inventoryItem);
        }

        private void UpdateInventoryItem(GvaInventoryItem inventoryItem, PartVersion partVersion)
        {
            inventoryItem.EditedBy = this.userRepository.GetUser(partVersion.CreatorId).Fullname;
            inventoryItem.EditedDate = partVersion.CreateDate;

            this.ModifyInventoryData(inventoryItem, partVersion.Content, partVersion.Part.SetPart.Alias, partVersion.Part.Lot);
        }

        private void ModifyInventoryData(GvaInventoryItem inventoryItem, dynamic content, string partAlias, Lot lot)
        {
            inventoryItem.Number = content.documentNumber;
            inventoryItem.Valid = content.valid == null ? null : content.valid.code == "Y";
            inventoryItem.FromDate = content.documentDateValidFrom;
            inventoryItem.ToDate = content.documentDateValidTo;
            inventoryItem.Type = content.documentType == null ? null : content.documentType.name;
            inventoryItem.Publisher = content.documentPublisher == null ? null: content.documentPublisher.ToString();
            inventoryItem.Date = content.completionDate ?? content.documentDateValidFrom;

            if (partAlias == "education")
            {
                inventoryItem.Name = "Образование";
                inventoryItem.Publisher = content.school.name;
                inventoryItem.Type = content.graduation.name;
            }
            else if (partAlias == "documentId")
            {
                inventoryItem.Name = "Документ за самоличност";
            }
            else if (partAlias == "training")
            {
                inventoryItem.Name = content.documentRole.name;
            }
            else if (partAlias == "medical")
            {
                inventoryItem.Name = "Медицинско свидетелство";
                inventoryItem.Publisher = content.documentPublisher.name;

                dynamic personData = lot.GetPart("personData").Content;
                inventoryItem.Number = string.Format(
                    "{0}-{1}-{2}-{3}",
                    content.documentNumberPrefix,
                    content.documentNumber,
                    personData.lin,
                    content.documentNumberSuffix);
            }
            else if (partAlias == "check")
            {
                inventoryItem.Name = content.documentRole.name;
            }
            else if (partAlias == "other")
            {
                inventoryItem.Name = content.documentRole.name;
            }
        }
    }
}
