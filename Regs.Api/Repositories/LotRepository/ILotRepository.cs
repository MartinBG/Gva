using Regs.Api.Models;

namespace Regs.Api.Repositories.LotRepositories
{
    public interface ILotRepository
    {
        Set GetSet(int setId);

        Set GetSet(string alias);

        Lot GetLot(int lotId, int? commitId = null);

        Lot GetLotIndex(int lotId);

        Commit LoadCommit(int? commitId);
    }
}
