using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class AircraftOperationTypeNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature NoInput = new BaseNomenclature("0", "Няма въведени данни", "");
        public static readonly BaseNomenclature Passengers = new BaseNomenclature("A1", "Транспортна категория - пътници", "");
        public static readonly BaseNomenclature Cargo = new BaseNomenclature("A2", "Транспортна категория - карго", "");
        public static readonly BaseNomenclature Emergency = new BaseNomenclature("A3", "Полети за спешна медицинска помощ", "");
        public static readonly BaseNomenclature TransportMail = new BaseNomenclature("A4", "Превоз на поща", "");
        public static readonly BaseNomenclature ExternalLoad = new BaseNomenclature("AW001", "Превоз на товари на външно окачване", "");
        public static readonly BaseNomenclature Construction = new BaseNomenclature("AW002", "Строително-монтажни работи", "");
        public static readonly BaseNomenclature Inspection = new BaseNomenclature("AW003", "Патрулиране и наблюдение", "");
        public static readonly BaseNomenclature Photography = new BaseNomenclature("AW004", "Фотографиране", "");
        public static readonly BaseNomenclature Surveying = new BaseNomenclature("AW005", "Геофизични изследвания и картиране", "");
        public static readonly BaseNomenclature Fire = new BaseNomenclature("AW006", "Борба с пожари, вкл. горски", "");
        public static readonly BaseNomenclature Spraying = new BaseNomenclature("AW007", "Авиохимически работи", "");
        public static readonly BaseNomenclature Weather = new BaseNomenclature("AW008", "Наблюдение и/или въздействие на времето", "");
        public static readonly BaseNomenclature Search = new BaseNomenclature("AW009", "Аварийно-спасителни работи", "");
        public static readonly BaseNomenclature HumanOrgans = new BaseNomenclature("AW010", "Превоз на човешки органи", "");

        public static readonly BaseNomenclature Private = new BaseNomenclature("Private", "Частно", "");


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
                HumanOrgans,
                Private 
            };
        }
    }
}
