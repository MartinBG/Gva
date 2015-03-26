using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class AeromedicalCenterNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Romanian = new BaseNomenclature("01", "ROMANIAN CIVIL AERONAUTICAL AUTHORITY-AME No.02", "");
        public static readonly BaseNomenclature Austro = new BaseNomenclature("02", "Austro Control", "");
        public static readonly BaseNomenclature Ame = new BaseNomenclature("03", "AME-SWETZERLAND", "");
        public static readonly BaseNomenclature Amc = new BaseNomenclature("04", "AMC PRAGUE", "");
        public static readonly BaseNomenclature Fr = new BaseNomenclature("05", "FR AMC", "");
        public static readonly BaseNomenclature Faa = new BaseNomenclature("06", "FAA", "");
        public static readonly BaseNomenclature Gcaa = new BaseNomenclature("07", "GCAA UAE", "");
        public static readonly BaseNomenclature Caa = new BaseNomenclature("08", "CAA France", "");
        public static readonly BaseNomenclature Gr = new BaseNomenclature("09", "GR AME", "");
        public static readonly BaseNomenclature Uk = new BaseNomenclature("10", "UK AME", "");
        public static readonly BaseNomenclature AmcZero = new BaseNomenclature("11", "AMC01", "");
        public static readonly BaseNomenclature TrAme = new BaseNomenclature("12", "TR-AME-008/2", "");
        public static readonly BaseNomenclature AmcLatvia = new BaseNomenclature("13", "AMC Latvia", "");

        public AeromedicalCenterNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Romanian,
                Austro,
                Ame,
                Amc,
                Fr,
                Faa,
                Gcaa,
                Caa,
                Gr,
                Uk,
                AmcZero,
                TrAme,
                AmcLatvia
            }.OrderBy(e=>e.Text).ToList();
        }
    }
}
