using Regs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regs.Api.LotManager
{
    public interface ILotManager
    {
        Set GetSet(int setId);
        Set GetSet(string alias);
        Lot GetLot(int lotId, int? commitId = null);
    }
}
