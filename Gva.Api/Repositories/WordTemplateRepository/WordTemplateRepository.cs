using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Blob;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.WordTemplates;

namespace Gva.Api.Repositories.WordTemplateRepository
{
    public class WordTemplateRepository : IWordTemplateRepository
    {
        private IUnitOfWork unitOfWork;
        private IEnumerable<IDataGenerator> dataGenerators;
        private IAMLNationalRatingDataGenerator AMLNationalRatingDataGenerator;

        public WordTemplateRepository(
            IUnitOfWork unitOfWork,
            IEnumerable<IDataGenerator> dataGenerators,
            IAMLNationalRatingDataGenerator AMLNationalRatingDataGenerator)
        {
            this.unitOfWork = unitOfWork;
            this.dataGenerators = dataGenerators;
            this.AMLNationalRatingDataGenerator = AMLNationalRatingDataGenerator;
        }

        public IList<WordTemplateDO> GetTemplates()
        {
            var templates = this.unitOfWork.DbContext.Set<GvaWordTemplate>();
            IList<WordTemplateDO> results = new List<WordTemplateDO>();
            foreach (GvaWordTemplate template in templates)
            {
                var generator = dataGenerators.Where(dg => dg.GeneratorCode == template.DataGeneratorCode).SingleOrDefault();
                string generatorCode = null;
                string generatorName = null;
                if (generator != null)
                {
                    generatorCode = generator.GeneratorCode;
                    generatorName = generator.GeneratorName;

                }
                else if (generator == null && AMLNationalRatingDataGenerator.GeneratorCode == template.DataGeneratorCode)
                {
                    generatorCode = AMLNationalRatingDataGenerator.GeneratorCode;
                    generatorName = AMLNationalRatingDataGenerator.GeneratorName;
                }

                results.Add(new WordTemplateDO()
                {
                    Description = template.Description,
                    DataGenerator = new DataGeneratorDO()
                    {
                        Code = generatorCode,
                        Name = generatorName
                    },
                    TemplateId = template.GvaWordTemplateId,
                    Name = template.Name
                });
            }

            return results;
        }

        public DataGeneratorDO ChangeDataGeneratorPerTemplate(int templateId, string dataGenerator)
        {
            GvaWordTemplate template = this.unitOfWork.DbContext.Set<GvaWordTemplate>()
                .Where(t => t.GvaWordTemplateId == templateId)
                .Single();

            template.DataGeneratorCode = dataGenerator;

            this.unitOfWork.Save();

            var generator = this.dataGenerators.Where(d => d.GeneratorCode == dataGenerator).First();

            return new DataGeneratorDO
            {
                Name = generator.GeneratorName,
                Code = generator.GeneratorCode
            };
        }

        public void ChangeTemplateData(int templateId, WordTemplateDO template)
        {
            GvaWordTemplate templateData = this.unitOfWork.DbContext.Set<GvaWordTemplate>()
                .Where(t => t.GvaWordTemplateId == templateId)
                .Single();

            templateData.DataGeneratorCode = template.DataGenerator.Code;
            templateData.Description = template.Description;
            templateData.Name = template.Name;

            if (template.TemplateFile != null)
            {
                using (MemoryStream stream = new MemoryStream())
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                {
                    connection.Open();
                    using (var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", template.TemplateFile.Key))
                    {
                        blobStream.CopyTo(stream);
                        templateData.Template = stream.ToArray();
                    }
                }
            }

            this.unitOfWork.Save();
        }

        public WordTemplateDO GetTemplate(int templateId)
        {
            GvaWordTemplate template = this.unitOfWork.DbContext.Set<GvaWordTemplate>()
                    .Where(t => t.GvaWordTemplateId == templateId)
                    .Single();

            var generator = dataGenerators.Where(dg => dg.GeneratorCode == template.DataGeneratorCode).SingleOrDefault();
            string generatorCode = null;
            string generatorName = null;
            if (generator != null)
            {
                generatorCode = generator.GeneratorCode;
                generatorName = generator.GeneratorName;

            }
            else if (generator == null && AMLNationalRatingDataGenerator.GeneratorCode == template.DataGeneratorCode)
            {
                generatorCode = AMLNationalRatingDataGenerator.GeneratorCode;
                generatorName = AMLNationalRatingDataGenerator.GeneratorName;
            }

            return new WordTemplateDO()
            {
                Description = template.Description,
                DataGenerator = new DataGeneratorDO()
                {
                    Code = generatorCode,
                    Name = generatorName
                },
                TemplateId = template.GvaWordTemplateId,
                Name = template.Name
            };
        }

        public void CreateNewTemplate(WordTemplateDO template)
        {
            GvaWordTemplate newTemplate = new GvaWordTemplate()
            {
                Name = template.Name,
                Description = template.Description,
                DataGeneratorCode = template.DataGenerator.Code
            };

            using (MemoryStream stream = new MemoryStream())
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                connection.Open();
                using (var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", template.TemplateFile.Key))
                {
                    blobStream.CopyTo(stream);
                    newTemplate.Template = stream.ToArray();
                }
            }

            this.unitOfWork.DbContext.Set<GvaWordTemplate>().Add(newTemplate);

            this.unitOfWork.Save();
        }
    }
}
