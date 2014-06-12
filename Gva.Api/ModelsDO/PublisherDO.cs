using System;
using System.Linq;
using System.Collections.Generic;
using Gva.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class PublisherDO
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public PublisherType PublisherType { get; set; }
    }
}