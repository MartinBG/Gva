using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.PrintRepository;
using Gva.Api.WordTemplates;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/wordTemplates")]
    [Authorize]
    public class WordTemplatesController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private IEnumerable<IDataGenerator> dataGenerators;

        public WordTemplatesController(
            IUnitOfWork unitOfWork,
            IEnumerable<IDataGenerator> dataGenerators)
        {
            this.unitOfWork = unitOfWork;
            this.dataGenerators = dataGenerators;
        }

        [Route("{templateId}")]
        [HttpPost]
        public IHttpActionResult ChangeDataGeneratorPerTemplate(int templateId, string dataGenerator)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                GvaWordTemplate template = this.unitOfWork.DbContext.Set<GvaWordTemplate>()
                    .Where(t => t.GvaWordTemplateId == templateId)
                    .Single();

                template.DataGeneratorCode = dataGenerator;

                this.unitOfWork.Save();

                transaction.Commit();

                var generator = this.dataGenerators.Where(d => d.GeneratorCode == dataGenerator).First();

                return Ok(new 
                { 
                    Name = generator.GeneratorName,
                    Code = generator.GeneratorCode
                });
            }
        }
    }
}