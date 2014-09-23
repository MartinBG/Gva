using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Common.Api.UserContext;
using Common.Data;
using Common.Extensions;
using Regs.Api.Models;

namespace Regs.Api.Repositories.LotRepositories
{
    public class LotRepository : ILotRepository
    {
        private const int ReadPermissionId = 1;

        private IUnitOfWork unitOfWork;
        private UserContext userContext;

        public LotRepository(IUnitOfWork unitOfWork, UserContext userContext)
        {
            this.unitOfWork = unitOfWork;
            this.userContext = userContext;
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

        public Lot CreateLot(string setAlias)
        {
            return this.CreateLot(this.GetSet(setAlias));
        }

        public Lot CreateLot(Set set)
        {
            Commit index = new Commit
            {
                CommitId = Commit.CommitSequence.NextValue(),
                CommiterId = this.userContext.UserId,
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

        public Lot GetLotIndex(int lotId, bool fullAccess = false)
        {
            Lot lot = this.unitOfWork.DbContext.Set<Lot>()
                .Include(l => l.Commits)
                .Include(l => l.Set)
                .SingleOrDefault(l => l.LotId == lotId);

            if (lot == null)
            {
                throw new Exception(string.Format("Cannot find lot with id: {0}", lotId));
            }

            this.LoadParts(lot, fullAccess);

            var indexCommitId = lot.Commits.Where(c => c.IsIndex).Single().CommitId;

            Commit commit = this.LoadCommit(lot, indexCommitId, fullAccess);

            return lot;
        }

        public Lot GetLot(int lotId, int? commitId = null, bool fullAccess = false)
        {
            Lot lot = this.unitOfWork.DbContext.Set<Lot>()
                .Include(l => l.Commits)
                .Include(l => l.Set)
                .SingleOrDefault(l => l.LotId == lotId);

            if (lot == null)
            {
                throw new Exception(string.Format("Cannot find lot with id: {0}", lotId));
            }

            this.LoadParts(lot, fullAccess);

            Commit commit;
            if (commitId.HasValue)
            {
                commit = this.LoadCommit(lot, commitId, fullAccess);

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

                commit = this.LoadCommit(lot, lastCommitId, fullAccess);
            }

            return lot;
        }

        public Commit LoadCommit(Lot lot, int? commitId, bool fullAccess = false)
        {
            if (!commitId.HasValue)
            {
                throw new Exception("Invalid commit id");
            }

            var commitData = 
                this.unitOfWork.DbContext.Set<CommitVersion>()
                .Include(cv => cv.PartVersion)
                .Include(cv => cv.OldPartVersion)
                .Where(cv => cv.CommitId == commitId);

            if (!fullAccess)
            {
                int[] partIds = lot.Parts.Select(p => p.PartId).ToArray();

                commitData = commitData
                    .Where(cv => partIds.Contains(cv.PartVersion.PartId));
            }

            commitData.Load();

            var commit = this.unitOfWork.DbContext.Set<Commit>().Find(commitId.Value);

            commit.IsLoaded = true;

            return commit;
        }

        public void ExecSpSetLotPartTokens(int lotPartId)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("LotPartId", Helper.CastToSqlDbValue(lotPartId)));

            this.unitOfWork.DbContext.Database.ExecuteSqlCommand("spSetLotPartTokens @LotPartId", parameters.ToArray());
        }

        public void ExecSpRebuildLotPartTokens()
        {
            this.unitOfWork.DbContext.Database.ExecuteSqlCommand("spRebuildLotPartTokens");
        }

        private void LoadParts(Lot lot, bool fullAccess)
        {
            this.unitOfWork.DbContext.Set<SetPart>()
                .Where(sp => sp.SetId == lot.SetId)
                .Load();

            var partData =
                this.unitOfWork.DbContext.Set<Part>()
                .Where(p => p.LotId == lot.LotId);

            if (!fullAccess)
            {
                partData = partData.Where(p => p.PartUsers.Any(v => v.ClassificationPermissionId == ReadPermissionId && v.UserId == this.userContext.UserId));
            }

            partData.Load();
        }
    }
}
