using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ATONomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature AvioVarna = new BaseNomenclature("01", "АВИООТРЯД ВАРНА ООД", "");
        public static readonly BaseNomenclature InstituteAirTransport = new BaseNomenclature("02", "ИНСТИТУТ ПО ВЪЗДУШЕН ТРАНСПОРТ ЕООД", "");
        public static readonly BaseNomenclature TransportSofia = new BaseNomenclature("03", "ЧАСТЕН ТРАНСПОРТЕН КОЛЕЖ ООД-СОФИЯ", "");
        public static readonly BaseNomenclature FortunaAir = new BaseNomenclature("04", "ФОРТУНА ЕЪР ЕООД", "");
        public static readonly BaseNomenclature HighSchoolAviation = new BaseNomenclature("05", "ВИСША ШКОЛА \"АВИАЦИЯ\"-ТЕХНИЧЕСКИ УНИВЕРСИТЕТ-СОФИЯ", "");
        public static readonly BaseNomenclature BreezeAviation = new BaseNomenclature("06", "БРИЗ АВИЕЙШЪН АД", "");
        public static readonly BaseNomenclature AirSport = new BaseNomenclature("07", "ЕЪР СПОРТ ООД", "");
        public static readonly BaseNomenclature AirScorpio = new BaseNomenclature("08", "ЕЪР СКОРПИО ЕООД", "");
        public static readonly BaseNomenclature Ratan = new BaseNomenclature("09", "РАТАН ООД", "");
        public static readonly BaseNomenclature OlimpiaAir = new BaseNomenclature("10", "ОЛИМПИЯ ЕР ЕООД", "");
        public static readonly BaseNomenclature AirCities = new BaseNomenclature("11", "ЕЪР СИТИС ЕООД", "");
        public static readonly BaseNomenclature SkyVictory = new BaseNomenclature("12", "СКАЙ ВИКТОРИ ЕООД", "");

        public ATONomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                AvioVarna,
                InstituteAirTransport,
                TransportSofia,
                FortunaAir,
                HighSchoolAviation,
                BreezeAviation,
                AirSport,
                AirScorpio,
                Ratan,
                OlimpiaAir,
                AirCities,
                SkyVictory
            }.OrderBy(e => e.Text).ToList();
        }
    }
}
