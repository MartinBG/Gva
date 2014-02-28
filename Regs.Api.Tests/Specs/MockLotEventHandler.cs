using Common.Data;
using Regs.Api.LotEvents;
using Regs.Api.Tests.Common;

namespace Regs.Api.Tests.Specs
{
    public class MockLotEventHandler : ILotEventHandler
    {
        private IUnitOfWork unitOfWork;

        public MockLotEventHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEvent Event { get; set; }

        public void Handle(IEvent e)
        {
            this.Event = e;
        }
    }
}
