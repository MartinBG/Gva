using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class TypeOfObjectNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature StationaryLuggageBelts = new BaseNomenclature("01", "стационарни багажни ленти в пътническите терминали", "");
        public static readonly BaseNomenclature TransportBelts = new BaseNomenclature("02", "транспортни ленти за товарене или разтоварване на багажи и товари", "");
        public static readonly BaseNomenclature CheckInCounters = new BaseNomenclature("03", "гишета за регистрация на пътниците (транспортна лента и теглилка)", "");
        public static readonly BaseNomenclature LuggageCargoWeights = new BaseNomenclature("04", "теглилки за багажи и товари", "");
        public static readonly BaseNomenclature InformationSystems = new BaseNomenclature("05", "информационни системи за пътници", "");
        public static readonly BaseNomenclature PassengersRegistrationSystems = new BaseNomenclature("06", "системи за регистрация на пътници и обработка на багажите им", "");
        public static readonly BaseNomenclature LuggageTransportTechnics = new BaseNomenclature("07", "техника за транспортиране на багажи, товари и поща", "");
        public static readonly BaseNomenclature LoadingUnloadingLuggageTechnics = new BaseNomenclature("08", "техника за натоварване и разтоварване на багажи, товари и поща", "");
        public static readonly BaseNomenclature ApronBuses = new BaseNomenclature("09", "перонни автобуси", "");
        public static readonly BaseNomenclature GroundPowerMeans = new BaseNomenclature("10", "средства за наземно електрозахранване", "");
        public static readonly BaseNomenclature FuelingMachines = new BaseNomenclature("11", "горивозареждащи машини и транспортни автоцистерни", "");
        public static readonly BaseNomenclature SanitaryUnitsServiceMeans = new BaseNomenclature("12", "средства за обслужване на санитарните възли на самолетите", "");
        public static readonly BaseNomenclature Ladders = new BaseNomenclature("13", "подвижни стълби за качване и слизане на пътниците", "");
        public static readonly BaseNomenclature WaterLoadingAircraftMeans = new BaseNomenclature("14", "средства за зареждане на самолетите с вода за пиене", "");
        public static readonly BaseNomenclature AntiIcingMeans = new BaseNomenclature("15", "средства за противообледенителна обработка на въздухоплавателни средства", "");
        public static readonly BaseNomenclature Tractors = new BaseNomenclature("16", "влекачи, включително влекачи на въздухоплавателни средства", "");
        public static readonly BaseNomenclature BuffetLoadingMachines = new BaseNomenclature("17", "машини за зареждане на бордния бюфет", "");
        public static readonly BaseNomenclature AirBridges = new BaseNomenclature("18", "пътнически ръкави (air bridges)", "");
        public static readonly BaseNomenclature StandGuideAircraft = new BaseNomenclature("19", "системи за насочване на самолета към стоянка", "");

        public TypeOfObjectNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                StationaryLuggageBelts,
                TransportBelts,
                CheckInCounters,
                LuggageCargoWeights,
                InformationSystems,
                PassengersRegistrationSystems,
                LuggageTransportTechnics,
                LoadingUnloadingLuggageTechnics,
                ApronBuses,
                GroundPowerMeans,
                FuelingMachines,
                SanitaryUnitsServiceMeans,
                Ladders,
                WaterLoadingAircraftMeans,
                AntiIcingMeans,
                Tractors,
                BuffetLoadingMachines,
                AirBridges,
                StandGuideAircraft
            }.OrderBy(e => e.Text).ToList();
        }
    }
}
