﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Common.Json;
using Common.Linq;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Repositories.PersonRepository
{
    public class PersonRepository : IPersonRepository
    {
        private IUnitOfWork unitOfWork;
        private INomRepository nomRepository;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;

        public PersonRepository(
            IUnitOfWork unitOfWork,
            INomRepository nomRepository,
            ILotRepository lotRepository,
            IFileRepository fileRepository)
        {
            this.unitOfWork = unitOfWork;
            this.nomRepository = nomRepository;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
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
                .AndStringMatches(p => p.Names, names, exact)
                .AndStringContains(p => p.Licences, licences)
                .AndStringContains(p => p.Ratings, ratings)
                .AndStringMatches(p => p.Organization == null ? null : p.Organization.Name, organization, exact);


            if (caseTypeId.HasValue)
            {
                predicate = predicate.AndCollectionContains(o => o.GvaLotCases.Select(c => c.GvaCaseTypeId), caseTypeId.Value);
            }

            if (isExaminer)
            {
                predicate = predicate.And(p => p.OrganizationExaminers.Count != 0);
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
                .Where(predicate)
                .OrderBy(p => p.Names)
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

        public int GetNextLin(int linTypeId)
        {
            int? lastLin = this.unitOfWork.DbContext.Set<GvaViewPerson>()
                .Include(p => p.LinType)
                .Where(p => p.LinTypeId == linTypeId)
                .Max(p => (int?)p.Lin);

            if (!lastLin.HasValue)
            {
                lastLin = this.nomRepository.GetNomValue("linTypes", linTypeId).TextContent.Get<int>("initialLinVal");
            }

            return lastLin.Value + 1;
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

        public List<GvaLicenceEdition> GetStampedDocuments()
        {
            return this.unitOfWork.DbContext.Set<GvaLicenceEdition>()
                .Include(e => e.LicenceAction)
                .Include(e => e.Person)
                .Include(e => e.Person.LinType)
                .Include(e => e.Application)
                .Include(e => e.Application.ApplicationType)
                .Include(e => e.Application.Part)
                .Where(e => e.StampNumber != null && e.GvaStageId.HasValue && e.GvaStageId < GvaConstants.IsDoneApplication)
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
            IEnumerable<GvaViewPersonRating> ratings = this.unitOfWork.DbContext.Set<GvaViewPersonRating>()
                .Include(e => e.Person)
                .Include(e => e.Part)
                .Include(e => e.RatingType)
                .Include(e => e.RatingLevel)
                .Include(e => e.RatingClass)
                .Include(e => e.AircraftTypeGroup)
                .Include(e => e.Authorization)
                .Include(e => e.LocationIndicator)
                .Include(e => e.Editions)
                .Where(e => e.LotId == lotId);

            if (caseTypeId.HasValue)
            {
                return (from r in ratings
                        join f in this.unitOfWork.DbContext.Set<GvaLotFile>().Include(f => f.GvaCaseType) on r.PartId equals f.LotPartId
                        where f.GvaCaseTypeId == caseTypeId.Value
                        select r);
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
    }
}