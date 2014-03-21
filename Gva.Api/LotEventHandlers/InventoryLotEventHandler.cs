using System.Linq;
using System.Text.RegularExpressions;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.LotEvents;
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
            FileEvent fileEvent = e as FileEvent;
            if (fileEvent != null)
            {
                var partVersion = fileEvent.PartVersion;
                if (!parts.Contains(partVersion.Part.SetPart.Alias))
                {
                    return;
                }

                if (partVersion.PartOperation == PartOperation.Delete)
                {
                    this.inventoryRepository.DeleteInventoryItemsForPart(partVersion.Part.PartId);
                    return;
                }

                var inventoryItemWithoutDoc = this.inventoryRepository.GetInventoryItem(partVersion.Part.PartId, null);
                if (fileEvent.AddedFiles.Count == 0 && fileEvent.UpdatedFiles.Count == 0)
                {
                    if (inventoryItemWithoutDoc == null)
                    {
                        this.AddInventoryItem(partVersion, null);
                    }
                    else
                    {
                        this.UpdateInventoryItem(inventoryItemWithoutDoc, partVersion, null);
                    }
                }

                if (inventoryItemWithoutDoc != null && fileEvent.AddedFiles.Count != 0)
                {
                    this.inventoryRepository.DeleteInventoryItem(inventoryItemWithoutDoc);
                }

                foreach (var addedFile in fileEvent.AddedFiles)
                {
                    this.AddInventoryItem(partVersion, addedFile);
                }

                foreach (var updatedFile in fileEvent.UpdatedFiles)
                {
                    var inventoryItem = this.inventoryRepository.GetInventoryItem(partVersion.Part.PartId, updatedFile.GvaCaseTypeId);
                    this.UpdateInventoryItem(inventoryItem, partVersion, updatedFile);
                }

                foreach (var deletedFile in fileEvent.DeletedFiles)
                {
                    var inventoryItem = this.inventoryRepository.GetInventoryItem(partVersion.Part.PartId, deletedFile.GvaCaseTypeId);
                    this.inventoryRepository.DeleteInventoryItem(inventoryItem);
                }
            }
        }

        private void AddInventoryItem(PartVersion partVersion, GvaLotFile file)
        {
            GvaInventoryItem inventoryItem = new GvaInventoryItem()
            {
                Part = partVersion.Part,
                Lot = partVersion.Part.Lot,
                CaseTypeId = file == null ? null : (int?)file.GvaCaseTypeId,
                DocumentType = partVersion.Part.SetPart.Alias,
                CreatedBy = this.userRepository.GetUser(partVersion.CreatorId).Fullname,
                CreationDate = partVersion.CreateDate
            };

            this.ModifyInventoryData(inventoryItem, partVersion.Content, partVersion.Part.SetPart.Alias, partVersion.Part.Lot, file);

            this.inventoryRepository.AddInventoryItem(inventoryItem);
        }

        private void UpdateInventoryItem(GvaInventoryItem inventoryItem, PartVersion partVersion, GvaLotFile file)
        {
            inventoryItem.EditedBy = this.userRepository.GetUser(partVersion.CreatorId).Fullname;
            inventoryItem.EditedDate = partVersion.CreateDate;

            this.ModifyInventoryData(inventoryItem, partVersion.Content, partVersion.Part.SetPart.Alias, partVersion.Part.Lot, file);
        }

        private void ModifyInventoryData(GvaInventoryItem inventoryItem, dynamic content, string partAlias, Lot lot, GvaLotFile file)
        {
            inventoryItem.Number = content.documentNumber;
            inventoryItem.Valid = content.valid == null ? null : content.valid.code == "Y";
            inventoryItem.FromDate = content.documentDateValidFrom;
            inventoryItem.ToDate = content.documentDateValidTo;
            inventoryItem.Type = content.documentType == null ? null : content.documentType.name;
            inventoryItem.Publisher = content.documentPublisher == null ? null: content.documentPublisher.ToString();
            inventoryItem.Date = content.completionDate ?? content.documentDateValidFrom;

            if (file != null)
            {
                inventoryItem.BookPageNumber = file.PageIndex;
                inventoryItem.PageCount = file.PageNumber;

                var pageIndexNumPart = Regex.Match(file.PageIndex, @"^\d+");
                if (pageIndexNumPart.Success)
                {
                    inventoryItem.PageIndex = string.Format("{0:D5}", int.Parse(pageIndexNumPart.Value)) + file.PageIndex.Substring(pageIndexNumPart.Value.Length);
                }

                if (file.DocFileId.HasValue)
                {
                    inventoryItem.Filename = file.DocFile.DocFileName;
                    inventoryItem.FileContentId = file.DocFile.DocFileContentId;
                }
                else
                {
                    inventoryItem.Filename = file.GvaFile.Filename;
                    inventoryItem.FileContentId = file.GvaFile.FileContentId;
                }
            }

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
