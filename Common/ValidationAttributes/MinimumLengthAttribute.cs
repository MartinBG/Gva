using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Common.ValidationAttributes
{
    public class MinimumLengthAttribute : ValidationAttribute
    {
        private int minLength;

        public MinimumLengthAttribute(int minLength)
        {
            this.minLength = minLength;
        }

        public override bool IsValid(object value)
        {
            var collection = value as IEnumerable<object>;

            return collection.Count() >= this.minLength;
        }
    }
}
