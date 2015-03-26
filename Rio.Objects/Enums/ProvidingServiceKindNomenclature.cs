using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ProvidingServiceKindNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Kind1 = new BaseNomenclature("01", "Вид 1");
        public static readonly BaseNomenclature Kind2 = new BaseNomenclature("02", "Вид 2");

        public ProvidingServiceKindNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Kind1,
                Kind2
            };
        }
    }
}
