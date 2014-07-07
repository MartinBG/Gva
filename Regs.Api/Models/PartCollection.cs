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

        public PartVersion Get(string path)
        {
            return this
                .Where(pv => pv.Part.Path == path)
                .SingleOrDefault();
        }

        public PartVersion[] GetAll(string pathSpec)
        {
            return this
                .Where(pv => pv.Part.Matches(pathSpec))
                .OrderBy(pv => pv.Part.Path)
                .ToArray();
        }
    }
}
