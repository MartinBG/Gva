
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class AuthorTypeNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.AuthorTypeNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly AuthorTypeNomenclature Physical = new AuthorTypeNomenclature { ResourceKey = "Physical", Code = "01" };
        public static readonly AuthorTypeNomenclature Entity = new AuthorTypeNomenclature { ResourceKey = "Entity", Code = "02" };

        public static readonly AuthorTypeNomenclature PhysicalBulgarian = new AuthorTypeNomenclature { ResourceKey = "PhysicalBulgarian", Code = "03" };
        public static readonly AuthorTypeNomenclature PhysicalForeign = new AuthorTypeNomenclature { ResourceKey = "PhysicalForeign", Code = "04" };
        
    }
}
