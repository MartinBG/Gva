using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class DocListItemDO
    {
        public DocListItemDO()
        {
            this.DocCorrespondents = new List<DocCorrespondentDO>();
        }

        public DocListItemDO(Doc d, UnitUser unitUser = null)
            : this()
        {
            if (d != null)
            {
                this.DocId = d.DocId;
                this.RegDate = d.RegDate;
                this.RegUri = d.RegUri;
                this.DocSubject = d.DocSubject;

                if (d.DocStatus != null)
                {
                    this.DocStatusName = d.DocStatus.Name;
                }

                if (d.DocType != null)
                {
                    this.DocTypeName = d.DocType.Name;
                }

                if (d.DocDirection != null)
                {
                    this.DocDirectionName = d.DocDirection.Name;
                }

                if (d.DocHasReads != null && d.DocHasReads.Any())
                {
                    this.IsRead = d.DocHasReads.Any(e => e.HasRead);
                }

                if (d.DocSourceType != null)
                {
                    this.IsElectronic = d.DocSourceType.Alias == "Internet";
                }
            }
        }

        public int? DocId { get; set; }
        public DateTime? RegDate { get; set; }
        public string RegUri { get; set; }
        public string DocSubject { get; set; }

        public string DocDirectionName { get; set; }
        public string DocTypeName { get; set; }
        public string DocStatusName { get; set; }
        public List<DocCorrespondentDO> DocCorrespondents { get; set; }
        public string CorrespondentNames
        {
            get
            {
                StringBuilder sb = new StringBuilder("");
                if (this.DocCorrespondents.Any())
                {
                    foreach (var dc in this.DocCorrespondents)
                    {
                        if (!string.IsNullOrEmpty(dc.CorrespondentDisplayName))
                        {
                            if (sb.Length > 0)
                            {
                                sb.Append("<br/>");
                            }
                            sb.Append(dc.CorrespondentDisplayName);
                        }
                    }
                }

                return sb.ToString();
            }
        }

        //? in list do we show that information
        //public string DocCasePartTypeAlias { get; set; }
        //public string DocCasePartTypeName { get; set; }

        public bool IsSelected { get; set; }
        public bool IsRead { get; set; }
        public bool IsElectronic { get; set; }

        #region ForManagement/ForControl

        public DocRelationDO CaseDocRelation { get; set; }

        #endregion
    }
}
