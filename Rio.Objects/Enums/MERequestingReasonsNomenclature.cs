using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class MERequestingReasonsNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature NewManufactureImport = new BaseNomenclature("01", "ново производство или внос", "");
        public static readonly BaseNomenclature ModificationAdditionApprovedType = new BaseNomenclature("02", "модификация или допълнение на одобрен тип", "");
        public static readonly BaseNomenclature NewTechnologies = new BaseNomenclature("03", "нови технологии", "");
        public static readonly BaseNomenclature ExpiredApproval = new BaseNomenclature("04", "изтекъл срок на валидност на одобряването", "");

        public MERequestingReasonsNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
               NewManufactureImport,
               ModificationAdditionApprovedType,
               NewTechnologies,
               ExpiredApproval
            };
        }
    }
}
