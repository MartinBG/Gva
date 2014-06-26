using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ASCourseExamNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature ModuleOneMaths = new BaseNomenclature("01", "лндск 1 люрелюрхйю", "");
        public static readonly BaseNomenclature ModuleTwoPhysics = new BaseNomenclature("02", "лндск 2 тхгхйю", "");
        public static readonly BaseNomenclature ModuleThreeElectricityFundamentals = new BaseNomenclature("03", "лндск 3 нямнбх мю екейрпхвеярбнрн", "");
        public static readonly BaseNomenclature ElectronicsFundamentals = new BaseNomenclature("04", "нямнбх мю екейрпнмхйюрю", "");
        public static readonly BaseNomenclature DigitalEquipmentElectronicInstrumentSystems = new BaseNomenclature("05", "жхтпнбю реумхйю, екейрпнммн-опханпмх яхярелх", "");
        public static readonly BaseNomenclature MaterialsAccessories = new BaseNomenclature("06", "люрепхюкх х опхмюдкефмнярх", "");
        public static readonly BaseNomenclature Maintenance = new BaseNomenclature("07", "реумхвеяйн наяксфбюме", "");
        public static readonly BaseNomenclature AerodynamicsBasics = new BaseNomenclature("08", "нямнбх мю юепндхмюлхйюрю", "");
        public static readonly BaseNomenclature HumanFactor = new BaseNomenclature("09", "внбеьйх тюйрнп", "");
        public static readonly BaseNomenclature AviationLegislation = new BaseNomenclature("10", "юбхюжхнммю мнплюрхбмю спедаю", "");
        public static readonly BaseNomenclature TurbineAeroplanesAerodynamicsStructuresSystems = new BaseNomenclature("11A", "юепндхмюлхйю, йнмярпсйжхъ х яхярелх мю яюлнкерхре я рспахммх дбхцюрекх", "");
        public static readonly BaseNomenclature PistonAeroplanesAerodynamicsStructuresSystems = new BaseNomenclature("11B", "юепндхмюлхйю, йнмярпсйжхъ х яхярелх мю яюлнкерхре я асрюкмх дбхцюрекх", "");
        public static readonly BaseNomenclature HelicoptersAerodynamicsStructuresSystems = new BaseNomenclature("12", "юепндхмюлхйю, йнмярпсйжхъ х яхярелх мю бепрнкерхре", "");
        public static readonly BaseNomenclature AircraftAerodynamicsStructuresSystems = new BaseNomenclature("13", "юепндхмюлхйю, йнмярпсйжхх х яхярелх мю бзгдсунокюбюрекмхре япедярбю", "");
        public static readonly BaseNomenclature Propulsion = new BaseNomenclature("14", "яхкнбх спедах", "");
        public static readonly BaseNomenclature TurbineEngines = new BaseNomenclature("15", "цюгнрспахммх дбхцюрекх", "");
        public static readonly BaseNomenclature PistonEngines = new BaseNomenclature("16", "асрюкмх дбхцюрекх", "");
        public static readonly BaseNomenclature Propellers = new BaseNomenclature("17", "бхркю", "");

        public ASCourseExamNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                ModuleOneMaths,
                ModuleTwoPhysics,
                ModuleThreeElectricityFundamentals,
                ElectronicsFundamentals,
                DigitalEquipmentElectronicInstrumentSystems,
                MaterialsAccessories,
                Maintenance,
                AerodynamicsBasics,
                HumanFactor,
                AviationLegislation,
                TurbineAeroplanesAerodynamicsStructuresSystems,
                PistonAeroplanesAerodynamicsStructuresSystems,
                HelicoptersAerodynamicsStructuresSystems,
                AircraftAerodynamicsStructuresSystems,
                Propulsion,
                TurbineEngines,
                PistonEngines,
                Propellers
            }.OrderBy(e => e.Text).ToList();
        }
    }
}
