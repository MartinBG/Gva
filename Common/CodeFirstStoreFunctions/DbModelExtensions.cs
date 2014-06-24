using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstStoreFunctions
{
    public static class DbModelExtensions
    {
        public static IEnumerable<PropertyMapping> GetEntityTypePropertyMappings(this DbModel model, EntityType entityType)
        {
            var entityTypeMapping =
                model.ConceptualToStoreMapping.EntitySetMappings.SelectMany(s => s.EntityTypeMappings)
                    .Single(t => t.EntityType == entityType);

            return entityTypeMapping.Fragments.SelectMany(f => f.PropertyMappings).Where(p => entityType.Properties.Contains(p.Property));
        }
    }
}
