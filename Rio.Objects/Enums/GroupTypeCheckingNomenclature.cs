using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class GroupTypeCheckingNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Conventional = new BaseNomenclature("01", "Проверка с конвенционален рентген");
        public static readonly BaseNomenclature Manual = new BaseNomenclature("02", "Ръчна проверка");
        public static readonly BaseNomenclature ETD = new BaseNomenclature("03", "Проверка за сигурност чрез ETD");
        public static readonly BaseNomenclature Instructors = new BaseNomenclature("04", "Инструктори");

        public GroupTypeCheckingNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Conventional,
                Manual,
                ETD,
                Instructors
            };
        }
    }
}
