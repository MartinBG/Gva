using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class MonitoredRadioequipmentNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.MonitoredRadioequipmentNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly MonitoredRadioequipmentNomenclature Transmitters = new MonitoredRadioequipmentNomenclature { ResourceKey = "Transmitters", Code = "01" };
        public static readonly MonitoredRadioequipmentNomenclature ELT = new MonitoredRadioequipmentNomenclature { ResourceKey = "ELT", Code = "02" };
        public static readonly MonitoredRadioequipmentNomenclature Transponders = new MonitoredRadioequipmentNomenclature { ResourceKey = "Transponders", Code = "03" };
        public static readonly MonitoredRadioequipmentNomenclature WeatherRadar = new MonitoredRadioequipmentNomenclature { ResourceKey = "WeatherRadar", Code = "04" };
        public static readonly MonitoredRadioequipmentNomenclature TCAS = new MonitoredRadioequipmentNomenclature { ResourceKey = "TCAS", Code = "05" };
        public static readonly MonitoredRadioequipmentNomenclature DME = new MonitoredRadioequipmentNomenclature { ResourceKey = "DME", Code = "06" };
        public static readonly MonitoredRadioequipmentNomenclature RadioAltimeter = new MonitoredRadioequipmentNomenclature { ResourceKey = "RadioAltimeter", Code = "07" };

    }
}
