using Docs.Api.DataObjects;
using Gva.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gva.Api.ModelsDO
{
    public class ApplicationDO
    {
        public ApplicationDO()
        {
            this.AppDocCase = new List<ApplicationDocRelationDO>();
            this.AppFilesNotInCase = new List<ApplicationLotFileDO>();
            
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
                this.LotSetAlias = g.Lot.Set.Alias;
                this.LotSetId = g.Lot.SetId;
            }
        }

        public int ApplicationId { get; set; }
        public int? DocId { get; set; }
        public int LotId { get; set; }
        public int? GvaAppLotPartId { get; set; }

        public string LotSetAlias { get; set; }
        public int LotSetId { get; set; }
        public PersonDO Person { get; set; }
        public OrganizationDO Organization { get; set; }
        public AircraftDO Aircraft { get; set; }
        public AirportDO Airport { get; set; }
        public EquipmentDO Equipment { get; set; }

        public List<ApplicationDocRelationDO> AppDocCase { get; set; }

        public List<ApplicationLotFileDO> AppFilesNotInCase { get; set; }

        public List<ApplicationLotObjectDO> AppLotObjects { get; set; }
    }
}
