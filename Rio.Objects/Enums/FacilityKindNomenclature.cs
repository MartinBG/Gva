using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class FacilityKindNomenclature : BaseNomenclature
    {
        public List<BaseNomenclature> NavigationalAidsValues { get; set; }
        public List<BaseNomenclature> FitnessAutomatedATMValues { get; set; }

        public static readonly BaseNomenclature Automated = new BaseNomenclature("01", "�������������� ������� �� ����������� \"����-����\" � \"������-����\"");
        public static readonly BaseNomenclature AirSpace = new BaseNomenclature("02", "������� �� ���������� �� ���������� ������������");
        public static readonly BaseNomenclature Flow = new BaseNomenclature("03", "������� �� ���������� �� ������ �� �������� ��������");

        public static readonly BaseNomenclature Laboratory = new BaseNomenclature("04", "����������� �� ���� �� �������������� ���������� �� �������� ��������� � ������");
        public static readonly BaseNomenclature ExactLanding = new BaseNomenclature("05", "�����-�������� ������� �� ����� ������ �� ������");
        public static readonly BaseNomenclature FirstRadiolocator = new BaseNomenclature("06", "�������� ������������");
        public static readonly BaseNomenclature SecondRadiolocator = new BaseNomenclature("07", "�������� ������������");
        public static readonly BaseNomenclature AutomatedSystem = new BaseNomenclature("08", "�������������� ������� �� ������������ �� ������������ � ������� ����������");
        public static readonly BaseNomenclature RadiolocatorFlyingField = new BaseNomenclature("09", "������������ �� ����� �� ����������� ����");
        public static readonly BaseNomenclature MeteorologicalRadiolocator = new BaseNomenclature("10", "�������������� ������������");
        public static readonly BaseNomenclature AutomatedMonitoring = new BaseNomenclature("11", "�������������� �������������� ������������ �������");
        public static readonly BaseNomenclature OmnidirectionalBeacon = new BaseNomenclature("12", "���������� ��������");
        public static readonly BaseNomenclature DriveStation = new BaseNomenclature("13", "�������� ������������");
        public static readonly BaseNomenclature DalnomericSystem = new BaseNomenclature("14", "���������� �������");
        public static readonly BaseNomenclature AutoFinder = new BaseNomenclature("15", "����������� ���������������");
        public static readonly BaseNomenclature LightingLandingSystem = new BaseNomenclature("16", "��������������� ������� �� ������");
        public static readonly BaseNomenclature AeronavigationalSystem = new BaseNomenclature("17", "������� �� ���������������� ������������� ����������");

        public FacilityKindNomenclature()
        {
            this.FitnessAutomatedATMValues = new List<BaseNomenclature>()
            {
                Automated,
                AirSpace,
                Flow
            };

            this.NavigationalAidsValues = new List<BaseNomenclature>()
            {
                Laboratory,
                ExactLanding,
                FirstRadiolocator,
                SecondRadiolocator,
                AutomatedSystem,
                RadiolocatorFlyingField,
                MeteorologicalRadiolocator,
                AutomatedMonitoring,
                OmnidirectionalBeacon,
                DriveStation,
                DalnomericSystem,
                AutoFinder,
                LightingLandingSystem,
                AeronavigationalSystem
            }.OrderBy(e => e.Text).ToList();
        }
    }
}
