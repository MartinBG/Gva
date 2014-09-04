using System;
using System.Data.Entity;
using System.Linq;
using Common.Api.UserContext;
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

        public Lot CreateLot(string setAlias, UserContext userContext)
        {
            return this.CreateLot(this.GetSet(setAlias), userContext);
        }

        public Lot CreateLot(Set set, UserContext userContext)
        {
            Commit index = new Commit
            {
                CommitId = Commit.CommitSequence.NextValue(),
                CommiterId = userContext.UserId,
                CommitDate = DateTime.Now,
                IsIndex = true,
                IsLoaded = true
            };

            Lot lot = new Lot
            {
                NextIndex = 0,
                LotId = Lot.LotSequence.NextValue(),
                Set = set
            };
            lot.Commits.Add(index);

            this.unitOfWork.DbContext.Set<Lot>().Add(lot);

            return lot;
        }

        public Lot GetLotIndex(int lotId)
        {
            Lot lot = this.unitOfWork.DbContext.Set<Lot>()
                .Include(l => l.Commits)
                .Include(l => l.Set)
                .SingleOrDefault(l => l.LotId == lotId);

            if (lot == null)
            {
                throw new Exception(string.Format("Cannot find lot with id: {0}", lotId));
            }

            this.unitOfWork.DbContext.Set<SetPart>()
                .Where(sp => sp.SetId == lot.SetId)
                .Load();

            this.unitOfWork.DbContext.Set<Part>()
                .Where(p => p.LotId == lotId)
                .Load();

            var indexCommitId = lot.Commits.Where(c => c.IsIndex).Single().CommitId;

            Commit commit = this.LoadCommit(indexCommitId);

            return lot;
        }

        public Lot GetLot(int lotId, int? commitId = null)
        {
            Lot lot = this.unitOfWork.DbContext.Set<Lot>()
                .Include(l => l.Commits)
                .Include(l => l.Set)
                .SingleOrDefault(l => l.LotId == lotId);

            if (lot == null)
            {
                throw new Exception(string.Format("Cannot find lot with id: {0}", lotId));
            }

            this.unitOfWork.DbContext.Set<SetPart>()
                .Where(sp => sp.SetId == lot.SetId)
                .Load();

            this.unitOfWork.DbContext.Set<Part>()
                .Where(p => p.LotId == lotId)
                .Load();

            Commit commit;
            if (commitId.HasValue)
            {
                commit = this.LoadCommit(commitId);

                if (commit.LotId != lotId)
                {
                    throw new Exception(string.Format("The specified commit with id {0} does not belong to lot with id {1}", commitId, lotId));
                }
            }
            else
            {
                var lastCommitId = lot.Commits.Where(c => c.IsIndex).Single().ParentCommitId;

                if (lastCommitId == null)
                {
                    throw new Exception(string.Format("The specified lot with id {0} does not have a non-index commit.", lotId));
                }

                commit = this.LoadCommit(lastCommitId);
            }

            return lot;
        }

        public Commit LoadCommit(int? commitId)
        {
            if (!commitId.HasValue)
            {
                throw new Exception("Invalid commit id");
            }

            var commit = this.unitOfWork.DbContext.Set<Commit>()
                .Include(c => c.CommitVersions)
                .Include(c => c.CommitVersions.Select(cv => cv.PartVersion))
                .Include(c => c.CommitVersions.Select(cv => cv.OldPartVersion))
                .SingleOrDefault(c => c.CommitId == commitId);
            commit.IsLoaded = true;

            return commit;
        }
    }
}
