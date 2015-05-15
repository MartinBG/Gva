using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class MESubsequentVerificationNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Periodic = new BaseNomenclature("01", "Периодична", "");
        public static readonly BaseNomenclature AfterRepair = new BaseNomenclature("02", "След ремонт", "");
        public static readonly BaseNomenclature DestroyedSign = new BaseNomenclature("03", "Унищожен знак от преходна проверка", "");
        public static readonly BaseNomenclature ByRequest = new BaseNomenclature("04", "По желание", "");

        public MESubsequentVerificationNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
               Periodic,
               AfterRepair,
               DestroyedSign,
               ByRequest
            };
        }
    }
}
