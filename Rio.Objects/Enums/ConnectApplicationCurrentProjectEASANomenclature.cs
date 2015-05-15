using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class ConnectApplicationCurrentProjectEASANomenclature
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
                    return App_LocalResources.ConnectApplicationCurrentProjectEASANomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly ConnectApplicationCurrentProjectEASANomenclature NotApplicable = new ConnectApplicationCurrentProjectEASANomenclature { ResourceKey = "NotApplicable", Code = "notApplicable" };
        public static readonly ConnectApplicationCurrentProjectEASANomenclature TcOtc = new ConnectApplicationCurrentProjectEASANomenclature { ResourceKey = "TcOtc", Code = "tcOtc" };
        public static readonly ConnectApplicationCurrentProjectEASANomenclature ChangeRepair = new ConnectApplicationCurrentProjectEASANomenclature { ResourceKey = "ChangeRepair", Code = "changeRepair" };


        public static List<ConnectApplicationCurrentProjectEASANomenclature> Values = new List<ConnectApplicationCurrentProjectEASANomenclature>()
        {
            NotApplicable,
            TcOtc,
            ChangeRepair
        };
    }
}
