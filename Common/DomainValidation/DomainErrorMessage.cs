using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DomainValidation
{
    public class DomainErrorMessage
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Enum DomainErrorCode { get; set; }

        public string DomainErrorText { get; set; }

        // could be made collection of key, value in future
        public object[] ErrorParameters { get; set; }

        public DomainErrorMessage(Enum domainErrorCode)
        {
            DomainErrorCode = domainErrorCode;
        }

        public DomainErrorMessage(Enum domainErrorCode, string domainErrorText)
        {
            DomainErrorCode = domainErrorCode;
            DomainErrorText = domainErrorText;
        }

        public DomainErrorMessage(Enum domainErrorCode, string domainErrorText, params object[] errorParameters)
        {
            DomainErrorCode = domainErrorCode;
            DomainErrorText = domainErrorText;
            ErrorParameters = errorParameters;
        }
    }
}
