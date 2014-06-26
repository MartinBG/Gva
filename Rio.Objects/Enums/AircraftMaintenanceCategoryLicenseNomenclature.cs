using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class AircraftMaintenanceCategoryLicenseNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature AircraftGasTurbineEngineA = new BaseNomenclature { Text = "Самолети с газотурбинни двигатели", Value = "A", Description = "" };
        public static readonly BaseNomenclature AircraftGasTurbineEngineBOne = new BaseNomenclature { Text = "Самолети с газотурбинни двигатели", Value = "B1", Description = "" };
        public static readonly BaseNomenclature PistonEngineAirplanesA = new BaseNomenclature { Text = "Самолети с бутални двигатели", Value = "A", Description = "" };
        public static readonly BaseNomenclature PistonEngineAirplanesBOne = new BaseNomenclature { Text = "Самолети с бутални двигатели", Value = "B1", Description = "" };
        public static readonly BaseNomenclature HelicopterTurbineEngineA = new BaseNomenclature { Text = "Вертолети с газотурбинни двигатели", Value = "A", Description = "" };
        public static readonly BaseNomenclature HelicopterTurbineEngineBOne = new BaseNomenclature { Text = "Вертолети с газотурбинни двигатели", Value = "B1", Description = "" };
        public static readonly BaseNomenclature HelicoptersPistonA = new BaseNomenclature { Text = "Вертолети с бутални двигатели", Value = "A", Description = "" };
        public static readonly BaseNomenclature HelicoptersPistonBOne = new BaseNomenclature { Text = "Вертолети с бутални двигатели", Value = "B1", Description = "" };
        public static readonly BaseNomenclature AvionicsBTwo = new BaseNomenclature { Text = "Авионикс", Value = "B2", Description = "" };
        public static readonly BaseNomenclature NonPressurisedAeroplanesMTOMBThree = new BaseNomenclature { Text = "Нехерметизирани самолети с бутални двигатели и МТОМ под 2т", Value = "B3", Description = "" };
        public static readonly BaseNomenclature LargeAircraftsC = new BaseNomenclature { Text = "Големи ВС", Value = "C", Description = "" };
        public static readonly BaseNomenclature NonLargeAircraftsC = new BaseNomenclature { Text = "ВС различни от големи", Value = "C", Description = "" };

        public AircraftMaintenanceCategoryLicenseNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                AircraftGasTurbineEngineA,
                AircraftGasTurbineEngineBOne,
                PistonEngineAirplanesA,
                PistonEngineAirplanesBOne,
                HelicopterTurbineEngineA,
                HelicopterTurbineEngineBOne,
                HelicoptersPistonA,
                HelicoptersPistonBOne,
                AvionicsBTwo,
                NonPressurisedAeroplanesMTOMBThree,
                LargeAircraftsC,
                NonLargeAircraftsC,
            };
        }
    }
}
