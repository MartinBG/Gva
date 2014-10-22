using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Gva.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class CaseDO
    {
        public CaseDO()
        {
            this.IsAdded = true;
            this.Applications = new List<ApplicationNomDO>();
        }

        public CaseDO(GvaLotFile lotFile)
        {
            this.LotFileId = lotFile.GvaLotFileId;
            if (lotFile.DocFile != null)
            {
                this.File = new FileDataDO(lotFile.DocFile);
            }
            else if (lotFile.GvaFile != null)
            {
                this.File = new FileDataDO(lotFile.GvaFile);
            }

            this.BookPageNumber = lotFile.PageIndex;
            this.PageCount = lotFile.PageNumber;

            this.CaseType = new NomValue()
            {
                NomValueId = lotFile.GvaCaseTypeId,
                Name = lotFile.GvaCaseType.Name,
                Alias = lotFile.GvaCaseType.Alias
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

        public int? LotFileId { get; set; }

        public FileDataDO File { get; set; }

        public string BookPageNumber { get; set; }

        public int? PageCount { get; set; }

        public NomValue CaseType { get; set; }

        public bool IsDocFile { get; set; }

        public List<ApplicationNomDO> Applications { get; set; }
    }
}