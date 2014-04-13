using System;
using Docs.Api.Models;
using Gva.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class FileDataDO
    {
        public FileDataDO()
        {
        }

        public FileDataDO(DocFile docFile)
        {
            this.Name = docFile.DocFileName;
            this.Key = docFile.DocFileContentId;
        }

        public FileDataDO(GvaFile gvaFile)
        {
            this.Name = gvaFile.Filename;
            this.MimeType = gvaFile.MimeType;
            this.Key = gvaFile.FileContentId;
        }

        public Guid Key { get; set; }

        public string Name { get; set; }

        public string MimeType { get; set; }
    }
}
