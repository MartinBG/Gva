using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class MEInitialVerificationNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature NewManufacture = new BaseNomenclature("01", "Ново производство", "");
        public static readonly BaseNomenclature Import = new BaseNomenclature("02", "Внос", "");

        public MEInitialVerificationNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
               NewManufacture,
               Import
            };
        }
    }
}
