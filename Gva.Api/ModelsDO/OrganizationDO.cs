using Gva.Api.Models;
using System;
using System.Collections.Generic;
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
            this.OrganizationType = o.OrganizationType;
            this.DateValidTo = o.DateValidTo;
            this.DateCAOValidTo = o.DateCAOValidTo;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string CAO { get; set; }

        public string Uin { get; set; }

        public string Valid { get; set; }

        public string OrganizationType { get; set; }

        public DateTime? DateValidTo { get; set; }

        public DateTime? DateCAOValidTo { get; set; }

        public List<string> CaseTypes { get; set; }

    }
}