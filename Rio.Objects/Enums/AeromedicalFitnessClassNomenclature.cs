using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class AeromedicalFitnessClassNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Class1 = new BaseNomenclature("01", "Class-1", "");
        public static readonly BaseNomenclature Class2 = new BaseNomenclature("02", "Class-2", "");
        public static readonly BaseNomenclature Class3 = new BaseNomenclature("03", "Class-3", "");
        public static readonly BaseNomenclature Class4 = new BaseNomenclature("04", "Class-4", "");

        public AeromedicalFitnessClassNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
               Class1,
               Class2,
               Class3,
               Class4
            };
        }
    }
}
