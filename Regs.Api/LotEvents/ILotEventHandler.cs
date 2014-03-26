using Common.Data;

namespace Regs.Api.LotEvents
{
    public interface ILotEventHandler
    {
        void Handle(ILotEvent e);
    }
}
