using System;
using Regs.Api.Models;

namespace Regs.Api.LotEvents
{
    public interface IProjection
    {
        void RebuildLot(Lot lot);
    }
}
