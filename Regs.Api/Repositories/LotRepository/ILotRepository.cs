using Common.Api.UserContext;
using Regs.Api.Models;

namespace Regs.Api.Repositories.LotRepositories
{
    public interface ILotRepository
    {
        Set GetSet(int setId);

        Set GetSet(string alias);

        Lot CreateLot(string setAlias);

        Lot CreateLot(Set set);

        Lot GetLot(int lotId, int? commitId = null, bool fullAccess = false);

        Lot GetLotIndex(int lotId, bool fullAccess = false);

        Commit LoadCommit(Lot lot, int? commitId, bool fullAccess = false);

        void ExecSpSetLotPartTokens(int lotPartId);

        void ExecSpRebuildLotPartTokens();
    }
}
