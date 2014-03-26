using Common.Data;

namespace Regs.Api.LotEvents
{
    public interface ILotEventDispatcher
    {
        void Dispatch(ILotEvent e);
    }
}
