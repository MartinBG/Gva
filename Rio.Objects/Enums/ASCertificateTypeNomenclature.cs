using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ASCertificateTypeNomenclature : BaseNomenclature
    {
        public List<BaseNomenclature> PilotValues;
        public List<BaseNomenclature> FlightDispatchersValues;
        public List<BaseNomenclature> NotPilotsValues;
        public List<BaseNomenclature> TrafficControllersValues;
        public List<BaseNomenclature> ForeignersValues;
        public List<BaseNomenclature> CabinCrewValues;
        public List<BaseNomenclature> ChangeCompetentValues;
        public List<BaseNomenclature> InstructorValues;

        public static readonly BaseNomenclature AmateurAirlinePilotPPL = new BaseNomenclature("PPL(A)", "������� ����� �� ������� PPL(A)");
        public static readonly BaseNomenclature ProfessionalAirlinePilotCPL = new BaseNomenclature("CPL(A)", "������������� ����� �� ������� CPL(A)");
        public static readonly BaseNomenclature ProfessionalHelicopterPilotCPL = new BaseNomenclature("CPL(H)", "������������� ����� �� ��������  CPL(H)");
        public static readonly BaseNomenclature ProfessionalAirshipPilotCPL = new BaseNomenclature("CPL(As)", "������������� ����� �� ��������� CPL(As)");
        public static readonly BaseNomenclature QualificationFlightInstructionAirplaneIR = new BaseNomenclature("IR(A)", "��������������� �� ����� �� ������� �� ������� IR(A)");
        public static readonly BaseNomenclature QualificationFlightInstructionHelicopterIR = new BaseNomenclature("IR(H)", "��������������� �� ����� �� ������� �� �������� IR(H)");
        public static readonly BaseNomenclature QualificationFlightInstructionAirshipIR = new BaseNomenclature("IR(As)", "��������������� �� ����� �� ������� �� ��������� IR(As)");

        public static readonly BaseNomenclature FDL = new BaseNomenclature("FDL", "FDL");

        public static readonly BaseNomenclature FEL = new BaseNomenclature("FNL", "������ ������� F/EL");
        public static readonly BaseNomenclature FROL = new BaseNomenclature("FROL", "������ ������ F/ROL");
        public static readonly BaseNomenclature FOL = new BaseNomenclature("FOL", "������ �������� F/OL");

        public static readonly BaseNomenclature ATCL = new BaseNomenclature("ATCL", "����������� �� ������ ATCL");
        public static readonly BaseNomenclature SATCL = new BaseNomenclature("SATCL", "������-����������� �� ������ SATCL ");
        public static readonly BaseNomenclature FDA = new BaseNomenclature("FDA", "��������-����������� �� ������ FDA  ");
        public static readonly BaseNomenclature CATM = new BaseNomenclature("CATM", "����������� �� ��� CATM");

        public static readonly BaseNomenclature CAL = new BaseNomenclature("CAL", "��������� C/AL ");
        public static readonly BaseNomenclature FCL = new BaseNomenclature("FCL", "������ ������������ F/CL");

        public static readonly BaseNomenclature Initial = new BaseNomenclature("initial", "������������ ��������");
        public static readonly BaseNomenclature Confirm = new BaseNomenclature("confirm", "�������������");
        public static readonly BaseNomenclature New = new BaseNomenclature("new", "�����������");
        public static readonly BaseNomenclature Expand = new BaseNomenclature("expand", "����������� �� �������");

        public ASCertificateTypeNomenclature()
        {
            this.PilotValues = new List<BaseNomenclature>()
            {
                AmateurAirlinePilotPPL,
                ProfessionalAirlinePilotCPL,
                ProfessionalHelicopterPilotCPL,
                ProfessionalAirshipPilotCPL,
                QualificationFlightInstructionAirplaneIR,
                QualificationFlightInstructionHelicopterIR,
                QualificationFlightInstructionAirshipIR 
            };

            this.FlightDispatchersValues = new List<BaseNomenclature>()
            {
                FDL
            };

            this.NotPilotsValues = new List<BaseNomenclature>()
            {
                FEL,
                FROL,
                FOL
            };

            this.TrafficControllersValues = new List<BaseNomenclature>()
            {
                ATCL, 
                SATCL,
                FDA,
                CATM 
            };

            this.ForeignersValues = new List<BaseNomenclature>()
            {
                AmateurAirlinePilotPPL,
                ProfessionalAirlinePilotCPL,
                ProfessionalHelicopterPilotCPL,
                ProfessionalAirshipPilotCPL,
                QualificationFlightInstructionAirplaneIR,
                QualificationFlightInstructionHelicopterIR,
                QualificationFlightInstructionAirshipIR,
                FDL,
                FEL,
                FROL,
                FOL,
                ATCL, 
                SATCL,
                FDA,
                CATM, 
                CAL,
                FCL
            };

            this.CabinCrewValues = new List<BaseNomenclature>()
            {
                CAL,
                FCL
            };

            this.ChangeCompetentValues = new List<BaseNomenclature>()
            {
                CAL,
                FCL
            };

            this.InstructorValues = new List<BaseNomenclature>()
            {
                Initial,
                Confirm,
                New,
                Expand 
            };
        }
    }
}
