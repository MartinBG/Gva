using Common.DomainValidation;
using System;
using System.Collections.Generic;

namespace Docs.Api.Infrastructure
{
    public class DomainValidator : IDomainValidator
    {
        ICollection<DomainErrorMessage> errorMessages = new List<DomainErrorMessage>();

        public DomainValidator()
        {
        }

        public void AddErrorMessage(Enum errorCode)
        {
            var message = new DomainErrorMessage(errorCode, DomainErrorResource.GetResourceTextByCode(errorCode));
            errorMessages.Add(message);
        }

        public void AddErrorMessage(Enum errorCode, params object[] errorParameters)
        {
            var message = new DomainErrorMessage(errorCode, DomainErrorResource.GetResourceTextWithParsedParamsByCode(errorCode, errorParameters));
            errorMessages.Add(message);
        }

        public void Validate()
        {
            if (errorMessages.Count > 0)
            {
                throw new DomainErrorException(errorMessages);
            }
        }
    }
}
