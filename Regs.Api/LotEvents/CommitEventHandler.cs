using System;
using System.Linq;
using System.Linq.Expressions;
using Common.Data;
using Regs.Api.Models;

namespace Regs.Api.LotEvents
{
    public abstract class CommitEventHandler : ILotEventHandler
    {
        public CommitEventHandler(bool isPrincipal)
        {
            this.IsPrincipal = isPrincipal;
        }

        public bool IsPrincipal { get; private set; }

        public abstract void Handle(ILotEvent e);
    }
}
