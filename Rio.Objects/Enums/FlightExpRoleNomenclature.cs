using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class FlightExpRoleNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.FlightExpRoleNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly FlightExpRoleNomenclature Commander = new FlightExpRoleNomenclature { ResourceKey = "Commander", Code = "01" };
        public static readonly FlightExpRoleNomenclature TraineeCommander = new FlightExpRoleNomenclature { ResourceKey = "TraineeCommander", Code = "02" };
        public static readonly FlightExpRoleNomenclature CommanderUnderObservation = new FlightExpRoleNomenclature { ResourceKey = "CommanderUnderObservation", Code = "03" };
        public static readonly FlightExpRoleNomenclature TraineeCommanderWithInstructor = new FlightExpRoleNomenclature { ResourceKey = "TraineeCommanderWithInstructor", Code = "04" };
        public static readonly FlightExpRoleNomenclature SecondPilot = new FlightExpRoleNomenclature { ResourceKey = "SecondPilot", Code = "05" };
        public static readonly FlightExpRoleNomenclature OtherCreditedHours = new FlightExpRoleNomenclature { ResourceKey = "OtherCreditedHours", Code = "06" };

        public static readonly FlightExpRoleNomenclature CrossCountryPilotCommand = new FlightExpRoleNomenclature { ResourceKey = "CrossCountryPilotCommand", Code = "07" };
        public static readonly FlightExpRoleNomenclature CrossCountryStudentPilotCommand = new FlightExpRoleNomenclature { ResourceKey = "CrossCountryStudentPilotCommand", Code = "08" };
        public static readonly FlightExpRoleNomenclature CrossCountryPilotCommandUnderSupervision = new FlightExpRoleNomenclature { ResourceKey = "CrossCountryPilotCommandUnderSupervision", Code = "09" };
        public static readonly FlightExpRoleNomenclature CrossCountryDualInstruction = new FlightExpRoleNomenclature { ResourceKey = "CrossCountryDualInstruction", Code = "10" };
        public static readonly FlightExpRoleNomenclature CrossCountryCoPilot = new FlightExpRoleNomenclature { ResourceKey = "CrossCountryCoPilot", Code = "11" };

        public static readonly FlightExpRoleNomenclature NightFlyingPilotCommand = new FlightExpRoleNomenclature { ResourceKey = "NightFlyingPilotCommand", Code = "12" };
        public static readonly FlightExpRoleNomenclature NightFlyingPilotCommandUnderSupervision = new FlightExpRoleNomenclature { ResourceKey = "NightFlyingPilotCommandUnderSupervision", Code = "13" };
        public static readonly FlightExpRoleNomenclature NightFlyingDualInstruction = new FlightExpRoleNomenclature { ResourceKey = "NightFlyingDualInstruction", Code = "14" };
        public static readonly FlightExpRoleNomenclature NightFlyingCoPilot = new FlightExpRoleNomenclature { ResourceKey = "NightFlyingCoPilot", Code = "15" };

        public static readonly FlightExpRoleNomenclature DualInstruction = new FlightExpRoleNomenclature { ResourceKey = "DualInstruction", Code = "16" };
        public static readonly FlightExpRoleNomenclature SpicIntegrated = new FlightExpRoleNomenclature { ResourceKey = "SpicIntegrated", Code = "17" };
        public static readonly FlightExpRoleNomenclature FTD = new FlightExpRoleNomenclature { ResourceKey = "FTD", Code = "18" };
        public static readonly FlightExpRoleNomenclature FNPT = new FlightExpRoleNomenclature { ResourceKey = "FNPT", Code = "19" };
        public static readonly FlightExpRoleNomenclature FSTD = new FlightExpRoleNomenclature { ResourceKey = "FSTD", Code = "20" };
        public static readonly FlightExpRoleNomenclature FlyingTime = new FlightExpRoleNomenclature { ResourceKey = "FlyingTime", Code = "21" };
        public static readonly FlightExpRoleNomenclature MCC = new FlightExpRoleNomenclature { ResourceKey = "MCC", Code = "22" };

        public static readonly FlightExpRoleNomenclature MultiPilotCommand = new FlightExpRoleNomenclature { ResourceKey = "MultiPilotCommand", Code = "23" };
        public static readonly FlightExpRoleNomenclature MultiPilotCommandUnderSupervision = new FlightExpRoleNomenclature { ResourceKey = "MultiPilotCommandUnderSupervision", Code = "24" };
        public static readonly FlightExpRoleNomenclature MultiDualInstruction = new FlightExpRoleNomenclature { ResourceKey = "MultiDualInstruction", Code = "25" };
        public static readonly FlightExpRoleNomenclature MultiCoPilot = new FlightExpRoleNomenclature { ResourceKey = "MultiCoPilot", Code = "26" };

        public static readonly FlightExpRoleNomenclature BordEngineer = new FlightExpRoleNomenclature { ResourceKey = "BordEngineer", Code = "27" };
        public static readonly FlightExpRoleNomenclature StateCommander = new FlightExpRoleNomenclature { ResourceKey = "StateCommander", Code = "28" };
        public static readonly FlightExpRoleNomenclature StateSecondPilot = new FlightExpRoleNomenclature { ResourceKey = "StateSecondPilot", Code = "29" };
        public static readonly FlightExpRoleNomenclature StateTraineeCommanderWithInstructor = new FlightExpRoleNomenclature { ResourceKey = "StateTraineeCommanderWithInstructor", Code = "30" };
        public static readonly FlightExpRoleNomenclature InstrumentTime = new FlightExpRoleNomenclature { ResourceKey = "InstrumentTime", Code = "31" };
        public static readonly FlightExpRoleNomenclature NightTime = new FlightExpRoleNomenclature { ResourceKey = "NightTime", Code = "32" };

        public static readonly IEnumerable<FlightExpRoleNomenclature> Values =
            new List<FlightExpRoleNomenclature>
            {
                Commander,
                TraineeCommander,
                CommanderUnderObservation,
                TraineeCommanderWithInstructor,
                SecondPilot,
                OtherCreditedHours,

                CrossCountryPilotCommand,
                CrossCountryStudentPilotCommand,
                CrossCountryPilotCommandUnderSupervision,
                CrossCountryDualInstruction,
                CrossCountryCoPilot,
                
                NightFlyingPilotCommand,
                NightFlyingPilotCommandUnderSupervision,
                NightFlyingDualInstruction,
                NightFlyingCoPilot,

                DualInstruction,
                SpicIntegrated, 
                FTD,
                FNPT,
                FSTD,
                FlyingTime,
                MCC,
                
                MultiPilotCommand,
                MultiPilotCommandUnderSupervision,
                MultiDualInstruction,
                MultiCoPilot,
                
                BordEngineer,
                StateCommander,
                StateSecondPilot,
                StateTraineeCommanderWithInstructor,
                InstrumentTime,
                NightTime
            };
    }
}
