using Regs.Api.Models;

namespace Regs.Api.Managers.LotManager
{
    public interface ILotManager
    {
        Set GetSet(int setId);
        Set GetSet(string alias);
        Lot GetLot(int lotId, int? commitId = null);
    }
}
