using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class CourtCaseActKindNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Command = new BaseNomenclature("5007", "Заповед");
        public static readonly BaseNomenclature Definition = new BaseNomenclature("5002", "Определение");
        public static readonly BaseNomenclature Sentence = new BaseNomenclature("5003", "Присъда");
        public static readonly BaseNomenclature Protocol = new BaseNomenclature("5006", "Протокол");
        public static readonly BaseNomenclature Disposal = new BaseNomenclature("5005", "Разпореждане");
        public static readonly BaseNomenclature Decision = new BaseNomenclature("5001", "Решение");
        public static readonly BaseNomenclature Agreement = new BaseNomenclature("5004", "Споразумение");

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
