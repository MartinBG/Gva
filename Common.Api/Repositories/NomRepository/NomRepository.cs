using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Api.Models;
using Common.Data;
using Newtonsoft.Json.Linq;
using Common.Linq;

namespace Common.Api.Repositories.NomRepository
{
    public class NomRepository : INomRepository
    {
        private IUnitOfWork unitOfWork;

        public NomRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Nom GetNom(string alias)
        {
            return this.unitOfWork.DbContext.Set<Nom>()
              .Include(n => n.NomValues)
              .SingleOrDefault(n => n.Alias == alias);
        }

        public NomValue GetNomValue(string alias, int id)
        {
            return this.unitOfWork.DbContext.Set<NomValue>().Where(nv => nv.Nom.Alias == alias && nv.NomValueId == id).SingleOrDefault();
        }

        public NomValue GetNomValue(string alias, string valueAlias)
        {
            return this.unitOfWork.DbContext.Set<NomValue>().Where(nv => nv.Nom.Alias == alias && nv.Alias == valueAlias).SingleOrDefault();
        }

        public IEnumerable<NomValue> GetNomValues(string alias, int[] ids)
        {
            return this.unitOfWork.DbContext.Set<NomValue>()
                .Where(nv => nv.Nom.Alias == alias && ids.Contains(nv.NomValueId))
                .ToList();
        }

        public IEnumerable<NomValue> GetNomValues(string alias, string[] valueAliases)
        {
            return this.unitOfWork.DbContext.Set<NomValue>()
                .Where(nv => nv.Nom.Alias == alias && valueAliases.Contains(nv.Alias))
                .ToList();
        }

        public IEnumerable<NomValue> GetNomValues(string alias, string term = null, int? parentValueId = null, int offset = 0, int? limit = null)
        {
            var predicate =
                PredicateBuilder.True<NomValue>()
                .And(nv => nv.Nom.Alias == alias)
                .AndEquals(nv => nv.ParentValueId.Value, parentValueId)
                .AndStringContains(nv => nv.Name, term);

            return this.unitOfWork.DbContext.Set<NomValue>()
                .Where(predicate)
                .OrderBy(nv => nv.Order)
                .ThenBy(nv => nv.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public IEnumerable<NomValue> GetNomValues(string alias)
        {
            return this.unitOfWork.DbContext.Set<NomValue>()
                .Where(nv => nv.Nom.Alias == alias)
                .ToList();
        }

        public IEnumerable<NomValue> GetNomValues(
            string alias,
            string parentAlias,
            string prop,
            string propValue,
            string term = null,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<NomValue>();

            if (!string.IsNullOrWhiteSpace(term))
            {
                predicate = predicate.AndStringContains(nv => nv.Name, term);
            }

            return this.unitOfWork.DbContext.GetNomValuesByTextContentProperty(parentAlias, prop, propValue)
                .Join(
                    this.unitOfWork.DbContext.Set<NomValue>().Where(nv => nv.Nom.Alias == alias),
                    (p) => p.NomValueId,
                    (c) => c.ParentValueId,
                    (p, c) => c)
                .Where(predicate)
                .OrderBy(nv => nv.Order)
                .ThenBy(nv => nv.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }
    }
}
