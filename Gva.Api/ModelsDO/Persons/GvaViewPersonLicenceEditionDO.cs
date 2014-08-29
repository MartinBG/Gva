using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.ModelsDO.Persons
{
    public class GvaViewPersonLicenceEditionDO
    {
        public GvaViewPersonLicenceEditionDO(GvaViewPersonLicenceEdition edition, int? stageId = null, bool isReady = false, bool isReceived = false)
        {
            this.LotId = edition.LotId;
            this.PartIndex = edition.Part.Index;
            this.EditionIndex = edition.EditionIndex;
            this.LicenceTypeId = edition.LicenceTypeId;
            this.StampNumber = edition.StampNumber;
            this.DateValidFrom = edition.DateValidFrom;
            this.DateValidTo = edition.DateValidTo;
            this.LicenceActionId = edition.LicenceActionId;
            this.LicenceActionId = edition.LicenceActionId;
            this.IsReady = isReady;
            this.IsReceived = isReady;

            if (edition.LicenceActionId != null) 
            {
                this.LicenceActionName = edition.LicenceAction.Name;
            }
            if (stageId.HasValue)
            {
                this.StageId = stageId.Value;
            }

            this.LicenceNumber = edition.LicenceNumber;
            this.Person = new PersonViewDO(edition.Person);

            if (edition.ApplicationPartIndex.HasValue)
            {
                this.Application = new ApplicationNomDO
                {
                    ApplicationId = edition.GvaApplicationId.Value,
                    ApplicationName = edition.ApplicationName,
                    PartIndex = edition.ApplicationPartIndex
                };
            }
        }

        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public int EditionIndex { get; set; }

        public int LicenceTypeId { get; set; }

        public string StampNumber { get; set; }

        public DateTime DateValidFrom { get; set; }

        public DateTime DateValidTo { get; set; }

        public int LicenceActionId { get; set; }

        public string LicenceActionName { get; set; }

        public string LicenceNumber { get; set; }

        public int StageId { get; set; }

        public bool IsReceived { get; set; }

        public bool IsReady { get; set; }

        public PersonViewDO Person { get; set; }

        public ApplicationNomDO Application { get; set; }
    }
}
