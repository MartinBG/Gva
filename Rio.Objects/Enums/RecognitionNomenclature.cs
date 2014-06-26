using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class RecognitionNomenclature
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
                    return App_LocalResources.RecognitionNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }

        public static readonly RecognitionNomenclature Internship = new RecognitionNomenclature { ResourceKey = "Internship", Code = "01" };
        public static readonly RecognitionNomenclature Exam = new RecognitionNomenclature { ResourceKey = "Exam", Code = "02" };


        public static List<RecognitionNomenclature> Values = new List<RecognitionNomenclature>()
        {
            Internship,
            Exam
        };
    }
}
