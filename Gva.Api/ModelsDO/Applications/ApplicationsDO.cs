using Gva.Api.Models;
using System;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO.Applications
{
    public class ApplicationsDO
    {
        public IEnumerable<ApplicationListDO> Applications { get; set; }

        public int ApplicationsCount { get; set; }
    }
}
