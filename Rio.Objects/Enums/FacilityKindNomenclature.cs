using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class FacilityKindNomenclature : BaseNomenclature
    {
        public List<BaseNomenclature> NavigationalAidsValues { get; set; }
        public List<BaseNomenclature> FitnessAutomatedATMValues { get; set; }

        public static readonly BaseNomenclature Automated = new BaseNomenclature("01", "автоматизирани системи за комуникации \"земя-земя\" и \"въздух-земя\"");
        public static readonly BaseNomenclature AirSpace = new BaseNomenclature("02", "системи за управление на въздушното пространство");
        public static readonly BaseNomenclature Flow = new BaseNomenclature("03", "системи за управление на потока от въздушно движение");

        public static readonly BaseNomenclature Laboratory = new BaseNomenclature("04", "Лаборатория за тест на навигационните съоръжения за въздушна навигация и кацане");
        public static readonly BaseNomenclature ExactLanding = new BaseNomenclature("05", "Курсо-глисадна система за точен подход за кацане");
        public static readonly BaseNomenclature FirstRadiolocator = new BaseNomenclature("06", "Първичен радиолокатор");
        public static readonly BaseNomenclature SecondRadiolocator = new BaseNomenclature("07", "Вторичен радиолокатор");
        public static readonly BaseNomenclature AutomatedSystem = new BaseNomenclature("08", "Автоматизирана система за изобразяване на многорадарна и планова информация");
        public static readonly BaseNomenclature RadiolocatorFlyingField = new BaseNomenclature("09", "Радиолокатор за обзор на летателното поле");
        public static readonly BaseNomenclature MeteorologicalRadiolocator = new BaseNomenclature("10", "Метеорологичен радиолокатор");
        public static readonly BaseNomenclature AutomatedMonitoring = new BaseNomenclature("11", "Автоматизирана метеорологична наблюдателна система");
        public static readonly BaseNomenclature OmnidirectionalBeacon = new BaseNomenclature("12", "Всенасочен радиофар");
        public static readonly BaseNomenclature DriveStation = new BaseNomenclature("13", "Приводна радиостанция");
        public static readonly BaseNomenclature DalnomericSystem = new BaseNomenclature("14", "Далномерна система");
        public static readonly BaseNomenclature AutoFinder = new BaseNomenclature("15", "Автоматичен радиопеленгатор");
        public static readonly BaseNomenclature LightingLandingSystem = new BaseNomenclature("16", "Светотехническа система за кацане");
        public static readonly BaseNomenclature AeronavigationalSystem = new BaseNomenclature("17", "Система за аеронавигационно информационно обслужване");

        public FacilityKindNomenclature()
        {
            this.FitnessAutomatedATMValues = new List<BaseNomenclature>()
            {
                Automated,
                AirSpace,
                Flow
            };

            this.NavigationalAidsValues = new List<BaseNomenclature>()
            {
                Laboratory,
                ExactLanding,
                FirstRadiolocator,
                SecondRadiolocator,
                AutomatedSystem,
                RadiolocatorFlyingField,
                MeteorologicalRadiolocator,
                AutomatedMonitoring,
                OmnidirectionalBeacon,
                DriveStation,
                DalnomericSystem,
                AutoFinder,
                LightingLandingSystem,
                AeronavigationalSystem
            }.OrderBy(e => e.Text).ToList();
        }
    }
}
