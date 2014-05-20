using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gva.OrgMatchingTool.Model
{
    public class Organization
    {
        public string EIK { get; set; }
        public string TrimmedName { get; set; }
        public string Name { get; set; }
        public string MatchedOrgTrimmedName { get; set; }
        public string MatchedPersonTrimmedName { get; set; }
        public string MatchedOrgName { get; set; }
        public string MatchedPersonName { get; set; }
        public string MatchedPersonNameBgEn { get; set; }
        public string MatchType { get; set; }
        public int MatchCount { get; set; }
    }
}
