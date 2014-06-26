using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class ProvidedOperationTypesNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.ProvidedOperationTypesNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly ProvidedOperationTypesNomenclature PassengersTransport = new ProvidedOperationTypesNomenclature { ResourceKey = "PassengersTransport", Code = "01" };
        public static readonly ProvidedOperationTypesNomenclature CargoTransport = new ProvidedOperationTypesNomenclature { ResourceKey = "CargoTransport", Code = "02" };
        public static readonly ProvidedOperationTypesNomenclature PersonalUsage = new ProvidedOperationTypesNomenclature { ResourceKey = "PersonalUsage", Code = "03" };
        public static readonly ProvidedOperationTypesNomenclature AviationalActivity = new ProvidedOperationTypesNomenclature { ResourceKey = "AviationalActivity", Code = "04" };
        public static readonly ProvidedOperationTypesNomenclature SpecializedTransport = new ProvidedOperationTypesNomenclature { ResourceKey = "SpecializedTransport", Code = "05" };
    }
}
