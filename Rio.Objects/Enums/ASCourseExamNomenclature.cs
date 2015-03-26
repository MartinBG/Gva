using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ASCourseExamNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature ModuleOneMaths = new BaseNomenclature("01", "МОДУЛ 1 МАТЕМАТИКА", "");
        public static readonly BaseNomenclature ModuleTwoPhysics = new BaseNomenclature("02", "МОДУЛ 2 ФИЗИКА", "");
        public static readonly BaseNomenclature ModuleThreeElectricityFundamentals = new BaseNomenclature("03", "МОДУЛ 3 ОСНОВИ НА ЕЛЕКТРИЧЕСТВОТО", "");
        public static readonly BaseNomenclature ElectronicsFundamentals = new BaseNomenclature("04", "ОСНОВИ НА ЕЛЕКТРОНИКАТА", "");
        public static readonly BaseNomenclature DigitalEquipmentElectronicInstrumentSystems = new BaseNomenclature("05", "ЦИФРОВА ТЕХНИКА, ЕЛЕКТРОННО-ПРИБОРНИ СИСТЕМИ", "");
        public static readonly BaseNomenclature MaterialsAccessories = new BaseNomenclature("06", "МАТЕРИАЛИ И ПРИНАДЛЕЖНОСТИ", "");
        public static readonly BaseNomenclature Maintenance = new BaseNomenclature("07", "ТЕХНИЧЕСКО ОБСЛУЖВАНЕ", "");
        public static readonly BaseNomenclature AerodynamicsBasics = new BaseNomenclature("08", "ОСНОВИ НА АЕРОДИНАМИКАТА", "");
        public static readonly BaseNomenclature HumanFactor = new BaseNomenclature("09", "ЧОВЕШКИ ФАКТОР", "");
        public static readonly BaseNomenclature AviationLegislation = new BaseNomenclature("10", "АВИАЦИОННА НОРМАТИВНА УРЕДБА", "");
        public static readonly BaseNomenclature TurbineAeroplanesAerodynamicsStructuresSystems = new BaseNomenclature("11A", "АЕРОДИНАМИКА, КОНСТРУКЦИЯ И СИСТЕМИ НА САМОЛЕТИТЕ С ТУРБИННИ ДВИГАТЕЛИ", "");
        public static readonly BaseNomenclature PistonAeroplanesAerodynamicsStructuresSystems = new BaseNomenclature("11B", "АЕРОДИНАМИКА, КОНСТРУКЦИЯ И СИСТЕМИ НА САМОЛЕТИТЕ С БУТАЛНИ ДВИГАТЕЛИ", "");
        public static readonly BaseNomenclature HelicoptersAerodynamicsStructuresSystems = new BaseNomenclature("12", "АЕРОДИНАМИКА, КОНСТРУКЦИЯ И СИСТЕМИ НА ВЕРТОЛЕТИТЕ", "");
        public static readonly BaseNomenclature AircraftAerodynamicsStructuresSystems = new BaseNomenclature("13", "АЕРОДИНАМИКА, КОНСТРУКЦИИ И СИСТЕМИ НА ВЪЗДУХОПЛАВАТЕЛНИТЕ СРЕДСТВА", "");
        public static readonly BaseNomenclature Propulsion = new BaseNomenclature("14", "СИЛОВИ УРЕДБИ", "");
        public static readonly BaseNomenclature TurbineEngines = new BaseNomenclature("15", "ГАЗОТУРБИННИ ДВИГАТЕЛИ", "");
        public static readonly BaseNomenclature PistonEngines = new BaseNomenclature("16", "БУТАЛНИ ДВИГАТЕЛИ", "");
        public static readonly BaseNomenclature Propellers = new BaseNomenclature("17", "ВИТЛА", "");

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
