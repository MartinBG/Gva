using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class PersonKindRecordRatingNomenclature
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
                    return App_LocalResources.PersonKindRecordRatingNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly PersonKindRecordRatingNomenclature Senior = new PersonKindRecordRatingNomenclature { ResourceKey = "Senior", Code = "01" };
        public static readonly PersonKindRecordRatingNomenclature Instructor = new PersonKindRecordRatingNomenclature { ResourceKey = "Instructor", Code = "02" };

        public static readonly List<PersonKindRecordRatingNomenclature> Values = new List<PersonKindRecordRatingNomenclature>()
        {
            Senior, 
            Instructor
        };

    }
}
