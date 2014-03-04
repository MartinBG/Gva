using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    //public class DocInventoryItemDO
    //{
    //    public DocInventoryItemDO()
    //    {
    //    }

    //    public DocInventoryItemDO(DocInventoryItem d)
    //        : this()
    //    {
    //        if (d != null)
    //        {
    //            this.DocInventoryItemId = d.DocInventoryItemId;
    //            this.DocInventoryId = d.DocInventoryId;
    //            this.CaseDocId = d.CaseDocId;
    //            this.DocId = d.DocId;
    //            this.GivenDate = d.GivenDate;
    //            this.ReceivedDate = d.ReceivedDate;
    //            this.ModifyDate = d.ModifyDate;
    //            this.ModifyDate = d.ModifyDate;
    //            this.ModifyUserId = d.ModifyUserId;

    //            if (d.CaseDoc != null)
    //            {
    //                this.CsDocRegUri = d.CaseDoc.RegUri;
    //            }

    //            if (d.Doc != null)
    //            {
    //                this.DocSubject = d.Doc.DocSubject;
    //                this.DocRegUri = d.Doc.RegUri;
    //                this.DocRegDate = d.Doc.RegDate;

    //                if (d.Doc.DocDirection != null)
    //                {
    //                    this.DocDirectionName = d.Doc.DocDirection.Name;
    //                }

    //                if (d.Doc.DocType != null)
    //                {
    //                    this.DocTypeName = d.Doc.DocType.Name;
    //                }

    //                if (d.Doc.DocCorrespondents != null && d.Doc.DocCorrespondents.Any())
    //                {
    //                    StringBuilder sb = new StringBuilder();
    //                    foreach (var item in d.Doc.DocCorrespondents)
    //                    {
    //                        if (item.Correspondent != null)
    //                        {
    //                            if (sb.Length > 0)
    //                            {
    //                                sb.Append("<br/>");
    //                            }

    //                            sb.Append(item.Correspondent.DisplayName);
    //                        }
    //                    }

    //                    this.DocCorrespondent = sb.ToString();
    //                }
    //            }
    //        }
    //    }

    //    public DocInventoryItemDO(Doc d)
    //        : this()
    //    {
    //        if (d != null)
    //        {
    //            this.DocSubject = d.DocSubject;
    //            this.DocRegUri = d.RegUri;
    //            this.DocRegDate = d.RegDate;

    //            if (d.DocDirection != null)
    //            {
    //                this.DocDirectionName = d.DocDirection.Name;
    //            }

    //            if (d.DocType != null)
    //            {
    //                this.DocTypeName = d.DocType.Name;
    //            }

    //            if (d.DocCorrespondents != null && d.DocCorrespondents.Any())
    //            {
    //                StringBuilder sb = new StringBuilder();
    //                foreach (var item in d.DocCorrespondents)
    //                {
    //                    if (item.Correspondent != null)
    //                    {
    //                        if (sb.Length > 0)
    //                        {
    //                            sb.Append("<br/>");
    //                        }

    //                        sb.Append(item.Correspondent.DisplayName);
    //                    }
    //                }

    //                this.DocCorrespondent = sb.ToString();
    //            }
    //        }
    //    }

    //    public int DocInventoryItemId { get; set; }
    //    public int DocInventoryId { get; set; }
    //    public int CaseDocId { get; set; }
    //    public int DocId { get; set; }
    //    public Nullable<System.DateTime> GivenDate { get; set; }
    //    public Nullable<System.DateTime> ReceivedDate { get; set; }
    //    public Nullable<System.DateTime> ModifyDate { get; set; }
    //    public Nullable<int> ModifyUserId { get; set; }
    //    public byte[] Version { get; set; }

    //    public string DocDirectionName { get; set; }
    //    public string DocTypeName { get; set; }
    //    public string DocSubject { get; set; }
    //    public string DocRegUri { get; set; }
    //    public string DocCorrespondent { get; set; }
    //    public DateTime? DocRegDate { get; set; }

    //    public string CsDocRegUri { get; set; }

    //    public bool IsSelected { get; set; }
    //    public bool IsHidden { get; set; }
    //}
}
