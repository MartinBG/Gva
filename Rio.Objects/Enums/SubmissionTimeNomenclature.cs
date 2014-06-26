using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class SubmissionTimeNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.SubmissionTimeNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly SubmissionTimeNomenclature Applied = new SubmissionTimeNomenclature { ResourceKey = "Applied", Code = "01" };
        public static readonly SubmissionTimeNomenclature AircraftInspection = new SubmissionTimeNomenclature { ResourceKey = "AircraftInspection", Code = "02" };
        public static readonly SubmissionTimeNomenclature NotApply = new SubmissionTimeNomenclature { ResourceKey = "NotApply", Code = "03" };

    }
}
