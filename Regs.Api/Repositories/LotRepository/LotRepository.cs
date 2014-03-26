using System;
using System.Data.Entity;
using System.Linq;
using Common.Data;
using Regs.Api.Models;

namespace Regs.Api.Repositories.LotRepositories
{
    public class LotRepository : ILotRepository
    {
        private IUnitOfWork unitOfWork;

        public LotRepository(IUnitOfWork unitOfWork)
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

        public Lot GetLotIndex(int lotId)
        {
            Lot lot = this.unitOfWork.DbContext.Set<Lot>()
                .Include(l => l.Parts)
                .Include(l => l.Commits)
                .Include(l => l.Set)
                .Include(l => l.Set.SetParts)
                .SingleOrDefault(l => l.LotId == lotId);

            if (lot == null)
            {
                throw new Exception(string.Format("Cannot find lot with id: {0}", lotId));
            }

            Commit commit = this.unitOfWork.DbContext.Set<Commit>()
                .Include(c => c.PartVersions)
                .Where(c => c.LotId == lotId && c.IsIndex == true)
                .SingleOrDefault();

            commit.IsLoaded = true;

            return lot;
        }

        public Lot GetLot(int lotId, int? commitId = null)
        {
            Lot lot = this.unitOfWork.DbContext.Set<Lot>()
                .Include(l => l.Parts)
                .Include(l => l.Commits)
                .Include(l => l.Set)
                .Include(l => l.Set.SetParts)
                .SingleOrDefault(l => l.LotId == lotId);

            if (lot == null)
            {
                throw new Exception(string.Format("Cannot find lot with id: {0}", lotId));
            }

            Commit commit;
            if (commitId.HasValue)
            {
                commit = this.unitOfWork.DbContext.Set<Commit>()
                    .Include(c => c.PartVersions)
                    .SingleOrDefault(c => c.CommitId == commitId);
                if (commit.LotId != lotId)
                {
                    throw new Exception(string.Format("The specified commit with id {0} does not belong to lot with id {1}", commitId, lotId));
                }
            }
            else
            {
                commit = this.unitOfWork.DbContext.Set<Commit>()
                    .Where(c => c.LotId == lotId && c.IsIndex == true)
                    .Select(c => c.ParentCommit)
                    .SingleOrDefault();

                if (commit == null)
                {
                    throw new Exception(string.Format("The specified lot with id {0} does not have a non-index commit.", lotId));
                }

                this.unitOfWork.DbContext.Entry(commit).Collection(c => c.PartVersions).Load();
            }

            commit.IsLoaded = true;

            return lot;
        }

        public Commit LoadCommit(int? commitId)
        {
            if (!commitId.HasValue)
            {
                throw new Exception("Invalid commit id");
            }

            var commit = this.unitOfWork.DbContext.Set<Commit>()
                .Include(c => c.PartVersions)
                .SingleOrDefault(c => c.CommitId == commitId);
            commit.IsLoaded = true;

            return commit;
        }
    }
}
