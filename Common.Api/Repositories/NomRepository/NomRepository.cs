using Common.Api.Models;
using Common.Data;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Common.Api.Repositories.NomRepository
{
    public class NomRepository : INomRepository
    {
        private IUnitOfWork unitOfWork;

        public NomRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<NomValueDO> GetNoms(string alias, string term)
        {
            var noms = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == alias)
                .NomValues
                .Select(nv => new NomValueDO { NomValueId = nv.NomValueId, Name = nv.Name });

            return this.GetByTerm(noms, term);
        }

        public IEnumerable<NomValueDO> GetNomsWithProperty(string alias, string propName, string propValue, string term)
        {
            var noms = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == alias)
                .NomValues
                .Select(nv => new { nv.NomValueId, nv.Name, Content = JObject.Parse(nv.TextContent) })
                .Where(nv => nv.Content.GetValue(propName).ToString() == propValue)
                .Select(nv => new NomValueDO { NomValueId = nv.NomValueId, Name = nv.Name });

            return this.GetByTerm(noms, term);
        }

        public IEnumerable<NomValueDO> GetNomsNotWithCode(string alias, string[] invalidCodes, string term)
        {
            var noms = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == alias)
                .NomValues
                .Where(nv => !invalidCodes.Contains(nv.Code))
                .Select(nv => new NomValueDO { NomValueId = nv.NomValueId, Name = nv.Name });

            return this.GetByTerm(noms, term);
        }

        public IEnumerable<NomValueDO> GetNomsWithCode(string alias, string[] validCodes, string term)
        {
            var noms = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == alias)
                .NomValues
                .Where(nv => validCodes.Contains(nv.Code))
                .Select(nv => new NomValueDO { NomValueId = nv.NomValueId, Name = nv.Name });

            return this.GetByTerm(noms, term);
        }

        public IEnumerable<NomValueDO> GetNomsForParent(string alias, int parentValueId, string term)
        {
            var noms = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == alias)
                .NomValues
                .Where(nv => nv.ParentValueId == parentValueId)
                .Select(nv => new NomValueDO { NomValueId = nv.NomValueId, Name = nv.Name });

            return this.GetByTerm(noms, term);
        }

        public IEnumerable<NomValueDO> GetNomsForGrandparent(string alias, int grandparentValueId, string parentAlias, string term)
        {
            var parents = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == parentAlias)
                .NomValues;

            var noms = this.unitOfWork.DbContext.Set<Nom>()
                .Include(n => n.NomValues)
                .SingleOrDefault(n => n.Alias == alias)
                .NomValues
                .Where(nv => nv.ParentValue.ParentValueId == grandparentValueId)
                .Select(nv => new NomValueDO { NomValueId = nv.NomValueId, Name = nv.Name });

            return this.GetByTerm(noms, term);
        }

        private IEnumerable<NomValueDO> GetByTerm(IEnumerable<NomValueDO> noms, string term)
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
