﻿using Autofac;
using Autofac.Integration.WebApi;
using Common.Data;
using Gva.Api.Controllers;
using Gva.Api.LotEventHandlers;
using Gva.Api.LotEventHandlers.InventoryView;
using Gva.Api.LotEventHandlers.PersonView;
using Gva.Api.Models;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.InventoryRepository;
using Gva.Api.Repositories.PersonRepository;
using Regs.Api.LotEvents;

namespace Gva.Api
{
    public class GvaApiModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<GvaDbConfiguration>().As<IDbConfiguration>().SingleInstance();

            moduleBuilder.RegisterType<PersonDataHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonEmploymentHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonLicenceHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonRatingHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<ApplicationHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<CheckHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<DocumentIdHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<EducationHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<EmploymentHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<MedicalHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<OtherHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<TrainingHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<ApplicationLotEventHandler>().As<ILotEventHandler>().InstancePerApiRequest();

            moduleBuilder.RegisterType<PersonRepository>().As<IPersonRepository>();
            moduleBuilder.RegisterType<ApplicationRepository>().As<IApplicationRepository>();
            moduleBuilder.RegisterType<InventoryRepository>().As<IInventoryRepository>();
            moduleBuilder.RegisterType<FileRepository>().As<IFileRepository>();
            moduleBuilder.RegisterType<CaseTypeRepository>().As<ICaseTypeRepository>();

            //controllers
            moduleBuilder.RegisterType<ApplicationsController>().InstancePerApiRequest();
            moduleBuilder.RegisterType<GvaLotsController>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonsController>().InstancePerApiRequest();
            moduleBuilder.RegisterType<GvaNomController>().InstancePerApiRequest();
        }
    }
}
