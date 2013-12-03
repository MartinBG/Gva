﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Data;
using Regs.Api.Models;

namespace Regs.Api.Managers.LotManager
{
    public class LotManager : ILotManager
    {
        private IUnitOfWork unitOfWork;

        public LotManager(IUnitOfWork unitOfWork, IEnumerable<IEventHandler> eventHandlers)
        {
            this.unitOfWork = unitOfWork;

            foreach (var eventHandler in eventHandlers)
            {
                Events.Register(eventHandler);
            }
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
            Lot lot = this.unitOfWork.DbContext.Set<Lot>()
                .Include(l => l.Parts)
                .SingleOrDefault(l => l.LotId == lotId);

            if (lot == null)
            {
                throw new Exception(string.Format("Invalid lotId: {0}", lotId));
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
                    .Include(c => c.PartVersions)
                    .Where(c => c.LotId == lotId)
                    .SingleOrDefault(c => c.IsIndex == true);
            }

            this.unitOfWork.DbContext.Set<PartVersion>()
                .Include(pv => pv.TextBlob)
                .Where(pv => pv.Commits.Any(c => c.CommitId == commit.CommitId))
                .Load();

            return lot;
        }
    }
}
