using System;
using Common.Api.Models;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.ModelsDO.Persons
{
    public class GvaViewPersonLicenceEditionDO
    {
        public GvaViewPersonLicenceEditionDO(GvaLicenceEdition edition)
        {
            this.LotId = edition.LotId;
            this.PartIndex = edition.LicencePartIndex;
            this.EditionIndex = edition.EditionIndex;
            this.LicenceTypeId = edition.LicenceTypeId;
            this.StampNumber = edition.StampNumber;
            this.DateValidFrom = edition.DateValidFrom;
            this.DateValidTo = edition.DateValidTo;
            this.LicenceActionId = edition.LicenceActionId;
            if (edition.GvaStageId.HasValue)
            {
                this.IsReady = edition.GvaStageId <= GvaConstants.IsReadyApplication;
                this.IsReceived = edition.GvaStageId <= GvaConstants.IsReceivedApplication;
                this.IsDone = edition.GvaStageId <= GvaConstants.IsDoneApplication;
            }

            if (edition.LicenceAction != null)
            {
                this.LicenceActionName = edition.LicenceAction.Name;
            }

            this.LicenceNumber = edition.LicenceNumber;

            if (edition.Person != null)
            {
                this.Person = new PersonViewDO(edition.Person);
            }

            if (edition.Application != null)
            {
                this.Application = new ApplicationNomDO
                {
                    ApplicationName = edition.Application.ApplicationType.Name,
                    PartIndex = edition.Application.Part.Index,
                    ApplicationId = edition.GvaApplicationId.Value
                };
            }

            this.LicenceType = edition.LicenceType;
            this.LicencePartIndex = edition.LicencePartIndex;
            this.EditionPartIndex = edition.EditionPartIndex;
            this.FirstDocDateValidFrom = edition.FirstDocDateValidFrom;
            this.Valid = edition.Valid;
            this.LicenceTypeCode = edition.LicenceTypeCode;
            this.LicenceTypeCaCode = edition.LicenceTypeCaCode;
            this.PublisherCode = edition.PublisherCode;
        }

        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public int EditionIndex { get; set; }

        public int LicenceTypeId { get; set; }

        public string StampNumber { get; set; }

        public DateTime DateValidFrom { get; set; }

        public DateTime? DateValidTo { get; set; }

        public int LicenceActionId { get; set; }

        public string LicenceActionName { get; set; }

        public int? LicenceNumber { get; set; }

        public bool IsReceived { get; set; }

        public bool IsReady { get; set; }

        public bool IsDone { get; set; }

        public PersonViewDO Person { get; set; }

        public ApplicationNomDO Application { get; set; }

        public NomValue LicenceType { get; set; }

        public int LicencePartIndex { get; set; }

        public int EditionPartIndex { get; set; }

        public DateTime FirstDocDateValidFrom { get; set; }

        public bool Valid { get; set; }

        public string LicenceTypeCode { get; set; }

        public string LicenceTypeCaCode { get; set; }

        public string PublisherCode { get; set; }
    }
}
