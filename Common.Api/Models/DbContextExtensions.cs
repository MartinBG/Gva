using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeFirstStoreFunctions;

namespace Common.Api.Models
{
    public static class DbContextExtensions
    {
        [DbFunctionDetailsAttribute(StoreFunctionName = "ufnGetNomValuesByTextContentProperty")]
        [DbFunction("dummy", "edmGetNomValuesByTextContentProperty")]
        public static IQueryable<NomValue> GetNomValuesByTextContentProperty(this DbContext context, string nomAlias, string textContentProperty, string valueAsString)
        {
            var objectContext = ((IObjectContextAdapter)context).ObjectContext;

            return objectContext
                .CreateQuery<NomValue>(
                    "[" + objectContext.DefaultContainerName + "].[edmGetNomValuesByTextContentProperty](@nomAlias, @textContentProperty, @valueAsString)",
                    CreateClassObjectParameter("nomAlias", nomAlias),
                    CreateClassObjectParameter("textContentProperty", textContentProperty),
                    CreateClassObjectParameter("valueAsString", valueAsString));
        }

        private static ObjectParameter CreateClassObjectParameter<T>(string name, T value)
            where T : class
        {
            return value != null ?
                new ObjectParameter(name, value) :
                new ObjectParameter(name, typeof(T));
        }
    }
}
