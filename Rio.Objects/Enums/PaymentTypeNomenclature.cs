using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class PaymentTypeNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Cash = new BaseNomenclature("01", "В брой", "");
        public static readonly BaseNomenclature Bank = new BaseNomenclature("02", "По банков път", "");

        public PaymentTypeNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Cash,
                Bank
            };
        }
    }
}

