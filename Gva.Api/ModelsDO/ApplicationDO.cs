using Docs.Api.DataObjects;
using Gva.Api.Models;
using System;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO
{
    public class ApplicationDO
    {
        public ApplicationDO()
        {
            this.ApplicationDocCase = new List<ApplicationDocRelationDO>();
        }

        public ApplicationDO(GvaApplication g)
            : this()
        {
            if (g != null)
            {
                this.ApplicationId = g.GvaApplicationId;
                this.DocId = g.DocId;
                this.LotId = g.LotId;
                this.GvaAppLotPartId = g.GvaAppLotPartId;
            }
        }

        public int ApplicationId { get; set; }
        public int? DocId { get; set; }
        public int LotId { get; set; }
        public int? GvaAppLotPartId { get; set; }

        public PersonDO Person { get; set; }
        public List<ApplicationDocRelationDO> ApplicationDocCase { get; set; }
    }
}
