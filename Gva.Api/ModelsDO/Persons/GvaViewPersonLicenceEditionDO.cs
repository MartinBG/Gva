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
                this.IsReady = edition.GvaStageId >= GvaConstants.IsReadyLicence;
                this.IsReceived = edition.GvaStageId >= GvaConstants.IsReceivedLicence;
            }

            if (edition.OfficiallyReissuedStageId.HasValue)
            {
                this.IsOfficiallyReissued = true;
                this.IsReady = edition.OfficiallyReissuedStageId >= GvaConstants.IsReadyLicence;
                this.IsReceived = edition.OfficiallyReissuedStageId >= GvaConstants.IsReceivedLicence;
            }
            else
            {
                this.IsOfficiallyReissued = false;
            }

            this.LicenceAction = edition.LicenceAction;
            this.LicenceNumber = edition.LicenceNumber;

            if (edition.Person != null)
            {
                this.Person = new PersonViewDO(edition.Person);
            }

            if (edition.Application != null)
            {
                this.Application = new ApplicationNomDO(edition.GvaApplication);
            }

            this.LicenceType = edition.LicenceType;
            this.LicencePartIndex = edition.LicencePartIndex;
            this.EditionPartIndex = edition.EditionPartIndex;
            this.FirstDocDateValidFrom = edition.FirstDocDateValidFrom;
            this.Valid = edition.Valid;
            this.LicenceTypeCaCode = edition.LicenceTypeCaCode;
            this.PublisherCode = edition.PublisherCode;
            this.Inspector = edition.Inspector;
            this.ForeignLicence = string.Format("{0} {1}", edition.ForeignLicenceNumber, edition.ForeignPublisher);
            this.Notes = edition.Notes;
            this.StatusChange = edition.StatusChange;

            if (edition.LotFile != null)
            {
                this.PageCount = edition.LotFile.PageNumber;
                this.PageNumber = edition.LotFile.PageIndex;
                this.Case = new CaseDO(edition.LotFile);
            }

            this.Limitations = !string.IsNullOrEmpty(edition.Limitations) ? edition.Limitations.Replace(GvaConstants.ConcatenatingExp, ", ") : null;
        }

        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public int EditionIndex { get; set; }

        public int LicenceTypeId { get; set; }

        public string StampNumber { get; set; }

        public DateTime DateValidFrom { get; set; }

        public DateTime? DateValidTo { get; set; }

        public int LicenceActionId { get; set; }

        public int? LicenceNumber { get; set; }

        public bool IsReceived { get; set; }

        public bool IsReady { get; set; }

        public bool IsDone { get; set; }

        public PersonViewDO Person { get; set; }

        public ApplicationNomDO Application { get; set; }

        public NomValue LicenceType { get; set; }

        public NomValue LicenceAction { get; set; }

        public int LicencePartIndex { get; set; }

        public int EditionPartIndex { get; set; }

        public DateTime FirstDocDateValidFrom { get; set; }

        public bool Valid { get; set; }

        public string LicenceTypeCaCode { get; set; }

        public string PublisherCode { get; set; }

        public string Inspector { get; set; }

        public string ForeignLicence { get; set; }

        public string Notes { get; set; }

        public string StatusChange { get; set; }

        public string Limitations { get; set; }

        public string PageNumber { get; set; }

        public int? PageCount { get; set; }

        public CaseDO Case { get; set; }

        public bool IsOfficiallyReissued { get; set; }
    }
}
