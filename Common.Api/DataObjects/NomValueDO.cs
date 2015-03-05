using System.Data.Entity.ModelConfiguration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.Api.DataObjects
{
    public class NomValueDO
    {
        public int NomValueId { get; set; }

        public int NomId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public int? ParentValueId { get; set; }

        public string Alias { get; set; }

        public JContainer TextContent;

        public bool IsActive { get; set; }

        public int Order { get; set; }
    }
}
