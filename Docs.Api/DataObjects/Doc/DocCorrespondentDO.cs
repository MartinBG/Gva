using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class DocCorrespondentDO
    {
        public DocCorrespondentDO()
        {
        }

        public DocCorrespondentDO(Correspondent c, bool isSelected = false, bool isNew = false, bool isDeleted = false)
        {
            this.DocCorrespondentId = null;
            this.DocId = null;

            if (c != null)
            {
                this.CorrespondentId = c.CorrespondentId;
                this.CorrespondentDisplayName = c.DisplayName;
                this.CorrespondentEmail = c.Email;

                if (c.CorrespondentType != null)
                {
                    this.CorrespondentCorrespondentTypeName = c.CorrespondentType.Name;
                }

                this.CorrespondentRegisterIndexCodeName = c.RegisterIndex != null ? string.Format("{0} {1}", c.RegisterIndex.Code, c.RegisterIndex.Name) : string.Empty;
            }

            this.IsSelected = isSelected;
            this.IsNew = isNew;
            this.IsDeleted = isDeleted;
        }

        public DocCorrespondentDO(DocCorrespondent d)
        {
            if (d != null)
            {
                this.DocCorrespondentId = d.DocCorrespondentId;
                this.DocId = d.DocId;
                this.CorrespondentId = d.CorrespondentId;
                this.Version = d.Version;

                if (d.Correspondent != null)
                {
                    this.CorrespondentDisplayName = d.Correspondent.DisplayName;
                    this.CorrespondentEmail = d.Correspondent.Email;
                    this.CorrespondentCorrespondentTypeName = d.Correspondent.CorrespondentType != null ? d.Correspondent.CorrespondentType.Name : string.Empty;
                    this.CorrespondentRegisterIndexCodeName = d.Correspondent.RegisterIndex != null ? string.Format("{0} {1}", d.Correspondent.RegisterIndex.Code, d.Correspondent.RegisterIndex.Name) : string.Empty;

                    this.BgCitizenFirstName = d.Correspondent.BgCitizenFirstName;
                    this.BgCitizenLastName = d.Correspondent.BgCitizenLastName;
                    this.BgCitizenUIN = d.Correspondent.BgCitizenUIN;
                    this.LegalEntityName = d.Correspondent.LegalEntityName;
                    this.LegalEntityBulstat = d.Correspondent.LegalEntityBulstat;
                }
            }
        }

        public int? DocCorrespondentId { get; set; }
        public int? DocId { get; set; }
        public int CorrespondentId { get; set; }
        public byte[] Version { get; set; }

        //
        public bool IsSelected { get; set; }
        public bool IsNew { get; set; }
        public bool IsDeleted { get; set; }

        public string CorrespondentDisplayName { get; set; }
        public string CorrespondentEmail { get; set; }
        public string CorrespondentCorrespondentTypeName { get; set; }
        public string CorrespondentRegisterIndexCodeName { get; set; }

        public string BgCitizenFirstName { get; set; }
        public string BgCitizenLastName { get; set; }
        public string BgCitizenUIN { get; set; }
        public string LegalEntityName { get; set; }
        public string LegalEntityBulstat { get; set; }
    }
}
