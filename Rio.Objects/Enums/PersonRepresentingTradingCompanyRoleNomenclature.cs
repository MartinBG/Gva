using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class PersonRepresentingTradingCompanyRoleNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature Manager = new BaseNomenclature("01", "Управител", "");
        public static readonly BaseNomenclature Director = new BaseNomenclature("02", "Изпълнителен директор", "");
        public static readonly BaseNomenclature Council = new BaseNomenclature("03", "Член на съвета на директорите", "");

        public PersonRepresentingTradingCompanyRoleNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
                Manager,
                Director,
                Council
            };
        }
    }
}
