using System.Collections.Generic;
using Docs.Api.DataObjects;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Airports;
using Gva.Api.ModelsDO.Equipments;
using Gva.Api.ModelsDO.Persons;

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
        public PersonViewDO Person { get; set; }
        public OrganizationDO Organization { get; set; }
        public AircraftViewDO Aircraft { get; set; }
        public AirportViewDO Airport { get; set; }
        public EquipmentViewDO Equipment { get; set; }

        public List<ApplicationDocRelationDO> AppDocCase { get; set; }

        public List<ApplicationLotFileDO> AppFilesNotInCase { get; set; }

        public List<ApplicationLotObjectDO> AppLotObjects { get; set; }
    }
}
