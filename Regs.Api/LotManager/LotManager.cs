using Common.Data;
using Regs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Regs.Api.LotManager
{
    public class LotManager : ILotManager
    {
        private IUnitOfWork unitOfWork;

        public LotManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Set GetSet(int setId)
        {
            Set set = this.unitOfWork.DbContext.Set<Set>()
                .Include(s => s.SetParts)
                .FirstOrDefault(s => s.SetId == setId);

            return this.unitOfWork.DbContext.Set<Set>().Find(setId);
        }

        public Set GetSet(string alias)
        {
            Set set = this.unitOfWork.DbContext.Set<Set>()
                .Include(s => s.SetParts)
                .FirstOrDefault(s => s.Alias == alias);

            return set;
        }

        public Lot GetLot(int lotId, int? commitId = null)
        {
            Commit commit;
            if (commitId.HasValue)
            {
                commit = this.unitOfWork.DbContext.Set<Commit>()
                    .FirstOrDefault(c => c.CommitId == commitId);
                if (commit.LotId != lotId)
                {
                    throw new Exception(string.Format("The specified commit with id {0} does not belong to lot with id {1}", commitId, lotId));
                }
            }
            else
            {
                commit = this.unitOfWork.DbContext.Set<Commit>()
                    .Where(c => c.LotId == lotId).FirstOrDefault(c => c.IsIndex == true);
            }

            IEnumerable<PartVersion> partVersions = this.unitOfWork.DbContext.Set<PartVersion>()
                .Include(pv => pv.TextBlob)
                .Where(pv => pv.Commits.Select(c => c.CommitId).Contains(commit.CommitId));

            if (partVersions.Any())
            {
                partVersions.AsQueryable().Load();
            }

            Lot lot = this.unitOfWork.DbContext.Set<Lot>()
                .Include(l => l.Parts)
                .FirstOrDefault(l => l.LotId == lotId);

            return lot;
        }
    }
}
