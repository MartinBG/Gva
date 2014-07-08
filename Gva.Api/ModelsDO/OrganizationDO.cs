using Gva.Api.Models;
using System;
using System.Collections.Generic;
using Gva.Api.Models.Views.Organization;
namespace Gva.Api.ModelsDO
{
    public class OrganizationDO
    {
        public OrganizationDO(GvaViewOrganization o)
        {
            this.Id = o.LotId;
            this.Name = o.Name;
            this.CAO = o.CAO;
            this.Uin = o.Uin;
            this.Valid = o.Valid;
            this.OrganizationType = o.OrganizationType.Name;
            this.DateValidTo = o.DateValidTo;
            this.DateCAOValidTo = o.DateCAOValidTo;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string CAO { get; set; }

        public string Uin { get; set; }

        public bool Valid { get; set; }

        public string OrganizationType { get; set; }

        public DateTime? DateValidTo { get; set; }

        public DateTime? DateCAOValidTo { get; set; }

        public List<string> CaseTypes { get; set; }

    }
}