using Common.Data;
using Regs.Api.LotEvents;

namespace Regs.Api.Tests.Mocks
{
    public class MockLotEventHandler : ILotEventHandler
    {
        private IUnitOfWork unitOfWork;

        public MockLotEventHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ILotEvent Event { get; set; }

        public void Handle(ILotEvent e)
        {
            this.Event = e;
        }
    }
}
