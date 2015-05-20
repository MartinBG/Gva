using System;

namespace Common.DomainValidation
{
    public interface IDomainValidator
    {
        void AddErrorMessage(Enum errorCode);

        void AddErrorMessage(Enum errorCode, params object[] errorParameters);

        void Validate();
    }
}
