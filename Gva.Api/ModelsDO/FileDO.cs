using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Gva.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class FileDO
    {
        public FileDO()
        {
        }

        public FileDO(GvaLotFile lotFile)
        {
            this.LotFileId = lotFile.GvaLotFileId;
            this.File = new FileDataDO(lotFile);
            this.BookPageNumber = lotFile.PageIndex;
            this.PageCount = lotFile.PageNumber;

            this.CaseType = new NomValue()
            {
                NomValueId = lotFile.GvaCaseTypeId.Value,
                Name = lotFile.GvaCaseType.Name
            };

            this.IsDocFile = lotFile.DocFileId.HasValue;
            this.Applications = lotFile.GvaAppLotFiles
                .Where(af =>
                    af.GvaApplication != null &&
                    af.GvaApplication.GvaAppLotPart != null)
                .Select(af => new ApplicationNomDO(af.GvaApplication))
                .ToList();
        }

        public bool IsAdded { get;set; }

        public bool IsDeleted { get; set; }

        public int LotFileId { get; set; }

        public FileDataDO File { get; set; }

        public string BookPageNumber { get; set; }

        public int? PageCount { get; set; }

        public NomValue CaseType { get; set; }

        public bool IsDocFile { get; set; }

        public List<ApplicationNomDO> Applications { get; set; }
    }
}