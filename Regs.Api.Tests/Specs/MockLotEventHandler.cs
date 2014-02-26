using Common.Data;
using Regs.Api.LotEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
