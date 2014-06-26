using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class AircraftOperationTypeNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature NoInput = new BaseNomenclature("0", "���� �������� �����", "");
        public static readonly BaseNomenclature Passengers = new BaseNomenclature("A1", "����������� ��������� - �������", "");
        public static readonly BaseNomenclature Cargo = new BaseNomenclature("A2", "����������� ��������� - �����", "");
        public static readonly BaseNomenclature Emergency = new BaseNomenclature("A3", "������ �� ������ ���������� �����", "");
        public static readonly BaseNomenclature TransportMail = new BaseNomenclature("A4", "������ �� ����", "");
        public static readonly BaseNomenclature ExternalLoad = new BaseNomenclature("AW001", "������ �� ������ �� ������ ��������", "");
        public static readonly BaseNomenclature Construction = new BaseNomenclature("AW002", "����������-�������� ������", "");
        public static readonly BaseNomenclature Inspection = new BaseNomenclature("AW003", "����������� � ����������", "");
        public static readonly BaseNomenclature Photography = new BaseNomenclature("AW004", "�������������", "");
        public static readonly BaseNomenclature Surveying = new BaseNomenclature("AW005", "���������� ����������� � ���������", "");
        public static readonly BaseNomenclature Fire = new BaseNomenclature("AW006", "����� � ������, ���. ������", "");
        public static readonly BaseNomenclature Spraying = new BaseNomenclature("AW007", "������������� ������", "");
        public static readonly BaseNomenclature Weather = new BaseNomenclature("AW008", "���������� �/��� ����������� �� �������", "");
        public static readonly BaseNomenclature Search = new BaseNomenclature("AW009", "��������-���������� ������", "");
        public static readonly BaseNomenclature HumanOrgans = new BaseNomenclature("AW010", "������ �� ������� ������", "");


        public AircraftOperationTypeNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                NoInput,
                Passengers,
                Cargo,
                Emergency,
                TransportMail,
                ExternalLoad,
                Construction,
                Inspection,
                Photography,
                Surveying,
                Fire,
                Spraying,
                Weather,
                Search,
                HumanOrgans 
            };
        }
    }
}
