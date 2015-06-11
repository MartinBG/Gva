using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Gva.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class WordTemplateDO
    {
        public int TemplateId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DataGeneratorDO DataGenerator { get; set; }

        public FileDataDO TemplateFile { get; set; }
    }
}