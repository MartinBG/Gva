using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ValidationRecoveryNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Validation = new BaseNomenclature("validation", "потвърждаване");
        public static readonly BaseNomenclature Recovery = new BaseNomenclature("recovery", "възстановяване");


        public ValidationRecoveryNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Validation,
                Recovery
            };
        }
    }
}
