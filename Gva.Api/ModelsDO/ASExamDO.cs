using System.Collections.Generic;
using Docs.Api.DataObjects;

namespace Gva.Api.ModelsDO
{
    public class ASExamDO
    {
        public NomDo CommonQuestion { get; set; }

        public NomDo SpecializedQuestion { get; set; }

        public List<List<bool>> CommonQuestions { get; set; }

        public List<List<bool>> SpecializedQuestions { get; set; }

        public int SuccessThreshold { get; set; }
    }
}