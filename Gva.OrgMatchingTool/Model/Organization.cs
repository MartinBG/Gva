using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gva.OrgMatchingTool.Model
{
    public class FmOrg
    {
        public string EIK { get; set; }

        public string TrimmedName { get; set; }

        public string Name { get; set; }

        public int Id { get; set; }
    }

    public class OrgMatch
    {
        public string EIK { get; set; }

        public string FmOrgName { get; set; }

        public int? FmOrgId { get; set; }

        public string ApexOrgNameEn { get; set; }

        public int? ApexOrgId { get; set; }

        public string ApexPersonNameEn { get; set; }

        public string ApexPersonNameBg { get; set; }

        public int? ApexPersonId { get; set; }

        public string MatchType { get; set; }
    }
}
