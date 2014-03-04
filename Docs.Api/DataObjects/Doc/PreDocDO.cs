using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class PreDocDO
    {
        public PreDocDO()
        {
            this.DocNumbers = 1;
            //this.Correspondents = new List<int>();
        }

        public PreDocDO(Doc doc)
            : this()
        {
            if (doc != null)
            {
                this.DocId = doc.DocId;
                if (doc.DocRelations != null && doc.DocRelations.Any())
                {
                    this.ParentDocId = doc.DocRelations.First().ParentDocId;
                }
                this.DocFormatTypeId = doc.DocFormatTypeId;
                this.DocCasePartTypeId = doc.DocCasePartTypeId;
                this.DocDirectionId = doc.DocDirectionId;
                if (doc.DocType != null)
                {
                    this.DocTypeGroupId = doc.DocType.DocTypeGroupId;
                }
                this.DocTypeId = doc.DocTypeId;
                this.DocSubject = doc.DocSubject;

                if (doc.DocCorrespondents != null && doc.DocCorrespondents.Any())
                {
                    //?
                    this.Correspondents = doc.DocCorrespondents.First().CorrespondentId;
                }
            }
        }

        public Nullable<int> DocId { get; set; }
        public Nullable<int> ParentDocId { get; set; }
        public Nullable<int> DocFormatTypeId { get; set; }
        public Nullable<int> DocCasePartTypeId { get; set; }
        public int DocDirectionId { get; set; }
        public Nullable<int> DocTypeGroupId { get; set; }
        public Nullable<int> DocTypeId { get; set; }
        public string DocSubject { get; set; }

        public Nullable<int> Correspondents { get; set; }
        //?
        //public List<int> Correspondents { get; set; }

        public bool Register { get; set; }
        public int DocNumbers { get; set; }
    }
}
