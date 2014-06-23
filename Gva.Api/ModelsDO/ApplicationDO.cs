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

        public ApplicationDO(GvaApplication gvaApp, string lotSetAlias, int lotSetId)
            : this()
        {
            if (gvaApp != null)
            {
                this.ApplicationId = gvaApp.GvaApplicationId;
                this.DocId = gvaApp.DocId;
                this.LotId = gvaApp.LotId;
                this.GvaAppLotPartId = gvaApp.GvaAppLotPartId;
                this.LotSetAlias = lotSetAlias;
                this.LotSetId = lotSetId;
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
