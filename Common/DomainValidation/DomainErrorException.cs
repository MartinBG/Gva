using System;
using System.Collections.Generic;

namespace Common.DomainValidation
{
    public class DomainErrorException : Exception
    {
        ICollection<DomainErrorMessage> errorMessages = new List<DomainErrorMessage>();
        public ICollection<DomainErrorMessage> ErrorMessages
        {
            get
            {
                return errorMessages;
            }
        }

        public DomainErrorException(ICollection<DomainErrorMessage> errorMessages)
        {
            this.errorMessages = errorMessages;
        }        
    }
}
