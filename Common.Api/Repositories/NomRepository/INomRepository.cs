using System.Collections.Generic;
using Common.Api.Models;

namespace Common.Api.Repositories.NomRepository
{
    public interface INomRepository
    {
        Nom GetNom(string alias);

        NomValue GetNomValue(string alias, int id);

        NomValue GetNomValue(string alias, string valueAlias);

        IEnumerable<NomValue> GetNomValues(string alias, int[] ids);

        IEnumerable<NomValue> GetNomValues(string alias, string[] valueAliases);

        IEnumerable<NomValue> GetNomValues(string alias, string term = null, int? parentValueId = null, int? grandParentValueId = null, int offset = 0, int? limit = null);

        IEnumerable<NomValue> GetNomValues(string alias);
    }
}
