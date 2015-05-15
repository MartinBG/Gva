using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ElectrometerTypeNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature SinglePhase = new BaseNomenclature("01", "Електромер - еднофазен", "");
        public static readonly BaseNomenclature TriplePhase = new BaseNomenclature("02", "Електромер - трифазен", "");
        public static readonly BaseNomenclature TriplePhaseCombination = new BaseNomenclature("03", "Електромер - трифазен комбиниран", "");

        public ElectrometerTypeNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
               SinglePhase,
               TriplePhase,
               TriplePhaseCombination
            };
        }
    }
}
