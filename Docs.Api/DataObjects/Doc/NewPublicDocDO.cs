using System.Collections.Generic;

namespace Docs.Api.DataObjects
{
    public class NewPublicDocDO
    {
        public NewPublicDocDO()
        {
            this.Correspondents = new List<int>();
        }

        public string DocTypeAlias { get; set; }

        public List<int> Correspondents { get; set; }
    }
}
