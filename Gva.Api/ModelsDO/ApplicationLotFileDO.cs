using Docs.Api.Models;
using Gva.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class ApplicationLotFileDO
    {
        public ApplicationLotFileDO()
        {
            this.File = new BlobDO();
        }

        public ApplicationLotFileDO(GvaAppLotFile appFile, DocFile docFile)
            : this()
        {
            this.HasAppFile = appFile == null ? false : true;

            if (appFile != null)
            {
                this.GvaLotFileId = appFile.GvaLotFileId;
                this.PartIndex = appFile.GvaLotFile.LotPart.Index;
                this.SetPartName = appFile.GvaLotFile.LotPart.SetPart.Name;
                this.SetPartAlias = appFile.GvaLotFile.LotPart.SetPart.Alias;
                this.PageNumber = appFile.GvaLotFile.PageNumber;
                this.PageIndex = appFile.GvaLotFile.PageIndex;

                if (appFile.GvaLotFile.GvaCaseType != null)
                {
                    this.GvaCaseTypeName = appFile.GvaLotFile.GvaCaseType.Name;
                }

                if (appFile.DocFile != null)
                {
                    this.DocFileId = appFile.DocFile.DocFileId;
                    this.DocFileKindId = appFile.DocFile.DocFileKindId;
                    this.DocFileTypeId = appFile.DocFile.DocFileTypeId;
                    this.DocId = appFile.DocFile.DocId;
                    this.Name = appFile.DocFile.Name;

                    this.File.Key = appFile.DocFile.DocFileContentId;
                    this.File.Name = appFile.DocFile.DocFileName;
                }
                else if (appFile.GvaLotFile.GvaFile != null)
                {
                    this.File.Key = appFile.GvaLotFile.GvaFile.FileContentId;
                    this.File.Name = appFile.GvaLotFile.GvaFile.Filename;
                }
                else if (appFile.GvaLotFile.DocFile != null)
                {
                    this.File.Key = appFile.GvaLotFile.DocFile.DocFileContentId;
                    this.File.Name = appFile.GvaLotFile.DocFile.DocFileName;
                }
            }
            else if (docFile != null)
            {
                this.DocFileId = docFile.DocFileId;
                this.DocFileKindId = docFile.DocFileKindId;
                this.DocFileTypeId = docFile.DocFileTypeId;
                this.DocId = docFile.DocId;
                this.Name = docFile.Name;

                this.File.Key = docFile.DocFileContentId;
                this.File.Name = docFile.DocFileName;
            }
        }

        public bool HasAppFile { get; set; }
        public int GvaLotFileId { get; set; }
        public int? PartIndex { get; set; }
        public string SetPartName { get; set; }
        public string SetPartAlias { get; set; }
        public int? PageNumber { get; set; }
        public string PageIndex { get; set; }
        public string GvaCaseTypeName { get; set; }
        
        public int? DocFileId { get; set; }
        public int DocFileKindId { get; set; }
        public int DocFileTypeId { get; set; }
        public int? DocId { get; set; }
        public string Name { get; set; }

        public BlobDO File { get; set; }
        public string FileUrl { get; set; }
    }
}
