using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class DocCorrespondentContactDO
    {
        public DocCorrespondentContactDO()
        {
        }

        public DocCorrespondentContactDO(DocCorrespondentContact d)
        {
            if (d != null)
            {
                this.DocCorrespondentContactId = d.DocCorrespondentContactId;
                this.DocId = d.DocId;
                this.CorrespondentContactId = d.CorrespondentContactId;
                this.Version = d.Version;

                if (d.CorrespondentContact != null)
                {
                    this.CorrespondentContactName = d.CorrespondentContact.Name;
                }
            }
        }

        public int DocCorrespondentContactId { get; set; }
        public int DocId { get; set; }
        public int CorrespondentContactId { get; set; }
        public byte[] Version { get; set; }

        //
        public string CorrespondentFirstName { get; set; }
        public string CorrespondentSecondName { get; set; }
        public string CorrespondentLastName { get; set; }

        public string CorrespondentContactName { get; set; }

        public bool IsSelected { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }
    }
}
