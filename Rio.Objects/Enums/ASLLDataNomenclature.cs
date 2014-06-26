using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ASLLDataNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature WorkingFour = new BaseNomenclature("L4", "Работно (Ниво 4)", "");
        public static readonly BaseNomenclature ExtendedFive = new BaseNomenclature("L5", "Разширено (Ниво 5)", "");
        public static readonly BaseNomenclature ExpertSix = new BaseNomenclature("L6", "Експерт (Ниво 6)", "");
        public static readonly BaseNomenclature EnglishFour = new BaseNomenclature("L4", "English Level 4", "");
        public static readonly BaseNomenclature EnglishFive = new BaseNomenclature("L5", "English Level 5", "");
        public static readonly BaseNomenclature EnglishSix = new BaseNomenclature("L6", "English Level 6", "");
        public static readonly BaseNomenclature BulgarianFour = new BaseNomenclature("B4", "Bulgarian Level 4", "");
        public static readonly BaseNomenclature BulgarianFive = new BaseNomenclature("B5", "Bulgarian Level 5", "");
        public static readonly BaseNomenclature BulgarianSix = new BaseNomenclature("B6", "Bulgarian Level 6", "");

        public ASLLDataNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                WorkingFour,
                ExtendedFive,
                ExpertSix,
                EnglishFour,
                EnglishFive,
                EnglishSix,
                BulgarianFour,
                BulgarianFive,
                BulgarianSix
            };
        }
    }
}
