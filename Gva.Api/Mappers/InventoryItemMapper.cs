using System;
using AutoMapper;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Mappers
{
    public class InventoryItemMapper : IMapper
    {
        public void CreateMap()
        {
            Mapper.CreateMap<Tuple<GvaInventoryItem, GvaLotFile>, InventoryItemDO>()
                .ConvertUsing(t =>
                    {
                        var inventoryItem = t.Item1;
                        var lotFile = t.Item2;

                        var inventoryItemDO = new InventoryItemDO ()
                        {
                            DocumentType = inventoryItem.DocumentType,
                            PartIndex = inventoryItem.Part.Index.Value,
                            Name = inventoryItem.Name,
                            Type = inventoryItem.Type,
                            Number = inventoryItem.Number,
                            Date = inventoryItem.Date,
                            Publisher = inventoryItem.Publisher,
                            Valid = inventoryItem.Valid,
                            FromDate = inventoryItem.FromDate,
                            ToDate = inventoryItem.ToDate,
                            CreatedBy = inventoryItem.CreatedBy,
                            CreationDate = inventoryItem.CreationDate,
                            EditedBy = inventoryItem.EditedBy,
                            EditedDate = inventoryItem.EditedDate
                        };

                        if (lotFile == null)
                        {
                            return inventoryItemDO;
                        }

                        FileDO file = new FileDO()
                        {
                            LotFileId = lotFile.GvaLotFileId,
                            BookPageNumber = lotFile.PageIndex,
                            PageCount = lotFile.PageNumber,
                        };

                        FileDataDO fileData = new FileDataDO();
                        if (lotFile.DocFileId.HasValue)
                        {
                            fileData.Name = lotFile.DocFile.DocFileName;
                            fileData.Key = lotFile.DocFile.DocFileContentId;
                        }
                        else
                        {
                            fileData.Name = lotFile.GvaFile.Filename;
                            fileData.Key = lotFile.GvaFile.FileContentId;
                        }

                        file.File = fileData;
                        inventoryItemDO.File = file;

                        return inventoryItemDO;
                    }
                );
        }
    }
}
