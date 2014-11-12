using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Data;
using Common.Linq;
using Docs.Api.Models;
using Gva.Api.Models;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO;
using System;

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
                from i in this.unitOfWork.DbContext.Set<GvaViewInventoryItem>().Include(i => i.Type)
                join f in this.unitOfWork.DbContext.Set<GvaLotFile>() on i.PartId equals f.LotPartId into fg
                from f in fg.DefaultIfEmpty()
                join df in this.unitOfWork.DbContext.Set<DocFile>() on f.DocFileId equals df.DocFileId into dfg
                from df in dfg.DefaultIfEmpty()
                join gf in this.unitOfWork.DbContext.Set<GvaFile>() on f.GvaFileId equals gf.GvaFileId into gfg
                from gf in gfg.DefaultIfEmpty()
                select new
                {
                    i.LotId,
                    GvaCaseTypeId = (int?)f.GvaCaseTypeId,

                    SetPartAlias = i.SetPartAlias,
                    PartIndex = i.Part.Index,
                    ParentPartIndex = (int?)i.ParentPart.Index,
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
                    BookPageNumber = f.PageIndexInt,
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
                .OrderBy(f => f.BookPageNumber)
                .Select(i => new InventoryItemDO
                {
                    SetPartAlias = i.SetPartAlias,
                    PartIndex = i.PartIndex,
                    ParentPartIndex = i.ParentPartIndex,
                    Name = i.Name,
                    Type = i.Type == null ? null : i.Type.Name,
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

        public IEnumerable<string> GetNotes(string notes)
        {
            var predicate = PredicateBuilder.True<GvaViewInventoryItem>();
            if (notes != null)
            {
                char[] separator = { ' ' };
                var terms = notes.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                foreach (string term in terms)
                {
                    predicate = predicate.AndStringContains(p => p.Notes, term);
                }
            }

            return this.unitOfWork.DbContext.Set<GvaViewInventoryItem>()
                .Where(predicate)
                .Where(e => e.Notes != null)
                .Select(i => i.Notes)
                .Distinct()
                .ToList();
        }
    }
}
