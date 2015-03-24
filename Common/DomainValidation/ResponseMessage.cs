using System.Collections.Generic;

namespace Common.DomainValidation
{
    public class ResponseMessage
    {
        public string Status { get; set; }

        public ICollection<DomainErrorMessage> Messages { get; set; }
    }
}
