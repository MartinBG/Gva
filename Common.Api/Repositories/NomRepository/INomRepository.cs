using Common.Api.Models;
using System.Collections.Generic;

namespace Common.Api.Repositories.NomRepository
{
    public interface INomRepository
    {
        IEnumerable<NomValueDO> GetNoms(string alias, string term);

        IEnumerable<NomValueDO> GetNomsWithProperty(string alias, string propName, string propValue, string term);

        IEnumerable<NomValueDO> GetNomsWithStaffCode(string alias, string code, string term);

        IEnumerable<NomValueDO> GetNomsNotWithCode(string alias, string[] invalidCodes, string term);

        IEnumerable<NomValueDO> GetNomsWithCode(string alias, string[] validCodes, string term);

        IEnumerable<NomValueDO> GetNomsForParent(string alias, int parentValueId, string term);

        IEnumerable<NomValueDO> GetNomsForGrandparent(string alias, int grandparentValueId, string parentAlias, string term);
    }
}
