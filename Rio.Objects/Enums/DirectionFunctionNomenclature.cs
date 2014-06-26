using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class DirectionFunctionNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature CEO = new BaseNomenclature("01", "������������ ��������");
        public static readonly BaseNomenclature Code = new BaseNomenclature("02", "����������� �������� ��������� �������");
        public static readonly BaseNomenclature Quality = new BaseNomenclature("03", "����������� ��������  ������� ��������");
        public static readonly BaseNomenclature Security = new BaseNomenclature("04", "����������� ���������");
        public static readonly BaseNomenclature Chief = new BaseNomenclature("05", "����������� ����������� ������");
        public static readonly BaseNomenclature Operations = new BaseNomenclature("06", "�������� ��������� ������������");
        public static readonly BaseNomenclature OUPLG = new BaseNomenclature("07", "����������� �����");
        public static readonly BaseNomenclature CFO = new BaseNomenclature("08", "�������� ��������");
        public static readonly BaseNomenclature Sales = new BaseNomenclature("09", "��������� ��������");
        public static readonly BaseNomenclature Director = new BaseNomenclature("10", "����������� ������� ����������");
        public static readonly BaseNomenclature Administrative = new BaseNomenclature("11", "��������������� ��������");
        public static readonly BaseNomenclature Training = new BaseNomenclature("12", "����������� ����������");
        public static readonly BaseNomenclature Pilot = new BaseNomenclature("13", "������ �����");

        public DirectionFunctionNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                CEO ,
                Code,
                Quality,
                Security ,
                Chief,
                Operations ,
                OUPLG ,
                CFO ,
                Sales,
                Director,
                Administrative,
                Training ,
                Pilot
            }.OrderBy(e => e.Text).ToList();
        }
    }
}
