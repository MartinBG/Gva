using System.Collections.Generic;
using Common.Api.Models;

namespace Common.Api.Repositories.NomRepository
{
    public interface INomRepository
    {
        Nom GetNom(string alias);

        NomValue GetNomValue(int id);

        NomValue GetNomValue(string alias, int id);

        NomValue GetNomValue(string alias, string valueAlias);

        IEnumerable<NomValue> GetNomValues(string alias, int[] ids);

        IEnumerable<NomValue> GetNomValues(string alias, string[] valueAliases);

        IEnumerable<NomValue> GetNomValues(string alias, string term = null, int? parentValueId = null, int offset = 0, int? limit = null, bool onlyActive = true);

        IEnumerable<NomValue> GetNomValues(string alias);

        IEnumerable<NomValue> GetNomValues(int nomId, bool onlyActive = true);

        IEnumerable<NomValue> GetNomValues(
            string alias,
            string prop,
            string propValue,
            string term = null,
            bool onlyActive = true,
            int offset = 0,
            int? limit = null);

        IEnumerable<NomValue> GetNomValues(
            string alias,
            string parentAlias,
            string parentProp,
            string parentPropValue,
            string term = null,
            bool onlyActive = true,
            int offset = 0,
            int? limit = null);
    }
}
