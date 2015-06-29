using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gva.Api.ModelsDO;

namespace Gva.Api.Repositories.WordTemplateRepository
{
    public interface IWordTemplateRepository
    {
        IList<WordTemplateDO> GetTemplates();

        WordTemplateDO GetTemplate(int templateId);

        void ChangeTemplateData(int templateId, WordTemplateDO template);

        void CreateNewTemplate(WordTemplateDO template);
    }
}
