using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class MEVerificationNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature InitialVerification  = new BaseNomenclature("01", "Първоначална", "");
        public static readonly BaseNomenclature SubsequentVerification  = new BaseNomenclature("02", "Последваща", "");

        public MEVerificationNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
               InitialVerification,
               SubsequentVerification
            }; 
        }
    }
}
