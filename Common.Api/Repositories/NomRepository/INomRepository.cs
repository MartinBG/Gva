using System.Collections.Generic;
using Common.Api.Models;

namespace Common.Api.Repositories.NomRepository
{
    public interface INomRepository
    {
        NomValue GetNom(string alias, int id);

        NomValue GetNom(string alias, string valueAlias);

        IEnumerable<NomValue> GetNoms(string alias, int[] ids);

        IEnumerable<NomValue> GetNoms(string alias, string term = null, int? parentValueId = null, int? grandParentValueId = null, int offset = 0, int? limit = null);
    }
}
