﻿using Docs.Api.Models;
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

        public ApplicationLotFileDO(DocFile docFile, GvaAppLotFile appFile)
            : this()
        {
            this.HasAppFile = appFile == null ? false : true;
            if (appFile != null)
            {
                this.PartIndex = appFile.GvaLotFile.LotPart.Index;
                this.SetPartName = appFile.GvaLotFile.LotPart.SetPart.Name;
                this.SetPartAlias = appFile.GvaLotFile.LotPart.SetPart.Alias;
                this.PageNumber = appFile.GvaLotFile.PageNumber;
                this.PageIndex = appFile.GvaLotFile.PageIndex;
            }

            if (docFile != null)
            {
                this.DocFileId = docFile.DocFileId;
                this.DocFileKindId = docFile.DocFileKindId;
                this.DocFileTypeId = docFile.DocFileTypeId;
                this.DocId = docFile.DocId;
                this.Name = docFile.Name;

                this.File.Key = docFile.DocFileContentId;
                this.File.Name = docFile.DocFileName;
                this.File.RelativePath = "";
                this.DocFileUrl = string.Format("api/file?fileKey={0}&fileName={1}", docFile.DocFileContentId, docFile.DocFileName);
            }
        }

        public string PageIndex { get; set; }
        public int? PageNumber { get; set; } 
        public int? PartIndex { get; set; }
        public string SetPartName { get; set; }
        public string SetPartAlias { get; set; }
        public bool HasAppFile { get; set; }

        public int? DocFileId { get; set; }
        public int DocFileKindId { get; set; }
        public int DocFileTypeId { get; set; }
        public int? DocId { get; set; }
        public string Name { get; set; }

        public BlobDO File { get; set; }

        public string DocFileUrl { get; set; }
    }
}