using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class TypeFiscalDeviceNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature EKAFP = new BaseNomenclature("01", "ЕКАФП", "");
        public static readonly BaseNomenclature FPR = new BaseNomenclature("02", "ФПР", "");
        public static readonly BaseNomenclature ESFP = new BaseNomenclature("03", "ЕСФП", "");
        public static readonly BaseNomenclature Net = new BaseNomenclature("04", "мрежа", "");
        public static readonly BaseNomenclature IAS = new BaseNomenclature("05", "интегрирана автоматизирана система", "");

        public TypeFiscalDeviceNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
               EKAFP,
               FPR,
               ESFP,
               Net,
               IAS
            };
        }
    }
}

