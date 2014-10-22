using Common.Api.Models;
using Docs.Api.DataObjects;
using Docs.Api.Models;
using Gva.Api.Models;

namespace Gva.Api.ModelsDO.Applications
{
    public class DocCaseDO
    {
        public DocCaseDO()
        {
            this.BookPageNumber = null;
            this.PageCount = null;
            this.File = new BlobDO();
        }

        public DocCaseDO(DocFile docFile, GvaCaseType caseType)
            : this()
        {
            if (docFile != null)
            {
                this.DocFileId = docFile.DocFileId;
                this.Name = docFile.Name;

                this.File.Key = docFile.DocFileContentId;
                this.File.Name = docFile.DocFileName;
                this.File.RelativePath = ""; //?

                this.DocFileKind = new NomValue()
                {
                    NomValueId = docFile.DocFileKindId,
                    Name = docFile.DocFileKind.Name,
                    Alias = docFile.DocFileKind.Alias
                };
            }

            this.CaseType = new NomValue()
            {
                NomValueId = caseType.GvaCaseTypeId,
                Name = caseType.Name,
                Alias = caseType.Alias
            };
        }

        public int? DocFileId { get; set; }

        public NomValue DocFileKind { get; set; }

        public string Name { get; set; }

        public BlobDO File { get; set; }

        public string BookPageNumber { get; set; }

        public int? PageCount { get; set; }

        public NomValue CaseType { get; set; }
    }
}