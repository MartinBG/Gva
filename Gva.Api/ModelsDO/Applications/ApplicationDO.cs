using System.Collections.Generic;
using Common.Api.Models;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.ModelsDO.Airports;
using Gva.Api.ModelsDO.Equipments;
using Gva.Api.ModelsDO.Persons;

namespace Gva.Api.ModelsDO.Applications
{
    public class ApplicationDO : ApplicationNomDO
    {
        public ApplicationDO()
        {
            this.AppDocCase = new List<ApplicationDocRelationDO>();
            this.AppFilesNotInCase = new List<ApplicationLotFileDO>();
        }

        public ApplicationDO(GvaApplication application, string lotSetAlias)
            : base(application)
        {
            this.DocId = application.DocId;
            this.LotId = application.LotId;
            this.PartIndex = application.GvaAppLotPart != null ? (int?)application.GvaAppLotPart.Index : null;
            this.LotSetAlias = lotSetAlias.ToLowerInvariant();
            this.ApplicationType = application.GvaViewApplication != null ? application.GvaViewApplication.ApplicationType : null;

            this.AppDocCase = new List<ApplicationDocRelationDO>();
            this.AppFilesNotInCase = new List<ApplicationLotFileDO>();
        }

        public int? DocId { get; set; }

        public int LotId { get; set; }

        public int? PartIndex { get; set; }

        public string LotSetAlias { get; set; }

        public NomValue ApplicationType { get; set; }

        public PersonViewDO Person { get; set; }

        public OrganizationViewDO Organization { get; set; }

        public AircraftViewDO Aircraft { get; set; }

        public AirportViewDO Airport { get; set; }

        public EquipmentViewDO Equipment { get; set; }

        public List<ApplicationDocRelationDO> AppDocCase { get; set; }

        public List<ApplicationLotFileDO> AppFilesNotInCase { get; set; }
    }
}
