using Gva.Api.Models;
using System;
using System.Collections.Generic;
using Gva.Api.Models.Views.Organization;
namespace Gva.Api.ModelsDO
{
    public class OrganizationViewDO
    {
        public OrganizationViewDO(GvaViewOrganization o)
        {
            this.Id = o.LotId;
            this.Name = o.Name;
            this.Cao = o.Cao;
            this.Uin = o.Uin;
            this.Valid = o.Valid;
            this.OrganizationType = o.OrganizationType.Name;
            this.DateValidTo = o.DateValidTo;
            this.DateCaoValidTo = o.DateCaoValidTo;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Cao { get; set; }

        public string Uin { get; set; }

        public bool Valid { get; set; }

        public string OrganizationType { get; set; }

        public DateTime? DateValidTo { get; set; }

        public DateTime? DateCaoValidTo { get; set; }

        public List<string> CaseTypes { get; set; }

    }
}