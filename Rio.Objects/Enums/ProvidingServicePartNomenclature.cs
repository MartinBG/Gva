using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ProvidingServicePartNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Part11 = new BaseNomenclature("11", "Част 1.1", "", ProvidingServiceKindNomenclature.Kind1.Value);
        public static readonly BaseNomenclature Part12 = new BaseNomenclature("12", "Част 1.2", "", ProvidingServiceKindNomenclature.Kind1.Value);
        public static readonly BaseNomenclature Part21 = new BaseNomenclature("21", "Част 2.1", "", ProvidingServiceKindNomenclature.Kind2.Value);
        public static readonly BaseNomenclature Part22 = new BaseNomenclature("22", "Част 2.2", "", ProvidingServiceKindNomenclature.Kind2.Value);

        public ProvidingServicePartNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Part11,
                Part12,
                Part21,
                Part22
            };
        }
    }
}
