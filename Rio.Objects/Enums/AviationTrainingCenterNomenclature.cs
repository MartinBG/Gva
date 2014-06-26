using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class AviationTrainingCenterNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Institute = new BaseNomenclature("01", "�������� �� �������� ��������� ����");
        public static readonly BaseNomenclature Air = new BaseNomenclature("02", "��� ������� ����");
        public static readonly BaseNomenclature Private = new BaseNomenclature("03", "������ ����������� ����� ���-�����");
        public static readonly BaseNomenclature Aviational = new BaseNomenclature("04", "���������� ������ ������ - ������ �����");
        public static readonly BaseNomenclature Airport = new BaseNomenclature("05", "������� �������� ��������");

        public AviationTrainingCenterNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Institute,
                Air,
                Private,
                Aviational,
                Airport
            };
        }
    }
}
