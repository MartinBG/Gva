using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class AuthorityIssuedAttachedDocumentNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature TrainingFlight = new BaseNomenclature("01", "Training Flight Operations Support and Services", "");
        public static readonly BaseNomenclature Bombardier = new BaseNomenclature("02", "Bombardier Aerospace Training Center	", "");
        public static readonly BaseNomenclature Division = new BaseNomenclature("03", "Поделение", "");
        public static readonly BaseNomenclature SofiaFlight = new BaseNomenclature("04", "SOFIA FLIGHT TRAINING CENTER", "");
        public static readonly BaseNomenclature MVR = new BaseNomenclature("05", "МВР", "");

        public AuthorityIssuedAttachedDocumentNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                TrainingFlight,
                Bombardier,
                Division,
                SofiaFlight,
                MVR
                
            };
        }
    }
}
