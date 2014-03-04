using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class DocClassificationDO
    {
        public DocClassificationDO()
        {
        }

        public DocClassificationDO(DocClassification d)
            : this()
        {
            if (d != null)
            {
                this.DocClassificationId = d.DocClassificationId;
                this.DocId = d.DocId;
                this.ClassificationId = d.ClassificationId;
                this.ClassificationByUserId = d.ClassificationByUserId;
                this.ClassificationDate = d.ClassificationDate;
                this.IsActive = d.IsActive;
                this.Version = d.Version;

                if (d.Classification != null)
                {
                    this.ClassificationName = d.Classification.Name;
                }
            }
        }

        public Nullable<int> DocClassificationId { get; set; }
        public Nullable<int> DocId { get; set; }
        public Nullable<int> ClassificationId { get; set; }
        public Nullable<int> ClassificationByUserId { get; set; }
        public System.DateTime ClassificationDate { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }

        //
        public string ClassificationName { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }
    }
}
