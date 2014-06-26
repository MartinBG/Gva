using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class OVDBodyLocationIndicatorNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Bourgas = new BaseNomenclature("01", "LBBG ������", "");
        public static readonly BaseNomenclature Bohot = new BaseNomenclature("02", "LBBO �����-LM", "");
        public static readonly BaseNomenclature DolnaBania = new BaseNomenclature("03", "LBDB ����� ����", "");
        public static readonly BaseNomenclature GornaOriahovica = new BaseNomenclature("04", "LBGO ����� ����������", "");
        public static readonly BaseNomenclature Grivica = new BaseNomenclature("05", "LBGR �������", "");
        public static readonly BaseNomenclature Ihtiman = new BaseNomenclature("06", "LBHT �������", "");
        public static readonly BaseNomenclature Kainardja = new BaseNomenclature("07", "LBKJ ���������", "");
        public static readonly BaseNomenclature Kalvacha = new BaseNomenclature("08", "LBKL �������", "");
        public static readonly BaseNomenclature Lozen = new BaseNomenclature("09", "LBLN �����", "");
        public static readonly BaseNomenclature Lesnovo = new BaseNomenclature("10", "LBLS �������", "");
        public static readonly BaseNomenclature Plovdiv = new BaseNomenclature("11", "LBPD �������", "");
        public static readonly BaseNomenclature Primorsko = new BaseNomenclature("12", "LBPR ���������", "");
        public static readonly BaseNomenclature Erden = new BaseNomenclature("13", "LBRD �����", "");
        public static readonly BaseNomenclature Ruse = new BaseNomenclature("14", "LBRS ����", "");
        public static readonly BaseNomenclature Sofia = new BaseNomenclature("15", "LBSF �����", "");
        public static readonly BaseNomenclature SofiaRPI = new BaseNomenclature("16", "LBSR ����� ���", "");
        public static readonly BaseNomenclature Striama = new BaseNomenclature("17", "LBST ������", "");
        public static readonly BaseNomenclature StaraZagora = new BaseNomenclature("18", "LBSZ ����� ������", "");
        public static readonly BaseNomenclature Balchik = new BaseNomenclature("19", "LBWB ������", "");
        public static readonly BaseNomenclature Varna = new BaseNomenclature("20", "LBWN �����", "");
        public static readonly BaseNomenclature Izgrev = new BaseNomenclature("21", "LBWV ������", "");

        public OVDBodyLocationIndicatorNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Bourgas,
                Bohot,
                DolnaBania,
                GornaOriahovica,
                Grivica,
                Ihtiman,
                Kainardja,
                Kalvacha,
                Lozen,
                Lesnovo,
                Plovdiv,
                Primorsko,
                Erden,
                Ruse,
                Sofia,
                SofiaRPI,
                Striama,
                StaraZagora,
                Balchik,
                Varna,
                Izgrev,
            }.OrderBy(e => e.Text).ToList();
        }
    }
}
