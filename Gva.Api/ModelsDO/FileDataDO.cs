using System;
using Gva.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class FileDataDO
    {
        public FileDataDO()
        {
        }

        public FileDataDO(GvaLotFile lotFile)
        {
            if (lotFile.DocFileId.HasValue)
            {
                this.Name = lotFile.DocFile.DocFileName;
                this.Key = lotFile.DocFile.DocFileContentId;
            }
            else
            {
                this.Name = lotFile.GvaFile.Filename;
                this.Key = lotFile.GvaFile.FileContentId;
            }
        }

        public Guid Key { get; set; }

        public string Name { get; set; }
    }
}
