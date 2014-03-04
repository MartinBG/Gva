using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class DocLinkDO
    {
        public DocLinkDO()
        {
        }

        public DocLinkDO(int id, string url, string name)
            : this()
        {
            this.Id = id;
            this.Url = url;
            this.Name = name;
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
    }
}
