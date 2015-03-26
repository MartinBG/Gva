using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class OperationalProgramNomenclature : BaseNomenclature
    {
        public OperationalProgramNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                new BaseNomenclature("01","Транспорт"),
                new BaseNomenclature("02","Околна среда"),
                new BaseNomenclature("03","Регионално развитие"),
                new BaseNomenclature("04","Конкурентоспособност"),
                new BaseNomenclature("05","Техническа помощ"),
                new BaseNomenclature("06","Развитие на човешките ресурси"),
                new BaseNomenclature("07","Административен капацитет")
            }.OrderBy(e=>e.Text).ToList();
        }
    }
}
