using System;
using System.Collections.Generic;

namespace Common.DomainValidation
{
    public class DomainValidator : IDomainValidator
    {
        ICollection<DomainErrorMessage> errorMessages = new List<DomainErrorMessage>();

        public DomainValidator()
        {
        }

        public void AddErrorMessage(Enum errorCode)
        {
            errorMessages.Add(new DomainErrorMessage(errorCode));
        }

        public void AddErrorMessage(Enum errorCode, params object[] errorParameters)
        {
            throw new NotImplementedException();
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
