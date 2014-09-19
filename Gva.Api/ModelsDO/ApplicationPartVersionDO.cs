using System.Collections.Generic;
using System.Linq;
using Gva.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class ApplicationPartVersionDO<T> where T : class, new()
    {
        public ApplicationPartVersionDO(PartVersion<T> partVersion)
        {
            this.PartIndex = partVersion.Part.Index;
            this.PartId = partVersion.PartId;
            this.Part = partVersion.Content;
            this.Applications = new List<ApplicationNomDO>();
        }

        public ApplicationPartVersionDO(PartVersion<T> partVersion, GvaApplication[] lotObjects)
            : this(partVersion)
        {
            this.Applications = lotObjects
                .Select(ga => new ApplicationNomDO(ga))
                .ToList();
        }

        public ApplicationPartVersionDO(T part, List<ApplicationNomDO> applications = null)
            : this()
        {
            this.Part = part;

            if (applications != null)
            {
                this.Applications = applications;
            }
        }

        public ApplicationPartVersionDO()
        {
            this.Applications = new List<ApplicationNomDO>();
            this.Part = new T();
        }

        public int PartIndex { get; set; }

        public int PartId { get; set; }

        public T Part { get; set; }

        public List<ApplicationNomDO> Applications { get; set; }
    }
}
