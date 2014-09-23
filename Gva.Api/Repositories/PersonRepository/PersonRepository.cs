using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Common.Json;
using Common.Linq;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Repositories.PersonRepository
{
    public class PersonRepository : IPersonRepository
    {
        private IUnitOfWork unitOfWork;
        private INomRepository nomRepository;
        private ILotRepository lotRepository;

        public PersonRepository(IUnitOfWork unitOfWork, INomRepository nomRepository, ILotRepository lotRepository)
        {
            this.unitOfWork = unitOfWork;
            this.nomRepository = nomRepository;
            this.lotRepository = lotRepository;
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

        public IEnumerable<GvaViewPersonLicenceEdition> GetPrintableDocs(
            int? licenceType = null,
            int? licenceAction = null,
            int? lin = null,
            string uin = null,
            string names = null,
            bool exact = false)
        {
            var predicate = PredicateBuilder.True<GvaViewPersonLicenceEdition>();

            predicate = predicate
                .And(e => string.IsNullOrEmpty(e.StampNumber))
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

            return this.unitOfWork.DbContext.Set<GvaViewPersonLicenceEdition>()
                .Include(e => e.Person)
                .Include(p => p.Person.LinType)
                .Include(p => p.Person.Organization)
                .Include(p => p.Person.Employment)
                .Include(p => p.Person.Inspector)
                .Include(e => e.Part)
                .Include(e => e.LicenceAction)
                .Where(predicate)
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

        public List<GvaViewPersonLicenceEditionDO> GetStampedDocuments()
        {
            return (from edition in this.unitOfWork.DbContext.Set<GvaViewPersonLicenceEdition>()
                        .Include(e => e.Person)
                        .Include(e => e.Part)
                        .Include(e => e.Application)
                        .Include(e => e.LicenceAction)
                        .Include(p => p.Person.LinType)
                        .Include(p => p.Person.Organization)
                        .Include(p => p.Person.Employment)
                        .Include(p => p.Person.Inspector)
                        .ToList()
                    where edition.StampNumber != null && edition.Application != null &&
                        this.unitOfWork.DbContext.Set<GvaApplicationStage>()
                        .Any(a => a.GvaApplicationId == edition.GvaApplicationId)
                    select edition)
                    .Select(edition =>
                    {
                        List<int> stages = this.unitOfWork.DbContext.Set<GvaApplicationStage>()
                            .Where(s => s.GvaApplicationId == edition.GvaApplicationId)
                            .Select(s => s.GvaStageId)
                            .ToList();

                        return new GvaViewPersonLicenceEditionDO(edition, stages);

                    })
                    .ToList();
        }

        public IEnumerable<GvaViewPersonLicenceEdition> GetLicences(int lotId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewPersonLicenceEdition>()
                .Include(e => e.Person)
                .Include(p => p.Person.LinType)
                .Include(p => p.Person.Organization)
                .Include(p => p.Person.Employment)
                .Include(p => p.Person.Inspector)
                .Include(e => e.Part)
                .Include(e => e.LicenceAction)
                .Include(e => e.LicenceType)
                .Include(e => e.Application)
                .Where(e => e.LotId == lotId && e.IsLastEdition == true)
                .ToList();
        }

        public string GetLastLicenceNumber(int lotId, string licenceTypeCode)
        {
            string licenceNumber = null;
            var licenceNumbers = this.unitOfWork.DbContext.Set<GvaViewPersonLicenceEdition>()
                .Include(e => e.LicenceType)
                .Where(e => e.LicenceType.Code == licenceTypeCode)
                .Select(e => e.LicenceNumber);

            if (licenceNumbers.Any())
            {
                licenceNumber = licenceNumbers.Max().ToString();
            }

            return licenceNumber;
        }

        public IEnumerable<GvaViewPersonRating> GetRatings(int lotId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewPersonRating>()
                .Include(e => e.Person)
                .Include(e => e.Part)
                .Include(e => e.RatingType)
                .Include(e => e.RatingLevel)
                .Include(e => e.RatingClass)
                .Include(e => e.AircraftTypeGroup)
                .Include(e => e.Authorization)
                .Where(e => e.LotId == lotId)
                .ToList();
        }
    }
}