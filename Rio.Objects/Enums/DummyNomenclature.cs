using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class DummyNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature FirstElement = new BaseNomenclature("01", "Елемент 1", "");
        public static readonly BaseNomenclature SecondElement = new BaseNomenclature("02", "Елемент 2", "");
        public static readonly BaseNomenclature ThirdElement = new BaseNomenclature("03", "Елемент 3", "");

        public DummyNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                FirstElement,
                SecondElement,
                ThirdElement
            };
        }
    }
}
