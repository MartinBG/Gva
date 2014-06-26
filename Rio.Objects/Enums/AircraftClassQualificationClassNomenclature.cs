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
        }
    }
}
