﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.PersonRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class PersonLicenceEditionProjection : Projection<GvaViewPersonLicenceEdition>
    {
        private INomRepository nomRepository;
        private IPersonRepository personRepository;

        public PersonLicenceEditionProjection(
            IUnitOfWork unitOfWork,
            INomRepository nomRepository,
            IPersonRepository personRepository)
            : base(unitOfWork, "Person")
        {
            this.nomRepository = nomRepository;
            this.personRepository = personRepository;
        }

        public override IEnumerable<GvaViewPersonLicenceEdition> Execute(PartCollection parts)
        {
            List<GvaViewPersonLicenceEdition> editions = new List<GvaViewPersonLicenceEdition>();
            var groupedEditions = parts.GetAll<PersonLicenceEditionDO>("licenceEditions")
                .GroupBy(e => e.Content.LicencePartIndex)
                .OrderBy(g => g.Key);

            foreach(var editionsGroup in groupedEditions)
            {
                var orderedEditions = editionsGroup.OrderByDescending(e => e.Content.Index);

                DateTime firstDocDateValidFrom = orderedEditions.Last().Content.DocumentDateValidFrom.Value;
                int maxIndex = orderedEditions.First().Content.Index;

                foreach (var edition in editionsGroup)
                {
                    editions.Add(this.Create(edition, firstDocDateValidFrom, edition.Content.Index == maxIndex));
                }
            }

            return editions;
        }

        private GvaViewPersonLicenceEdition Create(PartVersion<PersonLicenceEditionDO> edition, DateTime firstDocDateValidFrom, bool isLastEdition)
        {
            GvaViewPersonLicenceEdition licenceEdition = new GvaViewPersonLicenceEdition();

            licenceEdition.LotId = edition.Part.Lot.LotId;
            licenceEdition.PartId = edition.Part.PartId;
            licenceEdition.PartIndex = edition.Part.Index;
            licenceEdition.LicencePartIndex = edition.Content.LicencePartIndex.Value;
            licenceEdition.Index = edition.Content.Index;
            licenceEdition.StampNumber = edition.Content.StampNumber != null ? edition.Content.StampNumber.Replace(",", GvaConstants.ConcatenatingExp).Replace(";", GvaConstants.ConcatenatingExp) : null;
            licenceEdition.PaperId = edition.Content.PaperId.HasValue ? edition.Content.PaperId.Value : (!string.IsNullOrEmpty(edition.Content.StampNumber) ? 1 : (int?)null);
            licenceEdition.DateValidFrom = edition.Content.DocumentDateValidFrom.Value;
            licenceEdition.DateValidTo = edition.Content.DocumentDateValidTo;

            if(edition.Content.LicenceActionId.HasValue)
            {
                licenceEdition.LicenceActionId = edition.Content.LicenceActionId.Value;
            }

            licenceEdition.Notes = edition.Content.Notes;
            licenceEdition.FirstDocDateValidFrom = firstDocDateValidFrom;
            licenceEdition.IsLastEdition = isLastEdition;
            licenceEdition.PrintedFileId = edition.Content.PrintedFileId;
            licenceEdition.HasNoNumber = edition.Content.HasNoNumber;

            if(edition.Content.IsOfficiallyReissued == true)
            {
                licenceEdition.OfficiallyReissuedStageId = edition.Content.ОfficiallyReissuedStageId ?? GvaConstants.OfficiallyReissuedLicenceFirstStage;
            }

            if (edition.Content.InspectorId.HasValue)
            {
                var person = this.personRepository.GetPerson(edition.Content.InspectorId.Value);

                licenceEdition.Inspector = string.Format("{0} {1}", person.Lin, person.Names);
            }
            if (edition.Content.Limitations.Count > 0)
            {
                licenceEdition.Limitations = string.Join(GvaConstants.ConcatenatingExp, edition.Content.Limitations.Select(l => nomRepository.GetNomValue(l).Name));
            }

            return licenceEdition;
        }
    }
}
