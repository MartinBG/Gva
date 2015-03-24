using Docs.Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.Infrastructure
{
    public static class DomainErrorResource
    {
        // this could be loaded from Db in future if there is such requirement
        private static Dictionary<Enum, string> dictionary = new Dictionary<Enum, string> {            
                    {DomainErrorCode.Entity_NotFound, "Entity of type {0} with Id {1} not found."},
                    {DomainErrorCode.Unit_NotFound, "Unit with Id not found."},
                    {DomainErrorCode.Unit_NotFoundOrNotActive,   "Er2"},
                    {DomainErrorCode.Unit_ErrorWithParameters, "Test er: {0}, {1}"}
                };

        public static string GetResourceTextByCode(Enum domainErrorCode)
        {
            string text;
            if (!dictionary.TryGetValue(domainErrorCode, out text))
            {
                // fallback if resource is not entered
                text = DomainErrorCode.Unit_UnitTypeIsNotUser.ToString();
            }

            return text;
        }

        // Warning - number of parameters must be the same as parameters in Formated string, else error will be thrown.
        public static string GetResourceTextWithParsedParamsByCode(Enum domainErrorCode, params object[] errorParameters)
        {
            string text = GetResourceTextByCode(domainErrorCode);
            if (errorParameters.Length > 0)
            {
                text = string.Format(text, errorParameters);
            }

            return text;
        }
    }
}
