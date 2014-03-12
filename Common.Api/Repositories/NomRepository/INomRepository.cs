using System.Collections.Generic;
using Common.Api.Models;

namespace Common.Api.Repositories.NomRepository
{
    public interface INomRepository
    {
        IEnumerable<NomValue> GetNoms(string alias, string term);

        IEnumerable<NomValue> GetNomsWithProperty(string alias, string propName, string propValue, string term);

        IEnumerable<NomValue> GetNomsContainingProperty(string alias, string propName, string propValue, string term);

        IEnumerable<NomValue> GetDocumentRoles(string categoryAlias, string[] staffCodes, string term);

        IEnumerable<NomValue> GetDocumentTypes(bool isIdDocument, string[] staffCodes, string term);

        IEnumerable<NomValue> GetNomsForParent(string alias, int parentValueId, string term);

        IEnumerable<NomValue> GetNomsForGrandparent(string alias, int grandparentValueId, string parentAlias, string term);
    }
}
