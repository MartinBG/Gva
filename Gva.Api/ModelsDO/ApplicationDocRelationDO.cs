using Docs.Api.DataObjects;
using Docs.Api.Models;
using Gva.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gva.Api.ModelsDO
{
    public class ApplicationDocRelationDO
    {
        public ApplicationDocRelationDO()
        {
            this.ApplicationLotFiles = new List<ApplicationLotFileDO>();
        }

        public ApplicationDocRelationDO(DocRelation d)
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
                    this.DocDocDirectionName = d.Doc.DocDirection != null ? d.Doc.DocDirection.Name : string.Empty;
                    this.DocDocTypeName = d.Doc.DocType != null ? d.Doc.DocType.Name : string.Empty;
                    this.DocDocStatusName = d.Doc.DocStatus != null ? d.Doc.DocStatus.Name : string.Empty;
                    this.DocDocStatusAlias = d.Doc.DocStatus.Alias;
                    this.DocVersion = d.Doc.Version;
                }
            }
        }

        public int DocRelationId { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<int> ParentDocId { get; set; }
        public Nullable<int> RootDocId { get; set; }
        public byte[] Version { get; set; }

        public string DocDocDirectionName { get; set; }
        public string DocRegUri { get; set; }
        public string DocDocStatusName { get; set; }
        public string DocDocStatusAlias { get; set; }
        public byte[] DocVersion { get; set; }

        public string DocDocTypeName { get; set; }
        public string DocSubject { get; set; }

        public List<ApplicationLotFileDO> ApplicationLotFiles { get; set; }

        public string DocDataHtml
        {
            get
            {
                return string.Format("{0}:{1}<br/>{2}", this.DocDocDirectionName, this.DocRegUri, this.DocDocStatusName);
            }
        }

        public string DocDescriptionHtml
        {
            get
            {
                return string.Format("{0}: {1}", this.DocDocTypeName, this.DocSubject);
            }
        }

    }
}
