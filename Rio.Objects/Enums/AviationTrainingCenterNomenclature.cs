using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class AviationTrainingCenterNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Institute = new BaseNomenclature("01", "Институт по въздушен транспорт ЕООД");
        public static readonly BaseNomenclature Air = new BaseNomenclature("02", "Еър скорпио ЕООД");
        public static readonly BaseNomenclature Private = new BaseNomenclature("03", "Частен транспортен колеж ООД-СОФИЯ");
        public static readonly BaseNomenclature Aviational = new BaseNomenclature("04", "Авиационен учебен център - летище София");
        public static readonly BaseNomenclature Airport = new BaseNomenclature("05", "Еърпорт секюрити клиаранс");

        public AviationTrainingCenterNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Institute,
                Air,
                Private,
                Aviational,
                Airport
            };
        }
    }
}
