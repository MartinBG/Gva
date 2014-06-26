using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ProvidingServiceSubpartNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Subpart111 = new BaseNomenclature("111", "������� 1.1.1", "", ProvidingServicePartNomenclature.Part11.Value);
        public static readonly BaseNomenclature Subpart112 = new BaseNomenclature("112", "������� 1.1.2", "", ProvidingServicePartNomenclature.Part11.Value);
        public static readonly BaseNomenclature Subpart121 = new BaseNomenclature("121", "������� 1.2.1", "", ProvidingServicePartNomenclature.Part12.Value);
        public static readonly BaseNomenclature Subpart122 = new BaseNomenclature("122", "������� 1.2.2", "", ProvidingServicePartNomenclature.Part12.Value);
        public static readonly BaseNomenclature Subpart211 = new BaseNomenclature("211", "������� 2.1.1", "", ProvidingServicePartNomenclature.Part21.Value);
        public static readonly BaseNomenclature Subpart212 = new BaseNomenclature("212", "������� 2.1.2", "", ProvidingServicePartNomenclature.Part21.Value);
        public static readonly BaseNomenclature Subpart221 = new BaseNomenclature("221", "������� 2.2.1", "", ProvidingServicePartNomenclature.Part22.Value);
        public static readonly BaseNomenclature Subpart222 = new BaseNomenclature("222", "������� 2.2.2", "", ProvidingServicePartNomenclature.Part22.Value);

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
