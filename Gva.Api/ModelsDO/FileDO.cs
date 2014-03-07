using System.Collections.Generic;

namespace Gva.Api.ModelsDO
{
    public class FileDO
    {
        public string Key { get; set; }

        public string Name { get; set; }

        public IEnumerable<ApplicationDO> Applications { get; set; }
    }
}