using System.Configuration;
using Autofac;
using Common.Data;
using Common.Http;
using Common.Jobs;
using Docs.Api.Controllers;
using Docs.Api.EmailSender;
using Docs.Api.Jobs;
using Docs.Api.Models;
using Docs.Api.Repositories.CorrespondentRepository;
using Docs.Api.Repositories.DocRepository;
using Docs.Api.Repositories.ClassificationRepository;
using Docs.Api.Repositories.EmailRepository;
using Docs.Api.Repositories.UnitRepository;
using Docs.Api.BusinessLogic;

namespace Docs.Api
{
    public class DocsApiModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<DocsWebApiConfig>().As<IWebApiConfig>().SingleInstance();
            moduleBuilder.RegisterType<DocsDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<CorrespondentRepository>().As<ICorrespondentRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<DocRepository>().As<IDocRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ClassificationRepository>().As<IClassificationRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EmailRepository>().As<IEmailRepository>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<UnitRepository>().As<IUnitRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UnitBusinessLogic>().As<IUnitBusinessLogic>().InstancePerLifetimeScope();

            //controllers
            moduleBuilder.RegisterType<CorrespondentController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<DocController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AbbcdnController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<DocNomController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UnitController>().InstancePerLifetimeScope();

            string enableEmailsJobConf = ConfigurationManager.AppSettings["Docs.Api:EnableEmailsJob"];

            //Jobs
            bool enableEmailsJob;
            if (!string.IsNullOrEmpty(enableEmailsJobConf) && bool.TryParse(enableEmailsJobConf, out enableEmailsJob) && enableEmailsJob)
            {
                moduleBuilder.RegisterType<EmailsJob>().As<IJob>().ExternallyOwned();
                moduleBuilder.RegisterType<EmailSender.EmailSender>().As<IEmailSender>();
            }
        }
    }
}
