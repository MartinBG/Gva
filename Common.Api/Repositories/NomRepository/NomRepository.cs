using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Api.Models;
using Common.Data;
using Newtonsoft.Json.Linq;

namespace Common.Api.Repositories.NomRepository
{
    public class NomRepository : INomRepository
    {
        private IUnitOfWork unitOfWork;

        public NomRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<NomValue> GetNoms(string alias, string term)
        {
            var noms = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == alias)
                .NomValues;

            return this.GetByTerm(noms, term);
        }

        public IEnumerable<NomValue> GetNomsWithProperty(string alias, string propName, string propValue, string term)
        {
            var noms = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == alias)
                .NomValues
                .Where(nv => JObject.Parse(nv.TextContent).Value<string>(propName) == propValue);

            return this.GetByTerm(noms, term);
        }

        public IEnumerable<NomValue> GetNomsContainingProperty(string alias, string propName, string propValue, string term)
        {
            var noms = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == alias)
                .NomValues
                .Where(nv => (dynamic)JObject.Parse(nv.TextContent).Value<JArray>(propName).Values<string>().Contains(propValue));

            return this.GetByTerm(noms, term);
        }

        public IEnumerable<NomValue> GetDocumentRoles(string categoryAlias, string[] staffCodes, string term)
        {
            JToken staffAlias;

            var noms = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == "documentRoles")
                .NomValues
                .Where(
                    nv => JObject.Parse(nv.TextContent).Value<string>("categoryAlias") == categoryAlias &&
                    (!JObject.Parse(nv.TextContent).TryGetValue("staffAlias", out staffAlias) || staffCodes.Contains(staffAlias.ToString())));

            return this.GetByTerm(noms, term);
        }

        public IEnumerable<NomValue> GetDocumentTypes(bool isIdDocument, string[] staffCodes, string term)
        {
            JToken staffAlias;

            var noms = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == "documentTypes")
                .NomValues
                .Where(
                    nv => JObject.Parse(nv.TextContent).Value<bool>("isIdDocument") == isIdDocument &&
                    (!JObject.Parse(nv.TextContent).TryGetValue("staffAlias", out staffAlias) || staffCodes.Contains(staffAlias.ToString())));

            return this.GetByTerm(noms, term);
        }

        public IEnumerable<NomValue> GetNomsForParent(string alias, int parentValueId, string term)
        {
            var noms = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == alias)
                .NomValues
                .Where(nv => nv.ParentValueId == parentValueId);

            return this.GetByTerm(noms, term);
        }

        public IEnumerable<NomValue> GetNomsForGrandparent(string alias, int grandparentValueId, string parentAlias, string term)
        {
            this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == parentAlias);

            var noms = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == alias)
                .NomValues
                .Where(nv => nv.ParentValue.ParentValueId == grandparentValueId);

            return this.GetByTerm(noms, term);
        }

        private IEnumerable<NomValue> GetByTerm(IEnumerable<NomValue> noms, string term)
        {
            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.ToLower();
                noms = noms.Where(nv => nv.Name.ToLower().Contains(term));
            }

            return noms;
        }
    }
}
