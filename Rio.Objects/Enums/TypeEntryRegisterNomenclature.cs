using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class TypeEntryRegisterNomenclature
    {
        [ScriptIgnore]
        public string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ResourceKey))
                {
                    return string.Empty;
                }
                else
                {
                    return App_LocalResources.TypeEntryRegisterNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly TypeEntryRegisterNomenclature Initial = new TypeEntryRegisterNomenclature { ResourceKey = "Initial", Code = "01" };
        public static readonly TypeEntryRegisterNomenclature Change = new TypeEntryRegisterNomenclature { ResourceKey = "Change", Code = "02" };
        public static readonly TypeEntryRegisterNomenclature Deletion = new TypeEntryRegisterNomenclature { ResourceKey = "Deletion", Code = "03" };
    }
}
