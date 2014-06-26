using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ApplicationTypeNomenclature
    {
        public string Text { get; set; }
        public string Uri { get; set; }

        public static readonly ApplicationTypeNomenclature InitialApplication = new ApplicationTypeNomenclature { Text = "Първоначално заявление", Uri = "0006-000121" };
        public static readonly ApplicationTypeNomenclature UpdatingApplication = new ApplicationTypeNomenclature { Text = "Заявление за промяна или допълване на данни в първоначално подадено заявление", Uri = "0006-000122" };
        public static readonly ApplicationTypeNomenclature CorrectionApplication = new ApplicationTypeNomenclature { Text = "Заявление за отстраняване на нередовности или предоставяне на информация", Uri = "0006-000123" };

        public static readonly IEnumerable<ApplicationTypeNomenclature> Values =
            new List<ApplicationTypeNomenclature>
            {
                InitialApplication,
                UpdatingApplication,
                CorrectionApplication,
            };
    }
}
