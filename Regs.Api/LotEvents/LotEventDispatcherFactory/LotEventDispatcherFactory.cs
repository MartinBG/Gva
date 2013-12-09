using Regs.Api.LotEvents.LotEventDispatcher;

namespace Regs.Api.LotEvents.LotEventDispatcherFactory
{
    public class LotEventDispatcherFactory : ILotEventDispatcherFactory
    {
        public ILotEventDispatcher ForSet(string setAlias)
        {
            ILotEventDispatcher lotEventDispatcher = new LotEventDispatcher.LotEventDispatcher() { SetAlias = setAlias };

            return lotEventDispatcher;
        }
    }
}
