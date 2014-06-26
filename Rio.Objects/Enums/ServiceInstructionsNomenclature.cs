using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ServiceInstructionsNomenclature : BaseNomenclature
    {
        public ServiceInstructionsNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                new BaseNomenclature("01", "����"),
                new BaseNomenclature("02", "����"),
                new BaseNomenclature("03", "�� �����������"),
                new BaseNomenclature("04", "�� �����"),
                new BaseNomenclature("05", "�� �������"),
                new BaseNomenclature("06", "�� ������"),
                new BaseNomenclature("07", "��� �����"),
                new BaseNomenclature("08", "��� ����"),
                new BaseNomenclature("09", "��� ��������� ������"),
                new BaseNomenclature("10", "����� �����������"),
                new BaseNomenclature("11", "����� ������"),
                new BaseNomenclature("12", "����� �����"),
                new BaseNomenclature("13", "����� �������������"),
                new BaseNomenclature("14", "����� �����"),
                new BaseNomenclature("15", "����� �������"),
                new BaseNomenclature("16", "����� ���������"),
                new BaseNomenclature("17", "����� ������"),
                new BaseNomenclature("18", "����� ������"),
                new BaseNomenclature("19", "����� �������"),
                new BaseNomenclature("20", "����� ����"),
                new BaseNomenclature("21", "����� ������"),
                new BaseNomenclature("22", "����� �����"),
                new BaseNomenclature("23", "����� ����� ������"),
                new BaseNomenclature("24", "����� �������"),
                new BaseNomenclature("25", "����� �����")
            };
        }
    }
}
