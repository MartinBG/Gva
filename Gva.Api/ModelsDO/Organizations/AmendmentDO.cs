using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO.Organizations
{
    public class AmendmentDO
    {
        public AmendmentDO()
        {
            this.lims147 = new List<Lims147DO>();
            this.lims145 = new List<Lims145DO>();
            this.limsMG = new List<LimsMGDO>();
            this.includedDocuments = new List<IncludedDocumentDO>();
            this.Applications = new List<ApplicationNomDO>();
        }

        public string documentNumber { get; set; }

        public string documentDateIssue { get; set; }

        public int? changeNum { get; set; }

        public List<Lims147DO> lims147 { get; set; }

        public List<Lims145DO> lims145 { get; set; }

        public List<LimsMGDO> limsMG { get; set; }

        public List<IncludedDocumentDO> includedDocuments { get; set; }

        public List<ApplicationNomDO> Applications { get; set; }
    }
}
