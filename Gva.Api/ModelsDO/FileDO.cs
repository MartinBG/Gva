using System;

namespace Gva.Api.ModelsDO
{
    public class FileDO
    {
        public int LotFileId { get; set; }

        public FileDataDO File { get; set; }

        public string BookPageNumber { get; set; }

        public string PageCount { get; set; }
    }
}