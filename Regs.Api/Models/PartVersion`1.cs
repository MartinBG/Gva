using System;
using Newtonsoft.Json;

namespace Regs.Api.Models
{
    public class PartVersion<T> : PartVersion where T : class
    {
        public PartVersion()
        { }

        public PartVersion(PartVersion partVersion)
            : base(partVersion)
        { }

        private T content = null;

        public T Content
        {
            get
            {
                if (this.content == null)
                {
                    this.content = JsonConvert.DeserializeObject<T>(this.TextContent);
                }

                return this.content;
            }
        }
    }
}
