using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class CourtCaseActKindNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Command = new BaseNomenclature("1", "Заповед");
        public static readonly BaseNomenclature Definition = new BaseNomenclature("2", "Определение");
        public static readonly BaseNomenclature Sentence = new BaseNomenclature("3", "Присъда");
        public static readonly BaseNomenclature Protocol = new BaseNomenclature("4", "Протокол");
        public static readonly BaseNomenclature Disposal = new BaseNomenclature("5", "Разпореждане");
        public static readonly BaseNomenclature Decision = new BaseNomenclature("6", "Решение");
        public static readonly BaseNomenclature Agreement = new BaseNomenclature("7", "Споразумение");

        public CourtCaseActKindNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Command,
                Definition,
                Disposal, 
                Decision, 
            };
        }
    }
}
