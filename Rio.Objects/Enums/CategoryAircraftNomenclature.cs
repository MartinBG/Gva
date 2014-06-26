using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class CategoryAircraftNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature LargeAero = new BaseNomenclature("A1", "������� � ����� 5,700 �� ��� ������", "");
        public static readonly BaseNomenclature SmallAero = new BaseNomenclature("A2", "������� � ����� �� 5,700 ��", "");
        public static readonly BaseNomenclature LargeRotor = new BaseNomenclature("A3", "���������� � ����� 3,750 �� ��� ������", "");
        public static readonly BaseNomenclature SmallRotor = new BaseNomenclature("A4", "���������� � ����� �� 3,750 ��", "");
        public static readonly BaseNomenclature LightAero = new BaseNomenclature("VA", "����� ��� �������", "");
        public static readonly BaseNomenclature LightRotor = new BaseNomenclature("VH", "����� ��� ����������", "");
        public static readonly BaseNomenclature Motor = new BaseNomenclature("VM", "���������������", "");
        public static readonly BaseNomenclature Glider = new BaseNomenclature("VN", "���������� ������", "");
        public static readonly BaseNomenclature Balloon = new BaseNomenclature("FB", "�������� �����", "");
        public static readonly BaseNomenclature Experiment = new BaseNomenclature("EX", "���������������", "");

        public CategoryAircraftNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                LargeAero ,
                SmallAero ,
                LargeRotor,
                SmallRotor,
                LightAero ,
                LightRotor,
                Motor ,
                Glider ,
                Balloon  ,
                Experiment,
            };
        }
    }
}
