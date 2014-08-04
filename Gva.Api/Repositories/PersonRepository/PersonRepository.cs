﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public PersonRepository(IUnitOfWork unitOfWork, INomRepository nomRepository)
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
    }
}
