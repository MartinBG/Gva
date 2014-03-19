using System;
using System.Collections.Generic;
using Common.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class FileDO
    {
        public FileDO()
        {
            this.Applications = new List<ApplicationNomDO>();
        }

        public int LotFileId { get; set; }

        public FileDataDO File { get; set; }

        public string BookPageNumber { get; set; }

        public int? PageCount { get; set; }

        public NomValue CaseType { get; set; }

        public bool IsDocFile { get; set; }

        public List<ApplicationNomDO> Applications { get; set; }
    }
}