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

        public static readonly BaseNomenclature AmateurAirlinePilotPPL = new BaseNomenclature("PPL(A)", "Любител пилот на самолет PPL(A)");
        public static readonly BaseNomenclature ProfessionalAirlinePilotCPL = new BaseNomenclature("CPL(A)", "Професионален пилот на самолет CPL(A)");
        public static readonly BaseNomenclature ProfessionalHelicopterPilotCPL = new BaseNomenclature("CPL(H)", "Професионален пилот на вертолет  CPL(H)");
        public static readonly BaseNomenclature ProfessionalAirshipPilotCPL = new BaseNomenclature("CPL(As)", "Професионален пилот на дирижабъл CPL(As)");
        public static readonly BaseNomenclature QualificationFlightInstructionAirplaneIR = new BaseNomenclature("IR(A)", "Правоспособност за полет по прибори за самолет IR(A)");
        public static readonly BaseNomenclature QualificationFlightInstructionHelicopterIR = new BaseNomenclature("IR(H)", "Правоспособност за полет по прибори за вертолет IR(H)");
        public static readonly BaseNomenclature QualificationFlightInstructionAirshipIR = new BaseNomenclature("IR(As)", "Правоспособност за полет по прибори за дирижабъл IR(As)");

        public static readonly BaseNomenclature FDL = new BaseNomenclature("FDL", "FDL");

        public static readonly BaseNomenclature FEL = new BaseNomenclature("FNL", "Борден инженер F/EL");
        public static readonly BaseNomenclature FROL = new BaseNomenclature("FROL", "Борден радист F/ROL");
        public static readonly BaseNomenclature FOL = new BaseNomenclature("FOL", "Борден оператор F/OL");

        public static readonly BaseNomenclature ATCL = new BaseNomenclature("ATCL", "Ръководител на полети ATCL");
        public static readonly BaseNomenclature SATCL = new BaseNomenclature("SATCL", "Ученик-ръководител на полети SATCL ");
        public static readonly BaseNomenclature FDA = new BaseNomenclature("FDA", "Асистент-координатор на полети FDA  ");
        public static readonly BaseNomenclature CATM = new BaseNomenclature("CATM", "Координатор по УВД CATM");

        public static readonly BaseNomenclature CAL = new BaseNomenclature("CAL", "Стюардеса C/AL ");
        public static readonly BaseNomenclature FCL = new BaseNomenclature("FCL", "Борден съпроводител F/CL");

        public static readonly BaseNomenclature Initial = new BaseNomenclature("initial", "Първоначално издаване");
        public static readonly BaseNomenclature Confirm = new BaseNomenclature("confirm", "Потвърждаване");
        public static readonly BaseNomenclature New = new BaseNomenclature("new", "Подновяване");
        public static readonly BaseNomenclature Expand = new BaseNomenclature("expand", "Разширяване на правата");

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
