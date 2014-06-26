using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ASCourseExamNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature ModuleOneMaths = new BaseNomenclature("01", "����� 1 ����������", "");
        public static readonly BaseNomenclature ModuleTwoPhysics = new BaseNomenclature("02", "����� 2 ������", "");
        public static readonly BaseNomenclature ModuleThreeElectricityFundamentals = new BaseNomenclature("03", "����� 3 ������ �� ���������������", "");
        public static readonly BaseNomenclature ElectronicsFundamentals = new BaseNomenclature("04", "������ �� �������������", "");
        public static readonly BaseNomenclature DigitalEquipmentElectronicInstrumentSystems = new BaseNomenclature("05", "������� �������, ����������-�������� �������", "");
        public static readonly BaseNomenclature MaterialsAccessories = new BaseNomenclature("06", "��������� � ��������������", "");
        public static readonly BaseNomenclature Maintenance = new BaseNomenclature("07", "���������� ����������", "");
        public static readonly BaseNomenclature AerodynamicsBasics = new BaseNomenclature("08", "������ �� ��������������", "");
        public static readonly BaseNomenclature HumanFactor = new BaseNomenclature("09", "������� ������", "");
        public static readonly BaseNomenclature AviationLegislation = new BaseNomenclature("10", "���������� ���������� ������", "");
        public static readonly BaseNomenclature TurbineAeroplanesAerodynamicsStructuresSystems = new BaseNomenclature("11A", "������������, ����������� � ������� �� ���������� � �������� ���������", "");
        public static readonly BaseNomenclature PistonAeroplanesAerodynamicsStructuresSystems = new BaseNomenclature("11B", "������������, ����������� � ������� �� ���������� � ������� ���������", "");
        public static readonly BaseNomenclature HelicoptersAerodynamicsStructuresSystems = new BaseNomenclature("12", "������������, ����������� � ������� �� �����������", "");
        public static readonly BaseNomenclature AircraftAerodynamicsStructuresSystems = new BaseNomenclature("13", "������������, ����������� � ������� �� ������������������� ��������", "");
        public static readonly BaseNomenclature Propulsion = new BaseNomenclature("14", "������ ������", "");
        public static readonly BaseNomenclature TurbineEngines = new BaseNomenclature("15", "������������ ���������", "");
        public static readonly BaseNomenclature PistonEngines = new BaseNomenclature("16", "������� ���������", "");
        public static readonly BaseNomenclature Propellers = new BaseNomenclature("17", "�����", "");

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
