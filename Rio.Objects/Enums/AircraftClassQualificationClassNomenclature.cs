using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class AircraftClassQualificationClassNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature ClassAirplaneMultiplePistonEngines = new BaseNomenclature("ME-L", "Клас самолети с повече от един бутален двигател - кацащи и излитащи от земна повърхност", "");
        public static readonly BaseNomenclature MEPLand = new BaseNomenclature("MEP(land)", "MEP(land)", "");
        public static readonly BaseNomenclature MEPLandIR = new BaseNomenclature("MEP(land)/IR", "MEP(land)/IR", "");
        public static readonly BaseNomenclature MEPLandIRSP = new BaseNomenclature("MEP(land)/IR(SP)", "MEP(land)/IR(SP)", "");
        public static readonly BaseNomenclature MEPSea = new BaseNomenclature("MEP(sea)", "MEP(sea)", "");
        public static readonly BaseNomenclature MEPL = new BaseNomenclature("MEP-L", "MEP-L", "");
        public static readonly BaseNomenclature MEPS = new BaseNomenclature("MEP-S", "MEP-S", "");
        public static readonly BaseNomenclature MHG = new BaseNomenclature("MHG", "MHG", "");
        public static readonly BaseNomenclature SEPLand = new BaseNomenclature("SEP(land)", "SEP(land)", "");
        public static readonly BaseNomenclature SEPSea = new BaseNomenclature("SEP(sea)", "SEP(sea)", "");
        public static readonly BaseNomenclature SEPL = new BaseNomenclature("SEP-L", "SEP-L", "");
        public static readonly BaseNomenclature SEPS = new BaseNomenclature("SEP-S", "SEP-S", "");
        public static readonly BaseNomenclature SES = new BaseNomenclature("SE-S", "SE-S", "");
        public static readonly BaseNomenclature SnowAyrecSET = new BaseNomenclature("Snow/Ayrec SET", "Snow/Ayrec SET", "");
        public static readonly BaseNomenclature STAL = new BaseNomenclature("STA-L", "STA-L", "");
        public static readonly BaseNomenclature TMG = new BaseNomenclature("TMG", "TMG", "");
        public static readonly BaseNomenclature UltralightAircrafts = new BaseNomenclature("ULA", "Свръхлеки самолети", "");
        public static readonly BaseNomenclature VeryLightAircrafts = new BaseNomenclature("VLA", "Много леки самолети", "");

        public List<BaseNomenclature> InstructorValues;

        public static readonly BaseNomenclature A = new BaseNomenclature("A", "A");
        public static readonly BaseNomenclature H = new BaseNomenclature("H", "H");
        public static readonly BaseNomenclature PL = new BaseNomenclature("PL", "PL");
        public static readonly BaseNomenclature As = new BaseNomenclature("As", "As");
        public static readonly BaseNomenclature B = new BaseNomenclature("B", "B");
        public static readonly BaseNomenclature S = new BaseNomenclature("S", "S");

        public static List<BaseNomenclature> R5196Values = new List<BaseNomenclature>()
        {
            A,
            H,
            As
        };

        public static readonly BaseNomenclature PPLA = new BaseNomenclature("PPL(A)", "PPL(A)");
        public static readonly BaseNomenclature PPLH = new BaseNomenclature("PPL(H)", "PPL(H)");
        public static readonly BaseNomenclature SPL = new BaseNomenclature("SPL", "SPL");
        public static readonly BaseNomenclature BPL = new BaseNomenclature("BPL", "BPL");
        public static readonly BaseNomenclature CPLA = new BaseNomenclature("CPL(A)", "CPL(A)");
        public static readonly BaseNomenclature CPLH = new BaseNomenclature("CPL(H)", "CPL(H)");
        public static readonly BaseNomenclature ATPLA = new BaseNomenclature("ATPL(A)", "ATPL(A)");
        public static readonly BaseNomenclature ATPLH = new BaseNomenclature("ATPL(H)", "ATPL(H)");
        public static readonly BaseNomenclature WithIR = new BaseNomenclature("WithIR", "С IR");
        public static readonly BaseNomenclature WihtoutIR = new BaseNomenclature("WihtoutIR", "Без IR");

        public static List<BaseNomenclature> R5246Values = new List<BaseNomenclature>()
        {
            PPLA,
            PPLH,
            SPL,
            BPL,
            CPLA,
            CPLH,
            ATPLA,
            ATPLH,
            WithIR,
            WihtoutIR
        };

        public AircraftClassQualificationClassNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                ClassAirplaneMultiplePistonEngines,
                MEPLand,
                MEPLandIR,
                MEPLandIRSP,
                MEPSea,
                MEPL,
                MEPS,
                MHG,
                SEPLand,
                SEPSea,
                SEPL,
                SEPS,
                SES,
                SnowAyrecSET,
                STAL,
                TMG,
                UltralightAircrafts,
                VeryLightAircrafts
            };

            this.InstructorValues = new List<BaseNomenclature>()
            {
                A,
                H,
                PL,
                As,
                B,
                S 
            };
        }

    }
}
