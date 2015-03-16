using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class AnnouncedActKindNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature FinancialStatement = new BaseNomenclature("1", "Годишен финансов отчет");
        public static readonly BaseNomenclature Report = new BaseNomenclature("2", "Годишен доклад");

        public AnnouncedActKindNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                FinancialStatement,
                Report
            };
        }
    }
}
