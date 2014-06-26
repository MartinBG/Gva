using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class DegreeAccessRequestedPublicInformationNomenclature
    {
        public string Text { get; private set; }
        public string Uri { get; private set; }

        public static readonly DegreeAccessRequestedPublicInformationNomenclature FullAccess =
            new DegreeAccessRequestedPublicInformationNomenclature { Text = "Пълен достъп", Uri = "R-6072" };
        public static readonly DegreeAccessRequestedPublicInformationNomenclature PartialAccess =
            new DegreeAccessRequestedPublicInformationNomenclature { Text = "Частичен достъп", Uri = "R-6074" };

        public static readonly IEnumerable<DegreeAccessRequestedPublicInformationNomenclature> Values =
            new List<DegreeAccessRequestedPublicInformationNomenclature>
            {
                FullAccess,
                PartialAccess
            };
    }
}
