using System;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO
{
    public class FileDO
    {
        public FileDO()
        {
            this.Applications = new List<ApplicationDO>();
        }

        public int LotFileId { get; set; }

        public FileDataDO File { get; set; }

        public string BookPageNumber { get; set; }

        public string PageCount { get; set; }

        public bool IsDocFile { get; set; }

        public List<ApplicationDO> Applications { get; set; }
    }
}