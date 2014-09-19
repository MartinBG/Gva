using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regs.Api.Models
{
    public class PartCollection : List<PartVersion>
    {
        public PartCollection(IEnumerable<PartVersion> parts)
            : base(parts)
        {
        }

        public PartVersion<T> Get<T>(string path)
            where T : class
        {
            var partVersion = this.Where(pv => pv.Part.Path == path).SingleOrDefault();

            return partVersion == null ? null : new PartVersion<T>(partVersion);
        }

        public PartVersion<T>[] GetAll<T>(string pathSpec)
            where T : class
        {
            return this
                .Where(pv => pv.Part.Matches(pathSpec))
                .OrderBy(pv => pv.Part.Path)
                .Select(pv => new PartVersion<T>(pv))
                .ToArray();
        }
    }
}
