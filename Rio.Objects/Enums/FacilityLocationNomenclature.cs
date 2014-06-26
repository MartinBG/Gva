using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class FacilityLocationNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Sofia = new BaseNomenclature("01", "�� �� ��� - �����");
        public static readonly BaseNomenclature Varna = new BaseNomenclature("02", "�� �� ��� - �����");
        public static readonly BaseNomenclature Burgas = new BaseNomenclature("03", "�� �� ��� - ������");
        public static readonly BaseNomenclature Plovdiv = new BaseNomenclature("04", "�� �� ��� - �������");
        public static readonly BaseNomenclature GornaOriahovica = new BaseNomenclature("05", "�� �� ��� - ����� ���������");


        public FacilityLocationNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Sofia,
                Varna,
                Burgas,
                Plovdiv,
                GornaOriahovica
            };
        }
    }
}
