using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Data;
using Common.Linq;
using Docs.Api.Models;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Repositories.InventoryRepository
{
    public class InventoryRepository : IInventoryRepository
    {
        private IUnitOfWork unitOfWork;

        public InventoryRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<InventoryItemDO> GetInventoryItemsForLot(int lotId, int? caseTypeId)
        {
            var query =
                from i in this.unitOfWork.DbContext.Set<GvaViewInventoryItem>()
                join f in this.unitOfWork.DbContext.Set<GvaLotFile>() on i.PartId equals f.LotPartId into fg
                from f in fg.DefaultIfEmpty()
                join df in this.unitOfWork.DbContext.Set<DocFile>() on f.DocFileId equals df.DocFileId into dfg
                from df in dfg.DefaultIfEmpty()
                join gf in this.unitOfWork.DbContext.Set<GvaFile>() on f.GvaFileId equals gf.GvaFileId into gfg
                from gf in gfg.DefaultIfEmpty()
                select new
                {
                    i.LotId,
                    f.GvaCaseTypeId,

                    SetPartAlias = i.SetPartAlias,
                    PartIndex = i.Part.Index,
                    Name = i.Name,
                    Type = i.Type,
                    Number = i.Number,
                    Date = i.Date,
                    Publisher = i.Publisher,
                    Valid = i.Valid,
                    FromDate = i.FromDate,
                    ToDate = i.ToDate,
                    CreatedBy = i.CreatedBy,
                    CreationDate = i.CreationDate,
                    EditedBy = i.EditedBy,
                    EditedDate = i.EditedDate,
                    BookPageNumber = f.PageIndex,
                    PageCount = f.PageNumber,
                    DocFile = df,
                    GvaFile = gf
                };

            var predicate =
                PredicateBuilder.True(query)
                .And(i => i.LotId == lotId);

            if (caseTypeId != null)
            {
                predicate = predicate.And(i => i.GvaCaseTypeId == caseTypeId.Value);
            }

            return query.Where(predicate)
                .ToList()
                .OrderBy(f =>
                {
                    var pageIndexNumPart = Regex.Match(f.BookPageNumber ?? "", @"^\d+");
                    return pageIndexNumPart.Success ?
                        string.Format("{0:D5}{1}", int.Parse(pageIndexNumPart.Value), f.BookPageNumber.Substring(pageIndexNumPart.Value.Length)) :
                        f.BookPageNumber;
                })
                .Select(i => new InventoryItemDO
                {
                    SetPartAlias = i.SetPartAlias,
                    PartIndex = i.PartIndex,
                    Name = i.Name,
                    Type = i.Type,
                    Number = i.Number,
                    Date = i.Date,
                    Publisher = i.Publisher,
                    Valid = i.Valid,
                    FromDate = i.FromDate,
                    ToDate = i.ToDate,
                    CreatedBy = i.CreatedBy,
                    CreationDate = i.CreationDate,
                    EditedBy = i.EditedBy,
                    EditedDate = i.EditedDate,
                    BookPageNumber = i.BookPageNumber,
                    PageCount = i.PageCount,
                    File =
                        i.DocFile != null ?
                            new FileDataDO(i.DocFile) :
                            i.GvaFile != null ?
                                new FileDataDO(i.GvaFile) :
                                null
                });
        }
    }
}
