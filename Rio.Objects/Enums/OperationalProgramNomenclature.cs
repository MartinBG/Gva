using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class OperationalProgramNomenclature : BaseNomenclature
    {
        public OperationalProgramNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                new BaseNomenclature("01","���������"),
                new BaseNomenclature("02","������ �����"),
                new BaseNomenclature("03","���������� ��������"),
                new BaseNomenclature("04","��������������������"),
                new BaseNomenclature("05","���������� �����"),
                new BaseNomenclature("06","�������� �� ��������� �������"),
                new BaseNomenclature("07","��������������� ���������")
            }.OrderBy(e=>e.Text).ToList();
        }
    }
}
