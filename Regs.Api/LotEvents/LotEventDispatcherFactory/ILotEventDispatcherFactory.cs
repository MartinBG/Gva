using Regs.Api.LotEvents.LotEventDispatcher;

namespace Regs.Api.LotEvents.LotEventDispatcherFactory
{
    public interface ILotEventDispatcherFactory
    {
        ILotEventDispatcher ForSet(string setAlias);
    }
}
