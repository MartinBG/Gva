using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ServiceTermTypeNomenclature
    {
        public string Text { get; private set; }
        public string Uri { get; private set; }

        public static readonly ServiceTermTypeNomenclature Normal = new ServiceTermTypeNomenclature { Text = "Обикновена", Uri = "0006-000083" };
        public static readonly ServiceTermTypeNomenclature Fast = new ServiceTermTypeNomenclature { Text = "Бърза", Uri = "0006-000084" };
        public static readonly ServiceTermTypeNomenclature Express = new ServiceTermTypeNomenclature { Text = "Експресна", Uri = "0006-000085" };

        public static readonly IEnumerable<ServiceTermTypeNomenclature> Values =
            new List<ServiceTermTypeNomenclature>
            {
                Normal,
                Fast,
                Express,
            };
    }
}
