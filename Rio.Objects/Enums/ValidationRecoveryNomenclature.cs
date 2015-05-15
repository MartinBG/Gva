using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ValidationRecoveryNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Validation = new BaseNomenclature("validation", "Потвърждаване");
        public static readonly BaseNomenclature Recovery = new BaseNomenclature("recovery", "Възстановяване");
        public static readonly BaseNomenclature Entry = new BaseNomenclature("entry", "Вписване");
        public static readonly BaseNomenclature Deletion = new BaseNomenclature("deletion", "Заличаване");


        public ValidationRecoveryNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Validation,
                Recovery,
                Entry,
                Deletion
            };
        }
    }
}
