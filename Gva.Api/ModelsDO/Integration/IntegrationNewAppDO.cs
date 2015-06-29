using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Models;
using Docs.Api.DataObjects;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Persons;

namespace Gva.Api.ModelsDO.Integration
{
    public class IntegrationNewAppDO
    {
        public int DocId { get; set; }

        public int LotId { get; set; }

        public NomValue ApplicationType { get; set; }
    }
}
