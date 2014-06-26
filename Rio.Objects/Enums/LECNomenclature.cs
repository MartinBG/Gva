using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class LECNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Aerospase = new BaseNomenclature("01", "ST Aerospase", "");
        public static readonly BaseNomenclature Civil = new BaseNomenclature("02", "Japan Civil Aviation Authority", "");
        public static readonly BaseNomenclature Global = new BaseNomenclature("03", "GLOBAL MAINTENANCE", "");
        public static readonly BaseNomenclature Ava = new BaseNomenclature("04", "AVA FLYING CENTER", "");
        public static readonly BaseNomenclature Etihad = new BaseNomenclature("05", "ETIHAD AIRWAYS", "");
        public static readonly BaseNomenclature Hellenic = new BaseNomenclature("06", "HELLENIC CIVIL AVIATION AUTHORITY", "");
        public static readonly BaseNomenclature Falcon = new BaseNomenclature("07", "FALCON TRAINING CENTER", "");
        public static readonly BaseNomenclature Simcom = new BaseNomenclature("08", "SIMCOM TRAINING CENTER", "");


        public LECNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Aerospase,
                Civil,
                Global,
                Ava,
                Etihad,
                Hellenic,
                Falcon,
                Simcom
            }.OrderBy(e => e.Text).ToList();
        }
    }
}
