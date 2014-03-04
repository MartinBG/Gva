using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class CorrespondentContactDO
    {
        public CorrespondentContactDO()
        {
        }

        public CorrespondentContactDO(CorrespondentContact c)
        {
            if (c != null)
            {
                this.CorrespondentContactId = c.CorrespondentContactId;
                this.CorrespondentId = c.CorrespondentId;
                this.Name = c.Name;
                this.UIN = c.UIN;
                this.Note = c.Note;
                this.IsActive = c.IsActive;
                this.Version = c.Version;

                if (c.Correspondent != null)
                {
                    this.CorrespondentDisplayName = c.Correspondent.DisplayName;
                }
            }
        }

        public Nullable<int> CorrespondentContactId { get; set; }
        public Nullable<int> CorrespondentId { get; set; }
        public string Name { get; set; }
        public string UIN { get; set; }
        public string Note { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }

        //
        public bool IsNew { get; set; }
        public bool IsDirty { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsInEdit { get; set; }

        public string CorrespondentDisplayName { get; set; }
    }
}
