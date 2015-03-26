using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ProvidingServiceSubpartNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Subpart111 = new BaseNomenclature("111", "Подчаст 1.1.1", "", ProvidingServicePartNomenclature.Part11.Value);
        public static readonly BaseNomenclature Subpart112 = new BaseNomenclature("112", "Подчаст 1.1.2", "", ProvidingServicePartNomenclature.Part11.Value);
        public static readonly BaseNomenclature Subpart121 = new BaseNomenclature("121", "Подчаст 1.2.1", "", ProvidingServicePartNomenclature.Part12.Value);
        public static readonly BaseNomenclature Subpart122 = new BaseNomenclature("122", "Подчаст 1.2.2", "", ProvidingServicePartNomenclature.Part12.Value);
        public static readonly BaseNomenclature Subpart211 = new BaseNomenclature("211", "Подчаст 2.1.1", "", ProvidingServicePartNomenclature.Part21.Value);
        public static readonly BaseNomenclature Subpart212 = new BaseNomenclature("212", "Подчаст 2.1.2", "", ProvidingServicePartNomenclature.Part21.Value);
        public static readonly BaseNomenclature Subpart221 = new BaseNomenclature("221", "Подчаст 2.2.1", "", ProvidingServicePartNomenclature.Part22.Value);
        public static readonly BaseNomenclature Subpart222 = new BaseNomenclature("222", "Подчаст 2.2.2", "", ProvidingServicePartNomenclature.Part22.Value);

        public ProvidingServiceSubpartNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Subpart111,
                Subpart112,
                Subpart121,
                Subpart122,
                Subpart211,
                Subpart212,
                Subpart221,
                Subpart222
            };
        }
    }
}
