using System.Collections.Generic;
using Common.Api.Models;
using Gva.MigrationTool.Nomenclatures;

namespace Gva.MigrationTool.Sets
{
    public static class PersonUtils
    {
        public static NomValue getPersonCaseTypeByStaffTypeCode(Dictionary<string, Dictionary<string, NomValue>> noms, string code)
        {
            var caseTypeAliases = new Dictionary<string, string>()
            {
                { "F", "flightCrew" },
                { "T", "ovd"},
                { "G", "to_vs"},
                { "M", "to_suvd"}
            };

            return noms["personCaseTypes"].ByAlias(caseTypeAliases[code]);
        }
    }
}
