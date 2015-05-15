using System;
using System.IO;
using Common.Extensions;
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
            this.MimeType =
                MimeTypeHelper.GetFileMimeTypeByExtenstion(Path.GetExtension(docFile.DocFileName));
        }

        public FileDataDO(GvaFile gvaFile)
        {
            this.Name = gvaFile.Filename;
            this.Key = gvaFile.FileContentId;
            this.MimeType = gvaFile.MimeType ??
                MimeTypeHelper.GetFileMimeTypeByExtenstion(Path.GetExtension(gvaFile.Filename));
        }

        public Guid Key { get; set; }

        public string Name { get; set; }

        public string MimeType { get; set; }
    }
}
