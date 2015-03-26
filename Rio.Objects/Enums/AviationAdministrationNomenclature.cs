using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class AviationAdministrationNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature BG = new BaseNomenclature("BG", "ГЛАВНА ДИРЕКЦИЯ ГРАЖДАНСКА ВЪЗДУХОПЛАВАТЕЛНА АДМИНИСТРАЦИЯ", "");
        public static readonly BaseNomenclature A = new BaseNomenclature("A", "Austria", "");
        public static readonly BaseNomenclature B = new BaseNomenclature("B", "Belgium", "");
        public static readonly BaseNomenclature CY = new BaseNomenclature("CY", "Cyprus", "");
        public static readonly BaseNomenclature CZ = new BaseNomenclature("CZ", "Czech Republic", "");
        public static readonly BaseNomenclature DK = new BaseNomenclature("DK", "Denmark", "");
        public static readonly BaseNomenclature EST = new BaseNomenclature("EST", "Estonia", "");
        public static readonly BaseNomenclature FIN = new BaseNomenclature("FIN", "Finland", "");
        public static readonly BaseNomenclature FYROM = new BaseNomenclature("FYROM", "Former Yugoslav Republic of Macedonia", "");
        public static readonly BaseNomenclature F = new BaseNomenclature("F", "France", "");
        public static readonly BaseNomenclature D = new BaseNomenclature("D", "Germany", "");
        public static readonly BaseNomenclature GR = new BaseNomenclature("GR", "Greece", "");
        public static readonly BaseNomenclature H = new BaseNomenclature("H", "Hungary", "");
        public static readonly BaseNomenclature IS = new BaseNomenclature("IS", "Iceland", "");
        public static readonly BaseNomenclature IRL = new BaseNomenclature("IRL", "Ireland", "");
        public static readonly BaseNomenclature I = new BaseNomenclature("I", "Italy", "");
        public static readonly BaseNomenclature LVA = new BaseNomenclature("LVA", "Latvia", "");
        public static readonly BaseNomenclature L = new BaseNomenclature("L", "Luxembourg", "");
        public static readonly BaseNomenclature M = new BaseNomenclature("M", "Malta", "");
        public static readonly BaseNomenclature MD = new BaseNomenclature("MD", "Moldova", "");
        public static readonly BaseNomenclature MC = new BaseNomenclature("MC", "Monaco", "");
        public static readonly BaseNomenclature NL = new BaseNomenclature("NL", "Netherlands", "");
        public static readonly BaseNomenclature N = new BaseNomenclature("N", "Norway", "");
        public static readonly BaseNomenclature PL = new BaseNomenclature("PL", "Poland", "");
        public static readonly BaseNomenclature P = new BaseNomenclature("P", "Portugal", "");
        public static readonly BaseNomenclature R = new BaseNomenclature("R", "Romania", "");
        public static readonly BaseNomenclature SK = new BaseNomenclature("SK", "Slovak Republic", "");
        public static readonly BaseNomenclature SLO = new BaseNomenclature("SLO", "Slovenia", "");
        public static readonly BaseNomenclature E = new BaseNomenclature("E", "Spain", "");
        public static readonly BaseNomenclature SE = new BaseNomenclature("SE", "Sweden", "");
        public static readonly BaseNomenclature CH = new BaseNomenclature("CH", "Switzerland", "");
        public static readonly BaseNomenclature TR = new BaseNomenclature("TR", "Turkey", "");
        public static readonly BaseNomenclature UK = new BaseNomenclature("UK", "United Kingdom Civil Aviation Authority", "");
        public static readonly BaseNomenclature IR = new BaseNomenclature("IR", "I.R. of IRAN", "");
        public static readonly BaseNomenclature AL = new BaseNomenclature("AL", "DGCAA Albania", "");
        public static readonly BaseNomenclature FAA = new BaseNomenclature("FAA", "FAA", "");
        public static readonly BaseNomenclature CAD = new BaseNomenclature("CAD SRB", "Civil Aviation Directorate of the Republic of Serbia", "");
        public static readonly BaseNomenclature UKR = new BaseNomenclature("UKR", "CAA of Ukraine", "");
        public static readonly BaseNomenclature CAAS = new BaseNomenclature("CAAS", "Civil aviation Authority of Singapore", "");
        public static readonly BaseNomenclature NAM = new BaseNomenclature("NAM", "DCA NAMIBIA", "");
        public static readonly BaseNomenclature CADCan = new BaseNomenclature("CAD", "Civil Aviation Department", "");
        public static readonly BaseNomenclature CA = new BaseNomenclature("CA", "Canada", "");
        public static readonly BaseNomenclature DGCA = new BaseNomenclature("DGCA", "DGCA", "");
        public static readonly BaseNomenclature RU = new BaseNomenclature("RU", "Федерална агенция за въздушен траспорт", "");
        public static readonly BaseNomenclature CAAC = new BaseNomenclature("CAAC", "Гражданска въздухоплавателна администрация Китай", "");
        public static readonly BaseNomenclature MNE = new BaseNomenclature("MNE", "Civil Aviation Agency-Montenegro", "");
        public static readonly BaseNomenclature NCAA = new BaseNomenclature("NCAA", "NCAA of Brazil", "");
        public static readonly BaseNomenclature CAA = new BaseNomenclature("CAA", "Eritrea's Civil Aviation Authority", "");
        public static readonly BaseNomenclature DCA = new BaseNomenclature("DCA", "Department of Civil Aviation, Myanmar", "");
        public static readonly BaseNomenclature CAAV = new BaseNomenclature("CAAV", "Глажданска въздухоплавателна администрация Виетнам", "");
        public static readonly BaseNomenclature LCAA = new BaseNomenclature("LCAA", "Lithuania Civil Aviation Administration", "");

        public AviationAdministrationNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                BG,
                A,
                B,
                CY,
                CZ,
                DK,
                EST,
                FIN,
                FYROM,
                F,
                D,
                GR,
                H,
                IS,
                IRL,
                I,
                LVA,
                L,
                M,
                MD,
                MC,
                NL,
                N,
                PL,
                P,
                R,
                SK,
                SLO,
                E,
                SE,
                CH,
                TR,
                UK,
                IR,
                AL,
                FAA,
                CAD,
                UKR,
                CAAS,
                NAM,
                CADCan,
                CA,
                DGCA,
                RU,
                CAAC,
                MNE,
                NCAA,
                CAA,
                DCA,
                CAAV,
                LCAA  
            };
        }
    }
}
