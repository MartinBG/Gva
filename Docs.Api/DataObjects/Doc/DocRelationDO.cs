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
                    this.DocDocTypeName = d.Doc.DocType != null ? d.Doc.DocType.Name : string.Empty;
                    this.DocDocStatusName = d.Doc.DocStatus != null ? d.Doc.DocStatus.Name : string.Empty;
                    this.DocDocCasePartTypeId = d.Doc.DocCasePartTypeId;
                    this.DocDocCasePartTypeName = d.Doc.DocCasePartType != null ? d.Doc.DocCasePartType.Name : string.Empty;
                    this.DocDocCasePartTypeAlias = d.Doc.DocCasePartType != null ? d.Doc.DocCasePartType.Alias : string.Empty;
                    //switch (this.DocDocCasePartTypeAlias)
                    //{
                    //    case "Public":
                    //    case "Internal":
                    //        this.DocDocCasePartTypeStyleColor = "black";
                    //        break;
                    //    case "Control":
                    //        this.DocDocCasePartTypeStyleColor = "red";
                    //        //if (d.Doc.DocCasePartMovements != null && d.Doc.DocCasePartMovements.Any())
                    //        //{
                    //        //    DocCasePartMovement lastMovement = d.Doc.DocCasePartMovements.OrderByDescending(e => e.MovementDate).FirstOrDefault();
                    //        //    if (lastMovement != null)
                    //        //    {
                    //        //        this.DocDocCasePartyIncontrolBy = string.Format("от {0} на {1:dd.MM.yyyy}", lastMovement.User.Username, lastMovement.MovementDate);
                    //        //    }
                    //        //}
                    //        break;
                    //    default:
                    //        this.DocDocCasePartTypeStyleColor = "black";
                    //        break;
                    //};
                }
            }
        }

        public int DocRelationId { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<int> ParentDocId { get; set; }
        public Nullable<int> RootDocId { get; set; }
        public byte[] Version { get; set; }

        public string DocRegUri { get; set; }
        public string DocSubject { get; set; }
        public Nullable<DateTime> DocRegDate { get; set; }
        public string DocDocDirectionName { get; set; }
        public Nullable<int> DocDocCasePartTypeId { get; set; }
        public string DocDocCasePartTypeName { get; set; }
        public string DocDocCasePartTypeAlias { get; set; }
        public int DocStatusId { get; set; }
        public string DocDocTypeName { get; set; }
        public string DocDocStatusName { get; set; }

        public string DocDataHtml
        {
            get
            {
                return string.Format("{0}<br/>{1} | {2}", this.DocRegUri, this.DocDocDirectionName, this.DocDocCasePartTypeName);
            }
        }

        public string DocDescriptionHtml
        {
            get
            {
                return string.Format("{0}: {1}", this.DocDocTypeName, this.DocSubject);
            }
        }

        //public string DocDocCasePartTypeStyleColor { get; set; }

        //?
        //public string DocDocCasePartyIncontrolBy { get; set; }
        //public string RootSubject { get; set; }

        //public string ParentSubject { get; set; }
        //public string ParentRegUri { get; set; }
        //public string ParentDocTypeName { get; set; }

        //public string ParentDocInfo
        //{
        //    get
        //    {
        //        return string.Format("{0}{1} {2}",
        //            this.ParentRegUri + (string.IsNullOrEmpty(this.ParentRegUri) ? string.Empty : " "),
        //            this.ParentDocTypeName,
        //            this.ParentSubject);
        //    }
        //}

        //public bool HasParentDocInfo
        //{
        //    get
        //    {
        //        return !string.IsNullOrWhiteSpace(this.ParentDocInfo);
        //    }
        //}

        //public List<DocRelationDO> Children { get; set; }
        //public int Level { get; set; }
        //public string LevelCss { get; set; }

        ///* DKH */
        //public string RegNumberCol
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(this.DocDocCasePartyIncontrolBy))
        //        {
        //            return string.Format("{0}<br/>{1} | {2}", this.DocRegUri, this.DocDocDirectionName, this.DocDocCasePartTypeName);
        //        }
        //        else
        //        {
        //            return string.Format("{0}<br/>{1} | {2}<br/>{3}", this.DocRegUri, this.DocDocDirectionName, this.DocDocCasePartTypeName, this.DocDocCasePartyIncontrolBy);
        //        }
        //    }
        //}

        //public string DescriptionCol
        //{
        //    get
        //    {
        //        return string.Format("{0}: {1}", this.DocDocTypeName, this.DocSubject);
        //    }
        //}

        //public string DocInfo
        //{
        //    get
        //    {
        //        return string.Format("{0}{1} {2}",
        //            this.DocRegUri + (string.IsNullOrEmpty(this.DocRegUri) ? string.Empty : " "),
        //            this.DocDocTypeName,
        //            this.DocSubject);
        //    }
        //}

        //public bool HasDocInfo
        //{
        //    get
        //    {
        //        return !string.IsNullOrWhiteSpace(this.DocInfo);
        //    }
        //}

        //public string AdditionalInfo { get; set; }
    }
}
