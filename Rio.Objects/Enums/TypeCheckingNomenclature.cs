using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class TypeCheckingNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Conv1 = new BaseNomenclature("01", "CONV1 Ръчен багаж и носени вещи от персонала", "", GroupTypeCheckingNomenclature.Conventional.Value);
        public static readonly BaseNomenclature Conv2 = new BaseNomenclature("02", "CONV2 Регистриран багаж", "", GroupTypeCheckingNomenclature.Conventional.Value);
        public static readonly BaseNomenclature Conv3 = new BaseNomenclature("03", "CONV3 Товари, поща и други стоки и материали", "", GroupTypeCheckingNomenclature.Conventional.Value);
        public static readonly BaseNomenclature EDS = new BaseNomenclature("04", "EDS Проверка за сигурност на регистриран багаж чрез EDS", "", GroupTypeCheckingNomenclature.Conventional.Value);

        public static readonly BaseNomenclature HS1 = new BaseNomenclature("05", "HS1 Лица", "", GroupTypeCheckingNomenclature.Manual.Value);
        public static readonly BaseNomenclature HS2 = new BaseNomenclature("06", "HS2 Ръчен багаж и носени вещи", "", GroupTypeCheckingNomenclature.Manual.Value);
        public static readonly BaseNomenclature HS3 = new BaseNomenclature("07", "HS3 Регистриран багаж", "", GroupTypeCheckingNomenclature.Manual.Value);

        public static readonly BaseNomenclature ETD1 = new BaseNomenclature("08", "ETD1 Ръчен и регистриран багаж", "", GroupTypeCheckingNomenclature.ETD.Value);
        public static readonly BaseNomenclature ETD2 = new BaseNomenclature("09", "ETD2 Товари и поща", "", GroupTypeCheckingNomenclature.ETD.Value);
        public static readonly BaseNomenclature VEH = new BaseNomenclature("10", "VEH Проверка на МПС", "", GroupTypeCheckingNomenclature.ETD.Value);
        public static readonly BaseNomenclature SUB = new BaseNomenclature("11", "SUB Управление и контрол на служителите по сигурност", "", GroupTypeCheckingNomenclature.ETD.Value);
        public static readonly BaseNomenclature AC = new BaseNomenclature("12", "AC Контрол на достъпа, наблюдение и патрулиране", "", GroupTypeCheckingNomenclature.ETD.Value);

        public static readonly BaseNomenclature INS = new BaseNomenclature("13", "INS Инструктор по авиационна сигурност", "", GroupTypeCheckingNomenclature.Instructors.Value);

        public TypeCheckingNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Conv1,
                Conv2,
                Conv3,
                EDS,

                HS1,
                HS2,
                HS3,

                ETD1,
                ETD2,
                VEH,
                SUB, 
                AC,

                INS
            };
        }
    }
}
