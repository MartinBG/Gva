using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class PermissibleActivityNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature AW1 = new BaseNomenclature("01", "������ �� ������ �� ������ �������� (AW-1)");
        public static readonly BaseNomenclature AW2 = new BaseNomenclature("02", "����������-�������� ������ (AW-2)");
        public static readonly BaseNomenclature AW3 = new BaseNomenclature("03", "����������� � ���������� (AW-3)");
        public static readonly BaseNomenclature AW4 = new BaseNomenclature("04", "������������� (AW-4)");
        public static readonly BaseNomenclature AW5 = new BaseNomenclature("05", "���������� ����������� � ����� ����(�\\�-5)");
        public static readonly BaseNomenclature AW6 = new BaseNomenclature("06", "����� � ������, ���.������ (AW-6)");
        public static readonly BaseNomenclature AW7 = new BaseNomenclature("07", "������������� ������ (AW-7)");
        public static readonly BaseNomenclature AW8 = new BaseNomenclature("08", "���������� �/��� ����������� ����� ������� (AW-8)");
        public static readonly BaseNomenclature AW9 = new BaseNomenclature("09", "��������-���������� ������ (AW-9)");
        public static readonly BaseNomenclature AW10 = new BaseNomenclature("10", "������ �� ������� ������ (AW-10)");
        public static readonly BaseNomenclature AW11 = new BaseNomenclature("11", "������� (AW-11)");
        public static readonly BaseNomenclature AW12 = new BaseNomenclature("12", "������� ����� ���� ������� (AW-12)");
        public static readonly BaseNomenclature AW13 = new BaseNomenclature("13", "�����, ���� �� ������� ���� �� �������� (AW-13)");
        public static readonly BaseNomenclature AW14 = new BaseNomenclature("14", "������ ������ (AW-14)");
        public static readonly BaseNomenclature AW15 = new BaseNomenclature("15", "������� ������ (AW-15)");
        public static readonly BaseNomenclature AW16 = new BaseNomenclature("16", "����������� �� ���������������� �� ������� (AW-I6)");
        public static readonly BaseNomenclature AW17 = new BaseNomenclature("17", "������� �� ���������� �� (AW-17)");
        public static readonly BaseNomenclature AW18 = new BaseNomenclature("18", "������ �� ������� � ������� (AW-18)");

        public PermissibleActivityNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                AW1,
                AW2,
                AW3,
                AW4,
                AW5,
                AW6,
                AW7,
                AW8,
                AW9,
                AW10,
                AW11,
                AW12,
                AW13,
                AW14,
                AW15,
                AW16,
                AW17,
                AW18
            };
        }
    }
}
