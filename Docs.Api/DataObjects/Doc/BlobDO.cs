using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class BlobDO
    {
        public BlobDO()
        {
        }

        public Guid Key { get; set; }
        public string Name { get; set; }
        public string RelativePath { get; set; }
    }
}
