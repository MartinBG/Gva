using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class DocRelationDO
    {
        public DocRelationDO()
        {
        }

        public DocRelationDO(DocRelation d)
            : this()
        {
            if (d != null)
            {
                this.DocRelationId = d.DocRelationId;
                this.DocId = d.DocId;
                this.ParentDocId = d.ParentDocId;
                this.RootDocId = d.RootDocId;
                this.Version = d.Version;

                if (d.Doc != null)
                {
                    this.DocRegUri = d.Doc.RegUri;
                    this.DocSubject = d.Doc.DocSubject;
                    this.DocRegDate = d.Doc.RegDate;
                    this.DocDocDirectionName = d.Doc.DocDirection != null ? d.Doc.DocDirection.Name : string.Empty;
                    this.DocStatusId = d.Doc.DocStatusId;
                    this.DocVersion = d.Doc.Version;

                    this.DocDocTypeName = d.Doc.DocType != null ? d.Doc.DocType.Name : string.Empty;
                    this.DocDocTypeId = d.Doc.DocType != null ? d.Doc.DocType.DocTypeId : (int?)null; 
                    this.DocDocStatusName = d.Doc.DocStatus != null ? d.Doc.DocStatus.Name : string.Empty;
                    this.DocDocCasePartTypeId = d.Doc.DocCasePartTypeId;
                    this.DocDocCasePartTypeName = d.Doc.DocCasePartType != null ? d.Doc.DocCasePartType.Name : string.Empty;
                    this.DocDocCasePartTypeAlias = d.Doc.DocCasePartType != null ? d.Doc.DocCasePartType.Alias : string.Empty;
                }
            }
        }

        public int DocRelationId { get; set; }
        public int? DocId { get; set; }
        public int? ParentDocId { get; set; }
        public int? RootDocId { get; set; }
        public byte[] Version { get; set; }

        public string DocRegUri { get; set; }
        public string DocSubject { get; set; }
        public DateTime? DocRegDate { get; set; }
        public string DocDocDirectionName { get; set; }
        public int? DocDocCasePartTypeId { get; set; }
        public string DocDocCasePartTypeName { get; set; }
        public string DocDocCasePartTypeAlias { get; set; }
        public int DocStatusId { get; set; }
        public string DocDocTypeName { get; set; }
        public int? DocDocTypeId { get; set; }
        public string DocDocStatusName { get; set; }
        public byte[] DocVersion { get; set; }

        public string Data
        {
            get
            {
                return string.Format("{0} {1}: {2}", this.DocRegUri, this.DocDocTypeName, this.DocSubject);
            }
        }
    }
}
