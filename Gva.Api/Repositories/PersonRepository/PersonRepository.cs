using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Common.Json;
using Common.Linq;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.Repositories.PersonRepository
{
    public class PersonRepository : IPersonRepository
    {
        private IUnitOfWork unitOfWork;
        private INomRepository nomRepository;

        public PersonRepository(
            IUnitOfWork unitOfWork,
            INomRepository nomRepository)
        {
            this.unitOfWork = unitOfWork;
            this.nomRepository = nomRepository;
        }

        public IEnumerable<GvaViewPerson> GetPersons(
            int? lin = null,
            string linType = null,
            string uin = null,
            int? caseTypeId = null,
            string names = null,
            string licences = null,
            string ratings = null,
            string organization = null,
            bool isInspector = false,
            bool isExaminer = false,
            bool exact = false,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<GvaViewPerson>();

            predicate = predicate
                .AndEquals(p => p.Lin, lin)
                .AndStringMatches(p => p.LinType.Name, linType, exact)
                .AndStringMatches(p => p.Uin, uin, exact)
                .AndStringContains(p => p.Licences, licences)
                .AndStringContains(p => p.Ratings, ratings)
                .AndStringMatches(p => p.Organization == null ? null : p.Organization.Name, organization, exact);

            if (!string.IsNullOrEmpty(names))
            {
                var namesToSearch = names.Split(new [] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var name in namesToSearch)
                {
                    predicate = predicate.And(p => p.Names.Contains(name));
                }
            }

            if (caseTypeId.HasValue)
            {
                predicate = predicate.AndCollectionContains(o => o.GvaLotCases.Select(c => c.GvaCaseTypeId), caseTypeId.Value);
            }

            if (isExaminer)
            {
                predicate = predicate.And(p => p.Examiner != null);
            }

            if (isInspector)
            {
                predicate = predicate.And(p => p.Inspector != null);
            }

            return this.unitOfWork.DbContext.Set<GvaViewPerson>()
                .Include(p => p.LinType)
                .Include(p => p.Organization)
                .Include(p => p.Employment)
                .Include(p => p.Inspector)
                .Include(p => p.Examiner)
                .Where(predicate)
                .OrderBy(p => p.Names)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public List<GvaViewPerson> GetAwExaminers(string names = null, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<GvaViewPerson>();

            if (!string.IsNullOrEmpty(names))
            {
                predicate = predicate.AndStringMatches(p => p.Names, names, false);
            }

            var persons = this.unitOfWork.DbContext.Set<GvaViewPerson>()
                .Include(p => p.Lot)
                .Where(predicate);

            return (from p in persons
                    join ct in this.unitOfWork.DbContext.Set<GvaLotCase>().Include(lc => lc.GvaCaseType).Where(lc => lc.GvaCaseType.Alias == "awExaminer")
                        on p.LotId equals ct.LotId
                    orderby p.Names
                    select p)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }

        public List<GvaViewPerson> GetStaffExaminers(string names = null, int offset = 0, int? limit = null)
        {
            var predicate = PredicateBuilder.True<GvaViewPerson>();

            if (!string.IsNullOrEmpty(names))
            {
                predicate = predicate.AndStringMatches(p => p.Names, names, false);
            }

            var persons = this.unitOfWork.DbContext.Set<GvaViewPerson>()
                .Include(p => p.Lot)
                .Where(predicate);

            return (from p in persons
                    join ct in this.unitOfWork.DbContext.Set<GvaLotCase>().Include(lc => lc.GvaCaseType).Where(lc => lc.GvaCaseType.Alias == "staffExaminer")
                        on p.LotId equals ct.LotId
                    orderby p.Names
                    select p)
                    .WithOffsetAndLimit(offset, limit)
                    .ToList();
        }

        public GvaViewPerson GetPerson(int personId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewPerson>()
                .Include(p => p.LinType)
                .Include(p => p.Organization)
                .Include(p => p.Employment)
                .Include(p => p.Inspector)
                .SingleOrDefault(p => p.LotId == personId);
        }

        public IEnumerable<ASExamVariant> GetQuestions(
            int asExamQuestionTypeId,
            string name = null,
            bool exact = false,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<ASExamVariant>();

            predicate = predicate
                .AndStringMatches(p => p.Name, name, exact)
                .And(e => e.ASExamQuestionTypeId == asExamQuestionTypeId);

            var asExamVariants = this.unitOfWork.DbContext.Set<ASExamVariant>()
                .Where(predicate);

            return asExamVariants
                .OrderBy(p => p.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public IEnumerable<GvaLicenceEdition> GetPrintableDocs(
            int? licenceType = null,
            int? licenceAction = null,
            int? lin = null,
            string uin = null,
            string names = null,
            bool exact = false)
        {
            var predicate = PredicateBuilder.True<GvaLicenceEdition>();

            predicate = predicate
                .And(e => e.StampNumber == null)
                .And(e => e.IsLastEdition == true)
                .AndEquals(e => e.Person.Lin, lin)
                .AndStringMatches(e => e.Person.Uin, uin, exact)
                .AndStringMatches(e => e.Person.Names, names, exact);

            if (licenceType.HasValue)
            {
                predicate = predicate.AndEquals(e => e.LicenceTypeId, licenceType);
            }

            if (licenceAction.HasValue)
            {
                predicate = predicate.AndEquals(e => e.LicenceActionId, licenceAction);
            }

            return this.unitOfWork.DbContext.Set<GvaLicenceEdition>()
                .Include(e => e.LicenceAction)
                .Include(e => e.Person)
                .Include(e => e.Person.LinType)
                .Include(e => e.Application)
                .Include(e => e.Application.ApplicationType)
                .Include(e => e.Application.Part)
                .Where(predicate)
                .OrderByDescending(i => i.DateValidFrom)
                .ToList();
        }

        public int? GetNextLin(int linTypeId)
        {
            var linType = this.nomRepository.GetNomValue("linTypes", linTypeId);
            if (linType.Code == "noLin")
            {
                return null;
            }
            else
            {
                var lins = this.unitOfWork.DbContext.Set<GvaViewPerson>()
                .Include(p => p.LinType)
                .Where(p => p.LinTypeId == linTypeId);

                int? lastLin = lins.Max(p => (int?)p.Lin);

                if (!lastLin.HasValue)
                {
                    lastLin = linType.TextContent.Get<int>("initialLinVal");
                }

                return lastLin.Value + 1;
            }
        }

        public bool IsUniqueUin(string uin, int? personId = null)
        {
            if (personId.HasValue)
            {
                return !this.unitOfWork.DbContext.Set<GvaViewPerson>()
                    .Where(p => p.Uin == uin && p.LotId != personId).Any();
            }
            else
            {
                return !this.unitOfWork.DbContext.Set<GvaViewPerson>()
                    .Where(p => p.Uin == uin).Any();
            }
        }

        public List<GvaLicenceEdition> GetStampedDocuments(
            string uin,
            string names,
            string stampNumber,
            int? lin = null,
            int? licenceNumber = null)
        {
            var predicate = PredicateBuilder.True<GvaLicenceEdition>()
                .And(e => e.StampNumber != null && e.GvaStageId.HasValue)
                .And(e => e.GvaStageId < GvaConstants.IsDoneApplication);

            if (lin.HasValue) 
            {
                predicate = predicate.And(e => e.Person.Lin == lin);
            }

            if (!string.IsNullOrEmpty(uin))
            {
                predicate = predicate.And(e => e.Person.Uin.Contains(uin));
            }

            if (!string.IsNullOrEmpty(names)) 
            {
                predicate = predicate.And(e => e.Person.Names.Contains(names));
            }


            if (licenceNumber.HasValue)
            {
                predicate = predicate.And(e => e.LicenceNumber != null && e.LicenceNumber.Value == licenceNumber);
            }

            if (!string.IsNullOrEmpty(stampNumber))
            {
                predicate = predicate.And(e => e.StampNumber.Contains(stampNumber));
            }

            return this.unitOfWork.DbContext.Set<GvaLicenceEdition>()
                .Include(e => e.LicenceAction)
                .Include(e => e.Person)
                .Include(e => e.Person.LinType)
                .Include(e => e.Application)
                .Include(e => e.Application.ApplicationType)
                .Include(e => e.Application.Part)
                .Where(predicate)
                .OrderByDescending(i => i.DateValidFrom)
                .ToList();
        }

        public IEnumerable<GvaLicenceEdition> GetLicences(int lotId, int? caseTypeId)
        {
            var predicate = PredicateBuilder.True<GvaLicenceEdition>()
                .And(e => e.LotId == lotId)
                .And(e => e.IsLastEdition == true);

            if (caseTypeId != null)
            {
                predicate = predicate.And(f => f.LotFile.GvaCaseTypeId == caseTypeId.Value);
            }

            return this.unitOfWork.DbContext.Set<GvaLicenceEdition>()
                .Include(e => e.LicenceType)
                .Include(e => e.LotFile)
                .Include(e => e.LotFile.GvaCaseType)
                .Include(e => e.LotFile.GvaFile)
                .Include(e => e.Application)
                .Include(e => e.Application.Part)
                .Include(e => e.Application.ApplicationType)
                .Include(e => e.LicenceAction)
                .Where(predicate)
                .OrderByDescending(i => i.DateValidFrom)
                .ToList();
        }

        public string GetLastLicenceNumber(int lotId, string licenceTypeCode)
        {
            string licenceNumber = null;
            var licenceNumbers = this.unitOfWork.DbContext.Set<GvaViewPersonLicence>()
                .Include(e => e.LicenceType)
                .Where(e => e.LicenceType.Code == licenceTypeCode)
                .Select(e => e.LicenceNumber);

            if (licenceNumbers.Any())
            {
                licenceNumber = licenceNumbers.Max().ToString();
            }

            return licenceNumber;
        }

        public int GetLastLicenceEditionIndex(int lotId, int licencePartIndex)
        {
            return this.unitOfWork.DbContext.Set<GvaViewPersonLicenceEdition>()
                .Single(e => e.LotId == lotId && e.LicencePartIndex == licencePartIndex && e.IsLastEdition)
                .PartIndex;
        }

        public IEnumerable<GvaViewPersonRating> GetRatings(int lotId, int? caseTypeId)
        {
            var ratings = this.unitOfWork.DbContext.Set<GvaViewPersonRating>()
                .Include(e => e.Person)
                .Include(e => e.Part)
                .Include(e => e.RatingLevel)
                .Include(e => e.RatingClass)
                .Include(e => e.AircraftTypeGroup)
                .Include(e => e.AircraftTypeCategory)
                .Include(e => e.Authorization)
                .Include(e => e.LocationIndicator)
                .Include(e => e.Editions)
                .Where(e => e.LotId == lotId)
                .ToList();

            if (caseTypeId.HasValue)
            {
                var lotPartsForCase = this.unitOfWork.DbContext.Set<GvaLotFile>()
                    .Where(f => f.LotPart.LotId == lotId && f.GvaCaseTypeId == caseTypeId.Value)
                    .Select(f => f.LotPartId)
                    .ToList();

                return ratings.Where(a => lotPartsForCase.Contains(a.PartId)).ToList();
            }
            else {
                return ratings;
            }
        }

        public int GetLastRatingEditionIndex(int lotId, int ratingPartIndex)
        {
            return this.unitOfWork.DbContext.Set<GvaViewPersonRating>()
                .Include(r => r.Editions)
                .Single(e => e.LotId == lotId && e.PartIndex == ratingPartIndex)
                .Editions.OrderBy(e => e.Index)
                .Last()
                .PartIndex;
        }

        public GvaViewPersonDocument IsUniqueDocData(
            string documentNumber = null,
            int? documentPersonNumber = null,
            int? partIndex = null,
            int? typeId = null,
            int? roleId = null,
            string publisher = null,
            DateTime? dateValidFrom = null)
        {
            DateTime? date = dateValidFrom.HasValue ? dateValidFrom.Value.Date : (DateTime?)null;

            var predicate = PredicateBuilder.True<GvaViewPersonDocument>()
                .And(d => (d.DocumentNumber == null && documentNumber == null) || (d.DocumentNumber == documentNumber))
                .And(d => (d.Publisher == null && publisher == null) || (d.Publisher == publisher))
                .And(d => (!d.DocumentPersonNumber.HasValue && !documentPersonNumber.HasValue) || (d.DocumentPersonNumber.Value == documentPersonNumber.Value))
                .And(d => (!d.TypeId.HasValue && !typeId.HasValue) || (d.TypeId.Value == typeId.Value))
                .And(d => (!d.RoleId.HasValue && !roleId.HasValue) || (d.RoleId.Value == roleId.Value))
                .And(d => (!d.DateValidFrom.HasValue && !date.HasValue) || (d.DateValidFrom.Value == date));

            if(partIndex.HasValue)
            {
                predicate = predicate.And(d => d.PartIndex != partIndex);
            }

            return this.unitOfWork.DbContext.Set<GvaViewPersonDocument>()
                .Where(predicate)
                .OrderByDescending(r => r.DocumentPersonNumber)
                .SingleOrDefault();
        }

        public bool IsUniqueLicenceNumber(string licenceTypeCode, int? licenceNumber)
        {
            var licences = this.unitOfWork.DbContext.Set<GvaViewPersonLicence>()
                .Include(e => e.LicenceType)
                .Where(e => e.LicenceType.Code == licenceTypeCode && e.LicenceNumber == licenceNumber);

            return !licences.Any();
        }

        public bool IsFclLicence(int licenceTypeId)
        {
            NomValue licenceType = this.nomRepository.GetNomValue("licenceTypes", licenceTypeId);

            return licenceType.Code.Contains("FCL") || licenceType.Code == "BG CCA";
        }

        public List<GvaViewPersonCheck> GetChecksForReport(List<int> checks)
        {
            if (checks.Count() > 0)
            {
                return this.unitOfWork.DbContext.Set<GvaViewPersonCheck>()
                .Include(c => c.Authorization)
                .Include(c => c.DocumentRole)
                .Include(c => c.DocumentType)
                .Include(c => c.Valid)
                .Include(c => c.RatingClass)
                .Include(c => c.PersonCheckRatingValue)
                .Include(c => c.LicenceType)
                .Include(c => c.Person)
                .Where(c => checks.Contains(c.PartId))
                .ToList();
            }
            else
            {
                return new List<GvaViewPersonCheck>();
            }
        }
    }
}