using Common.Data;
using Regs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return this.unitOfWork.DbContext.Set<Set>().Find(setId);
        }

        public Set GetSet(string alias)
        {
            return this.unitOfWork.DbContext.Set<Set>().FirstOrDefault(s => s.Alias == alias);
        }

        public Lot GetLot(int lotId, int? commitId = null)
        {
            if (commitId.HasValue)
            {
                Commit commit = this.unitOfWork.DbContext.Set<Commit>().Find(commitId);
                if (commit.LotId != lotId)
                {
                    throw new Exception(string.Format("The specified commit with id {0} does not belong to lot with id {1}", commitId, lotId));
                }
            }
            else
            {
                this.unitOfWork.DbContext.Set<Commit>().Where(c => c.LotId == lotId).OrderByDescending(c => c.CommitDate).FirstOrDefault();
            }

            return this.unitOfWork.DbContext.Set<Lot>().Find(lotId);
        }
    }
}
