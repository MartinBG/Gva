using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class PrintedLicenceRatingEditionProjection : Projection<GvaViewPrintedRatingEdition>
    {
        public PrintedLicenceRatingEditionProjection(
            IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
        }

        public override IEnumerable<GvaViewPrintedRatingEdition> Execute(PartCollection parts)
        {
            List<GvaViewPrintedRatingEdition> printedRatingEditions = new List<GvaViewPrintedRatingEdition>();
            var groupedEditions = parts.GetAll<PersonLicenceEditionDO>("licenceEditions")
                .GroupBy(e => e.Content.LicencePartIndex);

            foreach(var editionsGroup in groupedEditions)
            {
                int licencePartIndex = editionsGroup.First().Content.LicencePartIndex.Value;
                foreach (var edition in editionsGroup.Select(e => e))
                {
                    foreach (var ratingEdition in edition.Content.PrintedRatingEditions)
                    {
                        printedRatingEditions.Add(this.Create(ratingEdition, edition.Part, licencePartIndex));
                    }
                }
            }

            return printedRatingEditions;
        }

        private GvaViewPrintedRatingEdition Create(PrintedRatingEditionDO edition, Part licenceEdition, int licencePartIndex)
        {
            GvaViewPrintedRatingEdition printedRatingEdition = new GvaViewPrintedRatingEdition();

            printedRatingEdition.LotId = licenceEdition.LotId;
            printedRatingEdition.LicencePartIndex = licencePartIndex;
            printedRatingEdition.LicenceEditionPartIndex = licenceEdition.Index;
            printedRatingEdition.RatingPartIndex = edition.RatingPartIndex;
            printedRatingEdition.RatingEditionPartIndex = edition.RatingEditionPartIndex;
            printedRatingEdition.PrintedFileId = edition.FileId;

            return printedRatingEdition;
        }
    }
}
