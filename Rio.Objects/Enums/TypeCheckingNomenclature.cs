using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class TypeCheckingNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Conv1 = new BaseNomenclature("01", "CONV1 ����� ����� � ������ ���� �� ���������", "", GroupTypeCheckingNomenclature.Conventional.Value);
        public static readonly BaseNomenclature Conv2 = new BaseNomenclature("02", "CONV2 ����������� �����", "", GroupTypeCheckingNomenclature.Conventional.Value);
        public static readonly BaseNomenclature Conv3 = new BaseNomenclature("03", "CONV3 ������, ���� � ����� ����� � ���������", "", GroupTypeCheckingNomenclature.Conventional.Value);
        public static readonly BaseNomenclature EDS = new BaseNomenclature("04", "EDS �������� �� ��������� �� ����������� ����� ���� EDS", "", GroupTypeCheckingNomenclature.Conventional.Value);

        public static readonly BaseNomenclature HS1 = new BaseNomenclature("05", "HS1 ����", "", GroupTypeCheckingNomenclature.Manual.Value);
        public static readonly BaseNomenclature HS2 = new BaseNomenclature("06", "HS2 ����� ����� � ������ ����", "", GroupTypeCheckingNomenclature.Manual.Value);
        public static readonly BaseNomenclature HS3 = new BaseNomenclature("07", "HS3 ����������� �����", "", GroupTypeCheckingNomenclature.Manual.Value);

        public static readonly BaseNomenclature ETD1 = new BaseNomenclature("08", "ETD1 ����� � ����������� �����", "", GroupTypeCheckingNomenclature.ETD.Value);
        public static readonly BaseNomenclature ETD2 = new BaseNomenclature("09", "ETD2 ������ � ����", "", GroupTypeCheckingNomenclature.ETD.Value);
        public static readonly BaseNomenclature VEH = new BaseNomenclature("10", "VEH �������� �� ���", "", GroupTypeCheckingNomenclature.ETD.Value);
        public static readonly BaseNomenclature SUB = new BaseNomenclature("11", "SUB ���������� � ������� �� ����������� �� ���������", "", GroupTypeCheckingNomenclature.ETD.Value);
        public static readonly BaseNomenclature AC = new BaseNomenclature("12", "AC ������� �� �������, ���������� � �����������", "", GroupTypeCheckingNomenclature.ETD.Value);

        public static readonly BaseNomenclature INS = new BaseNomenclature("13", "INS ���������� �� ���������� ���������", "", GroupTypeCheckingNomenclature.Instructors.Value);

        public TypeCheckingNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Conv1,
                Conv2,
                Conv3,
                EDS,

                HS1,
                HS2,
                HS3,

                ETD1,
                ETD2,
                VEH,
                SUB, 
                AC,

                INS
            };
        }
    }
}
