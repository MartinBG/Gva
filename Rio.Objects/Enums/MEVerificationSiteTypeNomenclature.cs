using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class MEVerificationSiteTypeNomenclature : BaseNomenclature
    {
        public static readonly BaseNomenclature ApplicantPlace  = new BaseNomenclature("01", "На място при заявителя", "");
        public static readonly BaseNomenclature Laboratory  = new BaseNomenclature("02", "В лаборатория", "");
        public static readonly BaseNomenclature Post  = new BaseNomenclature("03", "На пункт", "");

        public MEVerificationSiteTypeNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
               ApplicantPlace,
               Laboratory,
               Post
            }; 
        }
    }
}
