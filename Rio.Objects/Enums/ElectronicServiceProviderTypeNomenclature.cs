using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ElectronicServiceProviderTypeNomenclature
    {
        public string Text { get; private set; }
        public string Uri { get; private set; }

        public static readonly ElectronicServiceProviderTypeNomenclature AdministrativeBody =
            new ElectronicServiceProviderTypeNomenclature { Text = "Административен орган", Uri = "0006-000031" };
        public static readonly ElectronicServiceProviderTypeNomenclature PublicPerson =
            new ElectronicServiceProviderTypeNomenclature { Text = "Лице, осъществяващо публични функции", Uri = "0006-000032" };
        public static readonly ElectronicServiceProviderTypeNomenclature GovernmentOrganization =
            new ElectronicServiceProviderTypeNomenclature { Text = "Организация, предоставящи обществени услуги", Uri = "0006-000033" };

        public static readonly IEnumerable<ElectronicServiceProviderTypeNomenclature> Values =
            new List<ElectronicServiceProviderTypeNomenclature>
            {
                AdministrativeBody,
                PublicPerson,
                GovernmentOrganization,
            };
    }
}
