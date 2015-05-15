using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;
using Docs.Api.Models;

namespace Docs.Api.DataObjects
{
    public class DocFileDO
    {
        public DocFileDO()
        {
            this.File = new BlobDO();
        }

        public DocFileDO(DocFile d)
            : this()
        {
            if (d != null)
            {
                this.DocFileId = d.DocFileId;
                this.DocId = d.DocId;
                this.DocFileKindId = d.DocFileKindId;
                this.DocFileTypeId = d.DocFileTypeId;
                this.Name = d.Name;

                this.File.Key = d.DocFileContentId;
                this.File.Name = d.DocFileName;
                this.File.MimeType = MimeTypeHelper.GetFileMimeTypeByExtenstion(Path.GetExtension(d.DocFileName));
                this.File.RelativePath = ""; //?
                this.DocFileUrl = string.Format(
                    "api/file?fileKey={0}&fileName={1}&mimeType={2}",
                    d.DocFileContentId,
                    d.DocFileName,
                    this.File.MimeType);

                if (d.DocFileKind != null)
                {
                    this.DocFileKindAlias = d.DocFileKind.Alias;
                }

                if (d.DocFileType != null)
                {
                    this.DocFileTypealias = d.DocFileType.Alias;
                }

                if (d.DocFileOriginType != null)
                {
                    this.IsElectronicApplication = d.DocFileOriginType.Alias == "EApplication";
                    this.IsEditableFile = d.DocFileOriginType.Alias == "EditableFile";
                }
            }
        }

        public int? DocFileId { get; set; }
        public int? DocId { get; set; }
        public int DocFileKindId { get; set; }
        public int DocFileTypeId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDirty { get; set; }
        public bool IsInEdit { get; set; }
        public bool IsNew { get; set; }

        public BlobDO File { get; set; }

        public string DocFileUrl { get; set; }
        public string DocFileKindAlias { get; set; }
        public string DocFileTypealias { get; set; }

        public bool? IsElectronicApplication { get; set; }
        public bool? IsEditableFile { get; set; }

        //? implementation for xml files
        //public Nullable<int> TicketId { get; set; }
    }
}
