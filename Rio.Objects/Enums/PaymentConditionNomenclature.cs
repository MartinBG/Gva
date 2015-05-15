using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class PaymentConditionNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature InAdvance = new BaseNomenclature("01", "Предварително", "");
        public static readonly BaseNomenclature OnCheckPlace = new BaseNomenclature("02", "На мястото на проверката", "");

        public PaymentConditionNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                InAdvance,
                OnCheckPlace
            };
        }
    }
}
